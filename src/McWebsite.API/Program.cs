using McWebsite.API;
using McWebsite.Application;
using McWebsite.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Configure();

    builder.Services.AddPresentation();

    builder.Services.AddApplication();

    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Host.UseSerilog();

    builder.Services.AddEndpointsApiExplorer();
}

var app = builder.Build();
{
    app.InitAndRun();
}
