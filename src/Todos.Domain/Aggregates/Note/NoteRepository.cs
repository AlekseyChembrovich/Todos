using Todos.Domain.Abstractions;

namespace Todos.Domain.Aggregates.Note;

public interface INoteRepository : IRepository<Note>
{
    Task<bool> UpdateAsync(Guid noteId, string title, DateTime expiryDate, CancellationToken cancellationToken = default);
}
