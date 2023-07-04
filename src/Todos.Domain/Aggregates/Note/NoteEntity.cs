using Todos.Domain.Common;
using Todos.Domain.Exceptions;

namespace Todos.Domain.Aggregates.Note;

public class Note : AggregateRoot
{
    public string Title { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiryDate { get; private set; }

    private Note() { }

    public Note(Guid noteId, string title, DateTime createdAt, DateTime expiryDate)
    {
        Id = noteId;
        Title = title;
        CreatedAt = createdAt;
        if (CreatedAt >= expiryDate)
        {
            throw new DomainException("Note creation date must be greater than expiration date.");
        }
        
        ExpiryDate = expiryDate;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException("Note title cannot be empty.");
        }

        Title = title;
    }
    
    public void SetExpirationDate(DateTime expiryDate)
    {
        if (CreatedAt >= expiryDate)
        {
            throw new DomainException("Note expiration date must be smaller than creation date.");
        }

        ExpiryDate = expiryDate;
    }
}
