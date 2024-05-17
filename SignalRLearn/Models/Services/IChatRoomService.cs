using Microsoft.EntityFrameworkCore;
using SignalRLearn.Context;
using SignalRLearn.Models.Entities;

namespace SignalRLearn.Models.Services;

public interface IChatRoomService
{
    Task<Guid> CreateChatRoom(string connectionId);
    Task<Guid> GetChatRoomForConnection(string connectionId);
}

public class ChatRoomService : IChatRoomService
{
    private readonly DataBaseContext _context;

    public ChatRoomService(DataBaseContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateChatRoom(string connectionId)
    {
        var existChatRoom = await _context.ChatRooms.SingleOrDefaultAsync(i => i.ConnectionId == connectionId);

        if (existChatRoom is not null)
            return existChatRoom.Id;
        var model = new ChatRoom()
        {
            ConnectionId = connectionId,
            Id = Guid.NewGuid(),
        };
        await _context.ChatRooms.AddAsync(model);
        await _context.SaveChangesAsync();
        return model.Id;
    }

    public async Task<Guid> GetChatRoomForConnection(string connectionId)
    {
        var model = await _context.ChatRooms.SingleOrDefaultAsync(i => i.ConnectionId == connectionId);
        return model!.Id;
    }
}