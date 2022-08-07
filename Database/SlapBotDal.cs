﻿using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class SlapBotDal : DbContext
{
    public DbSet<Game> Games { get; private set; } = null!;

    public DbSet<User> Users { get; private set; } = null!;

    public DbSet<Slap> Slaps { get; private set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite(@"Data Source=SlapBotDB.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(
                game =>
                {
                    game.HasKey(g => g.Id);
                    game.HasMany(g => g.Users).WithMany(u => u.Games);
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