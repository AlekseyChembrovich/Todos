using MediatR;
using Todos.Domain.Abstractions;
using Todos.Domain.Aggregates.Note;

namespace Todos.Application.Behaviors.Note.Commands;

public record RemoveNoteCommand(Guid NoteId) : IRequest<bool>;

public class RemoveNoteCommandHandler : IRequestHandler<RemoveNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveNoteCommandHandler(INoteRepository noteRepository, IUnitOfWork unitOfWork)
    {
        _noteRepository = noteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RemoveNoteCommand request, CancellationToken cancellationToken)
    {
        var isRemoved = await _noteRepository.RemoveAsync(request.NoteId, cancellationToken);

        _ = await _unitOfWork.SaveChangesAsync();

        return isRemoved;
    }
}
