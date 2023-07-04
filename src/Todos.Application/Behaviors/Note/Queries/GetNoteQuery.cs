using MediatR;
using Todos.Application.Common.Dto;
using Todos.Domain.Aggregates.Note;

namespace Todos.Application.Behaviors.Note.Queries;

public record GetNoteQuery(Guid NoteId) : IRequest<NoteDto>;

public class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, NoteDto>
{
    private readonly INoteRepository _noteRepository;

    public GetNoteQueryHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<NoteDto> Handle(GetNoteQuery request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.NoteId, cancellationToken);

        return new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            CreatedAt = note.CreatedAt,
            ExpiryDate = note.ExpiryDate
        };
    }
}
