using Todos.Domain.Aggregates.Note;
using Microsoft.EntityFrameworkCore;
using Todos.Infrastructure.EfCore.Data;

namespace Todos.Infrastructure.EfCore.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly ApplicationDbContext _context;

    public NoteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Note>> GetListAsync(CancellationToken cancellationToken = default)
    {
        var notes = await _context.Notes.ToListAsync(cancellationToken);

        return notes;
    }

    public async Task<Note> GetByIdAsync(Guid noteId, CancellationToken cancellationToken = default)
    {
        var note = await _context.Notes.SingleOrDefaultAsync(x => x.Id == noteId, cancellationToken);

        return note;
    }

    public async Task<Note> CreateAsync(Note note, CancellationToken cancellationToken = default)
    {
        var entry = await _context.Notes.AddAsync(note, cancellationToken);

        return entry.Entity;
    }

    public async Task<bool> RemoveAsync(Guid noteId, CancellationToken cancellationToken = default)
    {
        var foundNote = await GetByIdAsync(noteId, cancellationToken);
        if (foundNote is null)
            return false;

        var entry = _context.Notes.Remove(foundNote);

        return true;
    }

    public async Task<bool> UpdateAsync(Guid noteId, string title, DateTime expiryDate, CancellationToken cancellationToken = default)
    {
        var foundNote = await GetByIdAsync(noteId, cancellationToken);
        if (foundNote is null)
            return false;

        foundNote.SetTitle(title);
        foundNote.SetExpirationDate(expiryDate);

        return true;
    }
}
