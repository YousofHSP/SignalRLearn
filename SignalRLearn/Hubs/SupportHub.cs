using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRLearn.Models.Services;

namespace SignalRLearn.Hubs;

[Authorize]
public class SupportHub: Hub
{
    private readonly IChatRoomService _service;
    private readonly IMessageService _messageService;

    public SupportHub(IChatRoomService service, IMessageService messageService)
    {
        _service = service;
        _messageService = messageService;
    }

    public override async Task OnConnectedAsync()
    {
        var rooms = await _service.GetAllRooms();
        await Clients.Caller.SendAsync("GetRooms", rooms);
        await base.OnConnectedAsync();
    }

    public async Task LoadMessage(Guid roomId)
    {
        var messages = await _messageService.GetChatMessage(roomId);
        await Clients.Caller.SendAsync("getNewMessage", messages);
    }
}