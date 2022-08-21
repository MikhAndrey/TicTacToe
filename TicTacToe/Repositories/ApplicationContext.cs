using Microsoft.EntityFrameworkCore;
using TicTacToe.Model.ViewModel;
using TicTacToe.Model.DBModels;
using System.Configuration;

namespace TicTacToe.Repositories
{
    /// <summary>
    ///   This class provides database configuration params (i.e. database name, table names, field params etc.)
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>Table with all games.</summary>
        public DbSet<GameDataForDB> Games { get; set; }
        /// <summary>Table with all players.</summary>
        public DbSet<Player> Players { get; set; }
        /// <summary>We just need to make sure that database was created.</summary>
        public ApplicationContext() => Database.EnsureCreated();
        /// <summary>This method sets some general params of database we want to use. Connection string, for instance.</summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString; 
            optionsBuilder.UseSqlServer(connectionString);
        }
        /// <summary>This method specifies DB model params. For example, makes field Id in games DB autoincrement</summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDataForDB>().Property("Id").ValueGeneratedOnAdd();
        }
    }
}
