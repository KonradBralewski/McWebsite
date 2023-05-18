using ErrorOr;
using McWebsite.Application.Authentication.Commands;
using McWebsite.Application.Authentication.Commands.Register;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using System.Reflection;
using McWebsite.Application.Authentication.Commands.Common;

namespace McWebsite.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));

            return services;
        }
    }
}
