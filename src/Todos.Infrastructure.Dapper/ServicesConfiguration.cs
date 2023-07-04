using Npgsql;
using Todos.Domain.Abstractions;
using Todos.Domain.Aggregates.Note;
using Todos.Infrastructure.Dapper.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todos.Infrastructure.Dapper.Repositories;

namespace Todos.Infrastructure.Dapper;

public static class ServicesConfiguration
{
    public static IServiceCollection AddDapperContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<DbConnectionProvider>(_ =>
        {
            var connectionString = configuration.GetConnectionString("ResourceDbConnectionString");
            return new DbConnectionProvider(() => new NpgsqlConnection(connectionString), configuration);
        });

        return services;
    }

    public static IServiceCollection AddDapperRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<INoteRepository, NoteRepository>();

        return services;
    }
}
