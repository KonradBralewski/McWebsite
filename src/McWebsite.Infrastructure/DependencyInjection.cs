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
using Microsoft.AspNetCore.Identity;
using System;
using McWebsite.Infrastructure.Persistence.Identity;
using McWebsite.Domain.GameServerSubscription;
using McWebsite.Application.Common.Interfaces.Services;
using McWebsite.Application.Common.Interfaces.DomainIntegration;
using McWebsite.Infrastructure.Persistence.Integration;

namespace McWebsite.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddPersistance(configuration);
            services.ConfigureIdentity();
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
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IGameServerRepository, GameServerRepository>();
            services.AddScoped<IGameServerReportRepository, GameServerReportRepository>();
            services.AddScoped<IGameServerSubscriptionRepository, GameServerSubscriptionRepository>();
            services.AddScoped<IInGameEventRepository, InGameEventRepository>();
            services.AddScoped<IInGameEventOrderRepository, InGameEventOrderRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IConversationRepository, ConversationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IConversationAndMessagesIntegration, ConversationAndMessagesIntegration>();

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
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .WriteTo.Console()
                .WriteTo.MSSqlServer(
                    connectionString: configuration["ConnectionString"],
                    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        AutoCreateSqlTable = true
                    }
                    )
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext()
                .CreateLogger();

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<McWebsiteIdentityUser>()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<McWebsiteDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
