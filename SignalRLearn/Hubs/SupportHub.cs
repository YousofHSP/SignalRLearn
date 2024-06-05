using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRLearn.Models.Services;

namespace SignalRLearn.Hubs;

[Authorize]
public class SupportHub(IChatRoomService service, IMessageService messageService, IHubContext<SiteChatHub> siteChatHub)
    : Hub
{
    private readonly IChatRoomService _service = service;
    private readonly IMessageService _messageService = messageService;
    private readonly IHubContext<SiteChatHub> _siteChatHub = siteChatHub;

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

    public async Task SendMessage(Guid roomId, string text)
    {
        var message = new MessageDto
        {
            Sender = Context.User.Identity.Name,
            Message = text,
            Time = DateTime.Now,
        };
        await _messageService.SaveChatMessage(roomId, message);
        await _siteChatHub.Clients.Group(roomId.ToString())
            .SendAsync("receiveNewMessage", message.Sender, message.Message, message.Time.ToShortTimeString());

    }
}