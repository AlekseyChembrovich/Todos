using System.Data;
using Microsoft.Extensions.Configuration;

namespace Todos.Infrastructure.Dapper.Data;

public class DbConnectionProvider : IDisposable
{
	private readonly Func<IDbConnection> _initializer;
    private IDbConnection _connection;

    public string ResourceDbScheme { get; private set; }
    public IDbTransaction Transaction { get; private set; }

    public DbConnectionProvider(Func<IDbConnection> initializer, IConfiguration configuration)
	{
        _initializer = initializer;
        ResourceDbScheme = configuration.GetSection("DbSchemes")
                                        .GetValue<string>("Resource");
    }

    public IDbConnection GetConnection()
    {
        if (_connection is null)
        {
            _connection = _initializer?.Invoke();
            _connection?.Open();
        }

        return _connection;
    }

    public IDbTransaction InitializeTransaction()
    {
        if (Transaction is null)
        {
            Transaction = _connection?.BeginTransaction();
        }

        return Transaction;
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
        Transaction?.Dispose();
    }
}
