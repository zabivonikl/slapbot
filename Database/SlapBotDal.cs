using Database.Entities;
using MessengersClients.Types;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class SlapBotDal : DbContext
{
    public DbSet<Game> Games { get; set; }
    
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite(@"Data Source=SlapBotDB.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(
                game =>
                {
                    game.HasMany(g => g.Users).WithOne()
                        .OnDelete(DeleteBehavior.Cascade);
                    game.HasMany(g => g.Slaps).WithOne(s => s.Game)
                        .OnDelete(DeleteBehavior.Cascade);
                }
            );
        modelBuilder.Entity<Slap>(
                slap =>
                {
                    slap.HasKey("id");
                    slap.HasOne(s => s.From).WithMany();
                    slap.HasOne(s => s.To).WithMany();
                }
            );
    }
}