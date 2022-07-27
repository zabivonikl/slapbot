using Database.Entities;
using Microsoft.EntityFrameworkCore;
using MessengersClients.Types;

namespace Database;

public class SlapBotDal : DbContext
{
    public DbSet<Chat> Users { get; set; }

    public DbSet<Game> Games { get; set; }

    public DbSet<Invitation> Invitations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite(@"Data Source=SlapBotDB.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(
                chat =>
                {
                    chat.HasKey(c => c.ChatId);
                    chat.Property(c => c.Username);
                }
            );
        modelBuilder.Entity<Game>(
                game =>
                {
                    game.HasKey("id");
                    game.HasMany(g => g.Users).WithOne();
                    game.HasMany(g => g.Slaps).WithOne(s => s.Game);
                }
            );
        modelBuilder.Entity<Invitation>(
                invitation =>
                {
                    invitation.HasKey("id");
                    invitation.HasOne(i => i.Chat).WithMany();
                    invitation.HasOne(i => i.Game).WithMany();
                }
            );
        modelBuilder.Entity<Slap>(
                slap =>
                {
                    slap.HasKey("id");
                    slap.Property(s => s.Time);
                    slap.HasOne(s => s.From).WithMany();
                    slap.HasOne(s => s.To).WithMany();
                }
            );
    }
}