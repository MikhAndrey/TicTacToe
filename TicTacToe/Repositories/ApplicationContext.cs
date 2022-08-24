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
        
        /// <summary>Value of connection string's name attribute.</summary>
        public string _connectionStringName;

        /// <summary>Table with all games.</summary>
        public DbSet<GameDataForDB> Games { get; set; }

        /// <summary>Table with all players.</summary>
        public DbSet<Player> Players { get; set; }

        /// <summary>Initializes <see cref="ApplicationContext"/> object. 
        /// Makes some basic actions like to make sure that database is created and sets connection string property.</summary>
        public ApplicationContext(string connectionStringName = Constants.Constants.ConnectionStringName) {
            _connectionStringName = connectionStringName;   
            Database.EnsureCreated();
        }

        /// <summary>Sets some general config params of database we want to use.</summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString; 
            optionsBuilder.UseSqlServer(connectionString);
        }

        /// <summary>Specifies DB model params.</summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDataForDB>().Property("Id").ValueGeneratedOnAdd();
        }
    }
}
