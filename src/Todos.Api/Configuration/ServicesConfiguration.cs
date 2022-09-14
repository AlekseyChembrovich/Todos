using Serilog;
using Microsoft.OpenApi.Models;

namespace Todos.Api.Configuration;

public static class ServicesConfiguration
{
    public static void SetUpSerilog(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(configuration)
            .CreateBootstrapLogger();
    }

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

    public static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Todos API",
                Version = "v1"
            });

            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddIdentityAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                var authority = configuration.GetValue<string>("AuthConfig:Authority"); // "http://localhost:5003/"
                options.Authority = authority;     
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = false
                };
            });

        return services;
    }
}
