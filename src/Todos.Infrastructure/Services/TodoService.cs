using MongoDB.Driver;
using Todos.Domain.Entities.Todo;
using Microsoft.Extensions.Options;
using Todos.Application.Interfaces;
using Todos.Infrastructure.Common.Models;

namespace Todos.Infrastructure.Services;

public class TodoService : ITodoService
{
    private readonly IMongoCollection<TodoItem> _todosCollection;

    public TodoService(IOptions<TodosDatabaseSettings> todosDatabaseSettings)
    {
        var mongoClient = new MongoClient(todosDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(todosDatabaseSettings.Value.DatabaseName);
        _todosCollection = mongoDatabase.GetCollection<TodoItem>(todosDatabaseSettings.Value.CollectionName);
    }

    public async Task<IEnumerable<TodoItem>> GetTodosAsync(string userId)
    {
        return await _todosCollection.Find(x => x.UserId.Equals(userId)).ToListAsync();
    }

    public async Task<TodoItem> GetTodoAsync(string id)
    {
        return await _todosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<TodoItem> CreateTodoAsync(TodoItem todo)
    {
        todo.Id = Guid.NewGuid().ToString();
        todo.CreatedDate = DateTime.Now.Date;
        await _todosCollection.InsertOneAsync(todo);
        return todo;
    }

    public async Task UpdateTodoAsync(TodoItem todo)
    {
        await _todosCollection.ReplaceOneAsync(x => x.Id == todo.Id, todo);
    }

    public async Task RemoveTodoAsync(string id)
    {
        await _todosCollection.DeleteOneAsync(x => x.Id == id);
    }
}
