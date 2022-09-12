namespace Todos.Domain.Entities.Todo;

public class TodoItem
{
    public string Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public string Task { get; set; }
    public DateTime ExpirationDate { get; set; }

    public string UserId { get; set; }
}
