using HseFootball.Models;
using Microsoft.EntityFrameworkCore;

namespace HseFootball.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<News> News { get; set; } = null!;
        public ApplicationContext()
        {
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=15432;Database=hse-football;Username=mal91r;Password=123456");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasOne(u => u.Team)
                .WithMany(c => c.Players)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
