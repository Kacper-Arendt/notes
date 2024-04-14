using Microsoft.EntityFrameworkCore;

namespace noteApp.Models;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserAuthentication> UserAuthentications { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<TaskList> TaskList { get; set; }
    public DbSet<TaskItem> TaskItem { get; set; }


    public DatabaseContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default(CancellationToken)
    )
    {
        OnBeforeSaving();
        return (await base.SaveChangesAsync(acceptAllChangesOnSuccess,
            cancellationToken));
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries();
        var utcNow = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            // for entities that inherit from BaseEntity,
            // set UpdatedOn / CreatedOn appropriately
            if (entry.Entity is BaseEntity trackable)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        trackable.UpdatedOn = utcNow;

                        entry.Property("CreatedOn").IsModified = false;
                        break;

                    case EntityState.Added:
                        trackable.CreatedOn = utcNow;
                        trackable.UpdatedOn = utcNow;
                        break;
                }
            }
        }
    }
}