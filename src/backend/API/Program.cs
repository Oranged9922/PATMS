using API;
using API.Common.Errors;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    _ = builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddCors();
    _ = builder.Services.AddControllers();

    _ = builder.Services.AddSingleton<ProblemDetailsFactory, PatmsProblemDetailsFactory>();
}

WebApplication app = builder.Build();
{
    _ = app
        .UseExceptionHandler("/error")
        .UseHttpsRedirection()
        .UseAuthentication()
        .UseAuthorization();
    _ = app.MapControllers();
    app.UseCors(builder =>
    {
        builder.WithOrigins("http://localhost:5230")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
    app.Run();
}