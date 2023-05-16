using McWebsite.API.Common.Errors;
using McWebsite.API.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace McWebsite.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<ProblemDetailsFactory, McWebsiteProblemDetailsFactory>();
            services.AddMapping();

            return services;
        }
    }
}
