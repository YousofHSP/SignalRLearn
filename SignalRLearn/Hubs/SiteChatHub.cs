using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;
using SignalRLearn.Models.Services;

namespace SignalRLearn.Hubs;

public class SiteChatHub(IChatRoomService service, IMessageService messageService) : Hub
{
    // OnConnectedAsync() --> when client connected to hub
    // OnDisconnectedAsync() --> when client disconnected from hub

    public async Task SendNewMessage(string sender, string message)
    {
        var roomId = await service.GetChatRoomForConnection(Context.ConnectionId);
        var messageDto = new MessageDto()
        {
            Message = message,
            Sender = sender,
            Time = DateTime.Now
        };
        await messageService.SaveChatMessage(roomId, messageDto);
        await Clients.Groups(roomId.ToString()).SendAsync("receiveNewMessage", sender, message, DateTime.Now.ToShortDateString());
    }
    
    public override async Task OnConnectedAsync()
    {
        if (Context.User.Identity.IsAuthenticated)
        {
            await base.OnConnectedAsync();
            return;
        }
        var roomId = await service.CreateChatRoom(Context.ConnectionId);
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        await Clients.Caller.SendAsync("receiveNewMessage", "support", "hi. how can we help you?", DateTime.Now.ToShortDateString());
        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}