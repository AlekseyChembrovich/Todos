using Todos.Domain.Entities.Todo;

namespace Todos.Application.Interfaces;

public interface ITodoService
{
    public Task<IEnumerable<TodoItem>> GetTodosAsync(string userId);

    public Task<TodoItem> GetTodoAsync(string id);

    public Task<TodoItem> CreateTodoAsync(TodoItem todo);

    public Task UpdateTodoAsync(TodoItem todo);

    public Task RemoveTodoAsync(string id);
}
