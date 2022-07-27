using Database.Entities;
using Microsoft.EntityFrameworkCore;
using User = MessengersClients.Types.Chat;

namespace Database;

public class SlapBotDal : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Game> Games { get; set; }

    public DbSet<Invitation> Invitations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("Data Source=SlapBotDB.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(u => u.ChatId);
        modelBuilder.Entity<User>()
            .Property(u => u.Username);

        modelBuilder.Entity<Game>()
            .HasKey("id");
        modelBuilder.Entity<Game>()
            .HasMany(g => g.Users)
            .WithOne();
        modelBuilder.Entity<Game>()
            .HasMany(g => g.Slaps)
            .WithOne(s => s.Game);

        modelBuilder.Entity<Invitation>()
            .HasKey("id");
        modelBuilder.Entity<Invitation>()
            .HasOne(i => i.User)
            .WithOne();
        modelBuilder.Entity<Invitation>()
            .HasOne(i => i.Game)
            .WithOne();
        modelBuilder.Entity<Invitation>()
            .HasOne(i => i.Game)
            .WithOne();

        modelBuilder.Entity<Slap>()
            .HasKey("id");
        modelBuilder.Entity<Slap>()
            .Property(s => s.Time);
        modelBuilder.Entity<Slap>()
            .HasOne(s => s.From)
            .WithOne();
        modelBuilder.Entity<Slap>()
            .HasOne(s => s.To)
            .WithOne();
    }
}