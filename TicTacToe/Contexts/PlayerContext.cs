using Microsoft.EntityFrameworkCore;
using TicTacToe.Entities;

namespace TicTacToe.Contexts
{
    public class PlayerContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public PlayerContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=playersdb;Trusted_Connection=True;");
        }
    }
}
