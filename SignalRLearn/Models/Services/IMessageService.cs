using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRLearn.Context;
using SignalRLearn.Models.Entities;

namespace SignalRLearn.Models.Services;

public interface IMessageService
{
    Task SaveChatMessage(Guid roomId, MessageDto dto);
    Task<List<MessageDto>> GetChatMessage(Guid roomId);
}

public class MessageService :IMessageService
{
    private readonly DataBaseContext _context;

    public MessageService(DataBaseContext context)
    {
        _context = context;
    }

    public async Task SaveChatMessage(Guid roomId, MessageDto dto)
    {
        var room = await _context.ChatRooms.SingleOrDefaultAsync(i => i.Id.Equals(roomId));
        if (room is null) return;
        var chatMessage = new ChatMessage()
        {
            ChatRoom = room,
            Message = dto.Message,
            Time = dto.Time,
            Sender = dto.Message
        };
        await _context.ChatMessages.AddAsync(chatMessage);
        _context.SaveChanges();
    }

    public async Task<List<MessageDto>> GetChatMessage(Guid roomId)
    {
        return await _context.ChatMessages.Where(i => i.ChatRoomId == roomId)
            .Select(p => new MessageDto()
            {
                Message = p.Message,
                Sender = p.Sender,
                Time = p.Time
            }).OrderBy(p => p.Time).ToListAsync();
    }
}


public class MessageDto
{
    public string Sender { get; set; }
    public string Message { get; set; }
    public DateTime Time { get; set; }
}