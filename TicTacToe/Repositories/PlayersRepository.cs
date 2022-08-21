using TicTacToe.Model.ViewModel;
using TicTacToe.Repositories.Interfaces;

namespace TicTacToe.Repositories
{
    /// <summary>
    ///   This class implement methods from IRepository interface to interacty exactly with players DB
    /// </summary>
    public class PlayersRepository : IRepository<Player>
    {
        private ApplicationContext _db;
        /// <summary>
        ///   When initializing this class object, we just create application context (in fact, make sure that DB exists, otherwise create the new one)
        /// </summary>
        public PlayersRepository() => _db = new();
        /// <summary>Gets all DB records.</summary>
        /// <returns>List with all DB records</returns>
        public List<Player> GetList() => _db.Players.ToList();
        /// <summary>Gets the player from DB by its ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Player with required ID.</returns>
        public Player? GetItem(int id) => _db.Players.Find(id);
        /// <summary>Adds the specified player.</summary>
        /// <param name="game">Specified player we want to add</param>
        public void Add(Player player) => _db.Players.Add(player);
        /// <summary>Updates the info about the specified player in DB.</summary>
        /// <param name="game">Specified player which info we want to update</param>
        public void Update(Player player) => _db.Update(player);
        /// <summary>Deletes the player with the specified identifier.</summary>
        /// <param name="id">The identifier of player we want to delete.</param>
        public void Delete(int id)
        {
            Player? player = _db.Players.Find(id);
            if (player != null)     //Checking if the player with required ID exists. If yes, remove it
                _db.Players.Remove(player);
        }
        /// <summary>Saves DB changes.</summary>
        public void Save() => _db.SaveChanges();
        /// <summary>The disposed boolean param checks if the object was disposed</summary>
        private bool disposed = false;
        /// <summary>Releases unmanaged and - optionally - managed resources.</summary>
        /// <param name="disposing">
        ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        public virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)  
                _db.Dispose();
            disposed = true;
        }
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged PlayersRepository resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
