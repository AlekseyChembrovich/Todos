using Todos.Domain.Abstractions;
using Todos.Domain.Aggregates.Note;
using Microsoft.EntityFrameworkCore;
using Todos.Infrastructure.EfCore.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todos.Infrastructure.EfCore.Repositories;

namespace Todos.Infrastructure.EfCore;

public static class ServicesConfiguration
{
    public static IServiceCollection AddEfCoreContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ResourceDbConnectionString");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }

    public static IServiceCollection AddEfCoreRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<INoteRepository, NoteRepository>();

        return services;
    }
}
