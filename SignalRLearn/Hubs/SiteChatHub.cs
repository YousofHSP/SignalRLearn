using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;
using SignalRLearn.Models.Services;

namespace SignalRLearn.Hubs;

public class SiteChatHub: Hub
{
    // OnConnectedAsync() --> when client connected to hub
    // OnDisconnectedAsync() --> when client disconnected from hub

    private readonly IChatRoomService _service;

    public SiteChatHub(IChatRoomService service)
    {
        _service = service;
    }

    public async Task SendNewMessage(string sender, string message)
    {
        var roomId = await _service.GetChatRoomForConnection(Context.ConnectionId);
        await Clients.Groups(roomId.ToString()).SendAsync("receiveNewMessage", sender, message, DateTime.Now.ToShortDateString());
    }
    
    public override async Task OnConnectedAsync()
    {
        var roomId = await _service.CreateChatRoom(Context.ConnectionId);
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        await Clients.Caller.SendAsync("receiveNewMessage", "support", "hi. how can we help you?", DateTime.Now.ToShortDateString());
        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}