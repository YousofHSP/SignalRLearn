using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRLearn.Models.Services;

namespace SignalRLearn.Hubs;

[Authorize]
public class SupportHub: Hub
{
    private readonly IChatRoomService _service;

    public SupportHub(IChatRoomService service)
    {
        _service = service;
    }

    public override async Task OnConnectedAsync()
    {
        var rooms = await _service.GetAllRooms();
        await Clients.Caller.SendAsync("GetRooms", rooms);
        await base.OnConnectedAsync();
    }
}