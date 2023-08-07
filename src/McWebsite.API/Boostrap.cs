using Azure.Identity;
using Serilog;
using System.Configuration;

namespace McWebsite.API
{
    /// <summary>
    /// Boostrap class used to init necessary middlewares / services / attributes and run the application.
    /// </summary>
    public static class Boostrap
    {
        public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
        {
            builder.AddAzureKeyVaults();

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
