using McWebsite.Application.Common.Services;
using McWebsite.Infrastructure.Authentication;
using McWebsite.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using McWebsite.Application.Common.Interfaces.Authentication;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using McWebsite.Infrastructure.Persistence.Repositories;
using McWebsite.Infrastructure.Persistence.Interceptors;
using Serilog;
using Serilog.Events;
using Serilog.Core.Enrichers;
using Serilog.Core;
using Serilog.Exceptions;

namespace McWebsite.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddPersistance(configuration);
            services.AddAuth(configuration);
            services.ConfigureSerilog(configuration);

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<McWebsiteDbContext>(options =>
            options.UseSqlServer(configuration["ConnectionString"]));

            services.AddScoped<PublishDomainEventsInterceptor>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGameServerRepository, GameServerRepository>();

            return services;
        }
        public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            JwtSettings jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });

            return services;
        }

        public static IServiceCollection ConfigureSerilog(this IServiceCollection services, ConfigurationManager configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel
                    .Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel
                    .Information()
                .WriteTo
                    .Console()
                .WriteTo
                    .MSSqlServer(
                    connectionString: configuration["ConnectionString"],
                    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        AutoCreateSqlTable = true
                    }
                    )
                .Enrich
                    .WithExceptionDetails()
                .Enrich
                    .FromLogContext()
                .CreateLogger();

            return services;
        }
    }
}
