using McWebsite.API;
using McWebsite.API.Common.Errors;
using McWebsite.Application;
using McWebsite.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Host.UseSerilog();
    builder.Services.AddEndpointsApiExplorer();
}

var app = builder.Build();
{
    app.UseSerilogRequestLogging();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
