using Todos.Application.Interfaces;
using Todos.Infrastructure.Services;
using Todos.Infrastructure.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Todos.Infrastructure.Configuration;

public static class ServicesConfiguration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TodosDatabaseSettings>(configuration.GetSection("TodosDatabase"));
        services.AddScoped<ITodoService, TodoService>();

        return services;
    }
}
