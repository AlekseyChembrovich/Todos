using MediatR;
using System.Reflection;
using Todos.AuthApi.Data;
using Todos.AuthApi.Configuration;

const string LocalCorsPolicyKey = nameof(LocalCorsPolicyKey);

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var environment = builder.Environment;

RegistrationServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var authContext = scope.ServiceProvider.GetRequiredService<IdentitiesDbContext>();
    try
    {
        IdentitiesDbInitializer.Initialize(authContext);
        await SeedData.EnsureSeedData(authContext, scope);
    }
    catch (Exception)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogCritical("Cannot create database.");
    }
}

Configuration(app);

app.Run();

void RegistrationServices(IServiceCollection services)
{
    services.AddIdentityServer4();

    services.AddCorsPolicy(LocalCorsPolicyKey);
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddMvc()
            .AddRazorRuntimeCompilation();

    services.AddMediatR(Assembly.GetExecutingAssembly());
}

void Configuration(WebApplication app)
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseCors(LocalCorsPolicyKey);
    app.UseRouting();

    app.UseAuthentication();
    app.UseIdentityServer();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
    });
}
