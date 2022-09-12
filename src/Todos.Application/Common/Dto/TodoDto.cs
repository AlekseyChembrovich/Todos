namespace Todos.Application.Common.Dto;

public class TodoDto
{
    public string Id { get; set; }
    public string Task { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}
