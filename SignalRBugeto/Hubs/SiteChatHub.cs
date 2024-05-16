using Microsoft.AspNetCore.SignalR;

namespace SignalRBugeto.Hubs;

public class SiteChatHub: Hub
{
    // OnConnectedAsync() --> when client connected to hub
    // OnDisconnectedAsync() --> when client disconnected from hub

    public async Task SendNewMessage(string sender, string message)
    {
        await Clients.All.SendAsync("getNewMessage", sender, message, DateTime.Now);
    }
    
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}