using MediatR;
using Todos.Application.Common.Dto;
using Todos.Domain.Aggregates.Note;

namespace Todos.Application.Behaviors.Note.Queries;

public record GetNotesQuery() : IRequest<IEnumerable<NoteDto>>;

public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, IEnumerable<NoteDto>>
{
    private readonly INoteRepository _noteRepository;

    public GetNotesQueryHandler(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<IEnumerable<NoteDto>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        var notes = await _noteRepository.GetListAsync(cancellationToken);

        return notes.Select(note => new NoteDto
        {
            Id = note.Id,
            Title = note.Title,
            CreatedAt = note.CreatedAt,
            ExpiryDate = note.ExpiryDate
        });
    }
}
