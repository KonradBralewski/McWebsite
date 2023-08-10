using Azure.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Serilog;
using System.Threading.RateLimiting;

namespace McWebsite.API
{
    /// <summary>
    /// Bootstrap class used to init necessary middlewares / services / attributes and run the application.
    /// </summary>
    public static class Bootstrap
    {
        public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
        {
            builder.AddAzureKeyVaults();

            builder.ConfigureRateLimiter();

            return builder;
        }

        private static WebApplicationBuilder AddAzureKeyVaults(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                return builder;
            }

            var keyVaultEndpoint = new Uri(builder.Configuration["VaultUri"]!);

            builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

            return builder;
        }

        private static WebApplicationBuilder ConfigureRateLimiter(this WebApplicationBuilder builder)
        {
            builder.Services.AddRateLimiter(_ => _
                .AddFixedWindowLimiter(policyName: "fixed", options =>
                {
                    options.PermitLimit = 10;
                    options.Window = TimeSpan.FromSeconds(15);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 4;
                }));

            return builder;
        }

        public static void InitAndRun(this WebApplication app)
        {
            app.UseExceptionHandler("/exceptions");

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
