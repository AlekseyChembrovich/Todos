namespace Todos.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}
