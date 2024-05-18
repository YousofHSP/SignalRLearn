using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRLearn.Models.Entities;

public class ChatMessage
{
    public Guid Id { get; set; }
    public string Sender { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime Time { get; set; } = DateTime.Now;
    public Guid ChatRoomId { get; set; }

    [ForeignKey(nameof(ChatRoomId))]
    public ChatRoom ChatRoom { get; set; }
}