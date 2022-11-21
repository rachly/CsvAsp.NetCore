using Microsoft.EntityFrameworkCore;

using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDBContext : DbContext
    {
       
       // public DbSet<AppTeam> AppTeams { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlite("Data Source=Data\\app.db");
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<Player> AppUsers { get; set; }

        public DbSet<AppTeam> AppTeam { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppTeam>().HasMany(s=>s.Players).WithOne(s => s.team);
        }
      





    }
    }


