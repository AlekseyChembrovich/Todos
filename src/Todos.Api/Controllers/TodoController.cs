using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Todos.Application.Behaviors.Todo.Queries;
using Todos.Application.Behaviors.Todo.Commands;

namespace Todos.Api.Controllers;

[ApiController]
[Route("api/todos")]
[Authorize(Roles = "User")]
public class TodoController : Controller
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        var todos = await _mediator.Send(new GetTodosQuery());

        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodos(string id)
    {
        var todo = await _mediator.Send(new GetTodoQuery
        {
            Id = id
        });

        return todo is not null ? Ok(todo) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo(CreateTodoCommand createTodoCommand)
    {
        var todoDto = await _mediator.Send(createTodoCommand);

        return Created($"/todos/{todoDto.Id}", todoDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTodo(UpdateTodoCommand updateTodoCommand)
    {
        await _mediator.Send(updateTodoCommand);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveTodo(string id)
    {
        await _mediator.Send(new RemoveTodoCommand(id));

        return NoContent();
    }
}
