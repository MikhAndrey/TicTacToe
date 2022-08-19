using Microsoft.EntityFrameworkCore;
using TicTacToe.Model.ViewModel;
using TicTacToe.Model.DBModels;
using System.Configuration;

namespace TicTacToe.Repositories
{
    public class ApplicationContext : DbContext
    {
        public DbSet<GameDataForDB> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDataForDB>().Property("Id").ValueGeneratedOnAdd();
        }
    }
}
