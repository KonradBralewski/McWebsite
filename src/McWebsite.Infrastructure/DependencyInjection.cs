using McWebsite.Application.Common.Interfaces;
using McWebsite.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace McWebsite.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            return services;
        }
    }
}
