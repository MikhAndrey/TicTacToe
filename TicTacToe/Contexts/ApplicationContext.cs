using Microsoft.EntityFrameworkCore;
using TicTacToe.Entities;
using TicTacToe.GameControllers;

namespace TicTacToe.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<GameDataForDB> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=tictactoedb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDataForDB>().Property("Id").ValueGeneratedOnAdd();
        }
    }
}
