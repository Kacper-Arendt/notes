using Microsoft.EntityFrameworkCore;

namespace note.Models;

public class DatabaseContext: DbContext
{
    public DbSet<User>Users { get; set; }
    public DbSet<UserAuthentication> UserAuthentications { get; set; }
    public DbSet<Note> Notes { get; set; }

    
    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}