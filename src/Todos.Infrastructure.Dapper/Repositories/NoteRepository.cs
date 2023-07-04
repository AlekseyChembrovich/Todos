using Dapper;
using System.Data;
using Todos.Domain.Aggregates.Note;
using Todos.Infrastructure.Dapper.Data;

namespace Todos.Infrastructure.Dapper.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly DbConnectionProvider _connectionProvider;
    private readonly IDbConnection _connection;
    private readonly string _dbScheme;

    public NoteRepository(DbConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
        _connection = _connectionProvider.GetConnection();
        _dbScheme = connectionProvider.ResourceDbScheme;
    }

    public async Task<IEnumerable<Note>> GetListAsync(CancellationToken cancellationToken = default)
    {
        var transaction = _connectionProvider.Transaction;

        var notes = await _connection.QueryAsync<Note>(
            @$"SELECT * FROM {_dbScheme}.""Note""",
            transaction: transaction,
            commandType: CommandType.Text);

        return notes;
    }

    public async Task<Note> GetByIdAsync(Guid noteId, CancellationToken cancellationToken = default)
    {
        var transaction = _connectionProvider.Transaction;

        var note = await _connection.QueryFirstAsync<Note>(
            @$"SELECT * FROM {_dbScheme}.""Note"" WHERE ""Id"" = @NoteId",
            new { NoteId = noteId },
            transaction: transaction,
            commandType: CommandType.Text);

        return note;
    }

    public async Task<Note> CreateAsync(Note note, CancellationToken cancellationToken = default)
    {
        var transaction = _connectionProvider.Transaction;

        var result = await _connection.ExecuteAsync(
            @$"INSERT INTO {_dbScheme}.""Note"" (""Id"", ""Title"", ""CreatedAt"", ""ExpiryDate"")
            VALUES (@Id, @Title, @CreatedAt, @ExpiryDate)",
            new { note.Id, note.Title, note.CreatedAt, note.ExpiryDate },
            transaction: transaction,
            commandType: CommandType.Text);

        return result > 0 ? note : null;
    }

    public async Task<bool> RemoveAsync(Guid noteId, CancellationToken cancellationToken = default)
    {
        var transaction = _connectionProvider.Transaction;

        var foundNote = await GetByIdAsync(noteId, cancellationToken);
        if (foundNote is null)
            return false;

        var result = await _connection.ExecuteAsync(
            @$"DELETE FROM {_dbScheme}.""Note"" WHERE ""Id"" = @NoteId",
            new { NoteId = noteId },
            transaction: transaction,
            commandType: CommandType.Text);

        return result > 0;
    }

    public async Task<bool> UpdateAsync(
        Guid noteId,
        string title,
        DateTime expiryDate,
        CancellationToken cancellationToken = default)
    {
        var transaction = _connectionProvider.Transaction;

        var foundNote = await GetByIdAsync(noteId, cancellationToken);
        if (foundNote is null)
            return false;

        var result = await _connection.ExecuteAsync(
            @$"UPDATE {_dbScheme}.""Note"" SET ""Title""=@Title, ""ExpiryDate""=@ExpiryDate  WHERE ""Id"" = @NoteId",
            new { NoteId = noteId, Title = title, ExpiryDate = expiryDate },
            transaction: transaction,
            commandType: CommandType.Text);

        return result > 0;
    }
}
