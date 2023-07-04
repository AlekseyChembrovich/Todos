using Microsoft.AspNetCore.SignalR;
using Todos.Application.Common.Dto;

namespace Todos.Api.Hubs;

public class NotesHub : Hub
{
    public async Task SendMessage(NoteDto noteDto)
    {
        await Clients.All.SendAsync("NoteChanged", noteDto);
    }
}
