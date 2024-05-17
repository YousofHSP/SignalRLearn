﻿using Microsoft.AspNetCore.SignalR;

namespace SignalRLearn.Hubs;

public class SiteChatHub: Hub
{
    // OnConnectedAsync() --> when client connected to hub
    // OnDisconnectedAsync() --> when client disconnected from hub

    public async Task SendNewMessage(string sender, string message)
    {
        Console.WriteLine($"sender => {sender} , message => {message}");
        await Clients.All.SendAsync("receiveNewMessage", sender, message, DateTime.Now.ToShortDateString());
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