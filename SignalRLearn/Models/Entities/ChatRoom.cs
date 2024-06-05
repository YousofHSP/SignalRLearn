namespace SignalRLearn.Models.Entities;

public class ChatRoom
{
    public Guid Id { get; set; }
    public string ConnectionId { get; set; } = null!;

    public ICollection<ChatMessage> ChatMessages { get; set; }
}