using System.Data;
using Todos.Domain.Abstractions;

namespace Todos.Infrastructure.Dapper.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IDbTransaction _transaction;
    private bool _commited = false;

    public UnitOfWork(DbConnectionProvider connectionProvider)
    {
        _ = connectionProvider.GetConnection();
        _transaction = connectionProvider.InitializeTransaction();
    }

    public Task<bool> SaveChangesAsync()
    {
        _transaction.Commit();
        _commited = true;

        return Task.FromResult(_commited);
    }

    public void Dispose()
    {
        if (!_commited)
        {
            _transaction?.Rollback();
        }
    }
}
