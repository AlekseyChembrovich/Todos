using MediatR;
using Todos.Domain.Abstractions;
using Todos.Domain.Aggregates.Note;

namespace Todos.Application.Behaviors.Note.Commands;

public record UpdateNoteCommand(Guid NoteId, string Title, DateTime ExpiryDate) : IRequest<bool>;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateNoteCommandHandler(INoteRepository noteRepository, IUnitOfWork unitOfWork)
    {
        _noteRepository = noteRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<bool> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var isUpdated = await _noteRepository.UpdateAsync(
            request.NoteId, request.Title, request.ExpiryDate, cancellationToken);

        _ = await _unitOfWork.SaveChangesAsync();

        return isUpdated;
    }
}
