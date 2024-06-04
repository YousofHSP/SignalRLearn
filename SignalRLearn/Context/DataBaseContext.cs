using Microsoft.EntityFrameworkCore;
using SignalRLearn.Models.Entities;

namespace SignalRLearn.Context;

public class DataBaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
}