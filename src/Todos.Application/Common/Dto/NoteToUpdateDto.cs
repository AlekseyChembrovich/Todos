namespace Todos.Application.Common.Dto;

public class NoteToUpdateDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public DateTime ExpiryDate { get; init; }
}
