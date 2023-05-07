using McWebsite.Application.Common.Services;
using McWebsite.Infrastructure.Authentication;
using McWebsite.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using McWebsite.Application.Common.Interfaces.Authentication;
using McWebsite.Application.Common.Interfaces.Persistence;
using McWebsite.Infrastructure.Persistence;

namespace McWebsite.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
