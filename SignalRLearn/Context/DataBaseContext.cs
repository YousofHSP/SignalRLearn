using Microsoft.EntityFrameworkCore;
using SignalRLearn.Models.Entities;

namespace SignalRLearn.Context;

public class DataBaseContext: DbContext
{
    public DataBaseContext(DbContextOptions options): base(options)
    {
        
    }
    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<User> Users { get; set; }
}