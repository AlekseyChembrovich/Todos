using Todos.Api;
using Todos.Api.Hubs;
using Todos.Application;
using Todos.Api.Services;
using Todos.Api.Middleware;
using Todos.Infrastructure.EfCore;
using Todos.Infrastructure.Dapper;
using Todos.Infrastructure.EfCore.Data;
using Todos.Application.Common.Interfaces;
using ApiConf = Todos.Api.ServicesConfiguration;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;
configuration.SetUpSerilog();

ConfigureServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        //DbInitializer.Initialize(scope.ServiceProvider);
    }
    catch (Exception)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogCritical("Cannot create database.");
    }
}

ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    //services.AddEfCoreContext(configuration);
    //services.AddEfCoreRepositories();

    services.AddDapperContext(configuration);
    services.AddDapperRepositories();

    services.AddApplicationServices();

    services.AddCorsPolicy(ApiConf.LocalCorsPolicyKey);
    services.AddOpenApi();
    services.AddEndpointsApiExplorer();
    services.AddIdentityAuthentication(configuration);

    services.AddHttpContextAccessor();
    services.AddTransient<IUserContextService, UserContextService> ();

    services.AddSignalR();
    services.AddControllers();
}

void ConfigureMiddleware(WebApplication app)
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseCors(ApiConf.LocalCorsPolicyKey);
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseMiddleware<LogEnrichMiddleware>();

    app.MapHub<NotesHub>("/notesHub");
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
