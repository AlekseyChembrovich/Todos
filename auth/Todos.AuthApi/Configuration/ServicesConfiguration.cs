using Todos.AuthApi.Data;
using Todos.AuthApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todos.AuthApi.Services.Interfaces;

namespace Todos.AuthApi.Configuration;

public static class ServicesConfiguration
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, string policyKey)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(policyKey, policy =>
            {
                policy.AllowAnyOrigin();   
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });

        return services;
    }

    public static IServiceCollection AddIdentityServer4(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(setup =>
        {
            setup.Password.RequiredLength = 4;
            setup.Password.RequireDigit = false;
            setup.Password.RequireNonAlphanumeric = false;
            setup.Password.RequireLowercase = false;
            setup.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<IdentitiesDbContext>()
          .AddDefaultTokenProviders();

        services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddInMemoryApiScopes(IdentityServer4Configuration.GetApiScopes())
                .AddInMemoryApiResources(IdentityServer4Configuration.GetApiResources())
                .AddInMemoryIdentityResources(IdentityServer4Configuration.GetIdentityResources())
                .AddInMemoryClients(IdentityServer4Configuration.GetClients(configuration))
                .AddDeveloperSigningCredential();

        services.ConfigureApplicationCookie(config =>
        {
            config.Cookie.Name = "IdentityServer.Cookie";
            config.LoginPath = "/Identity/Login";
            config.LogoutPath = "/Identity/Logout";
        });

        services.AddDbContext<IdentitiesDbContext>(options =>
        {
            options.UseInMemoryDatabase("IdentitiesDatabase");
        });

        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
