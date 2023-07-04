using MediatR;
using Todos.Domain.Abstractions;
using Todos.Application.Common.Dto;
using Todos.Domain.Aggregates.Note;
using Entities = Todos.Domain.Aggregates.Note;

namespace Todos.Application.Behaviors.Note.Commands;

public record CreateNoteCommand(string Title, DateTime ExpiryDate) : IRequest<NoteDto>;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteDto>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNoteCommandHandler(INoteRepository noteRepository, IUnitOfWork unitOfWork)
    {
        _noteRepository = noteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<NoteDto> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var noteId = Guid.NewGuid();
        var now = DateTime.UtcNow;
        var note = new Entities.Note(noteId, request.Title, now, request.ExpiryDate);

        var createdNote = await _noteRepository.CreateAsync(note, cancellationToken);

        _ = await _unitOfWork.SaveChangesAsync();

        return new NoteDto
        {
            Id = createdNote.Id,
            Title = createdNote.Title,
            CreatedAt = createdNote.CreatedAt,
            ExpiryDate = createdNote.ExpiryDate
        };
    }
}
