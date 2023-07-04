using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todos.Application.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Todos.Application.Behaviors.Note.Queries;
using Todos.Application.Behaviors.Note.Commands;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/notes")]
// [Authorize(Roles = "User")]
public class NoteController : Controller
{
    private readonly IMediator _mediator;
    
    public NoteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var query = new GetNotesQuery();
        var noteDtos = await _mediator.Send(query);

        return Ok(noteDtos);
    }

    [HttpGet("{noteId}")]
    public async Task<IActionResult> GetById(Guid noteId)
    {
        var query = new GetNoteQuery(noteId);
        var noteDto = await _mediator.Send(query);

        return noteDto is not null ? Ok(noteDto) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(NoteToCreateDto noteToCreate)
    {
        var command = new CreateNoteCommand(noteToCreate.Title, noteToCreate.ExpiryDate);
        var noteDto = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { noteId = noteDto.Id }, noteDto);
    }

    [HttpDelete("{noteId}")]
    public async Task<IActionResult> Remove(Guid noteId)
    {
        var command = new RemoveNoteCommand(noteId);
        var isRemoved = await _mediator.Send(command);

        return isRemoved ? NoContent() : NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> Update(NoteToUpdateDto noteToUpdate)
    {
        var command = new UpdateNoteCommand(noteToUpdate.Id, noteToUpdate.Title, noteToUpdate.ExpiryDate);
        var isUpdated = await _mediator.Send(command);

        return isUpdated ? NoContent() : NotFound();
    }
}
