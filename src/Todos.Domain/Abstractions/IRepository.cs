namespace Todos.Domain.Abstractions;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetListAsync(CancellationToken cancellationToken = default);

    Task<T> GetByIdAsync(Guid entityId, CancellationToken cancellationToken = default);

    Task<bool> RemoveAsync(Guid noteId, CancellationToken cancellationToken = default);

    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
}
