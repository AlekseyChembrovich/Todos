using Todos.Api.Services;
using Todos.Api.Middleware;
using Todos.Api.Configuration;
using Todos.Application.Interfaces;
using Todos.Application.Configuration;
using Todos.Infrastructure.Configuration;
using ApiConfig = Todos.Api.Configuration;

const string LocalCorsPolicyKey = nameof(LocalCorsPolicyKey);

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;

ApiConfig.ServicesConfiguration.SetUpSerilog(configuration);

ConfigureServices(builder.Services);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddInfrastructureServices(configuration);
    services.AddApplicationServices();

    services.AddCorsPolicy(LocalCorsPolicyKey);
    services.AddOpenApi();
    services.AddEndpointsApiExplorer();
    services.AddIdentityAuthentication(configuration);

    services.AddHttpContextAccessor();
    services.AddTransient<IUserContextService, UserContextService> ();

    services.AddControllers()    
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
}

void ConfigureMiddleware(WebApplication app)
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseCors(LocalCorsPolicyKey);
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseMiddleware<LogEnrichMiddleware>();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
