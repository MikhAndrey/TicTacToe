﻿using TicTacToe.Repositories.Interfaces;
using TicTacToe.Model.DBModels;

namespace TicTacToe.Repositories
{
    /// <summary>
    ///   This class implement methods from IRepository interface to interacty exactly with games DB
    /// </summary>
    public class GamesRepository : IRepository<GameDataForDB>
    {
        private ApplicationContext _db;

        /// <summary>
        ///   When initializing this class object, we just create application context (in fact, make sure that DB exists, otherwise create the new one)
        /// </summary>
        public GamesRepository() => _db = new();

        /// <summary>Gets all games DB records.</summary>
        /// <returns>List with all games DB records</returns>
        public List<GameDataForDB> GetList() => _db.Games.ToList();

        /// <summary>Gets the game from DB by its ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Game with required ID.</returns>
        public GameDataForDB? GetItem(int id) => _db.Games.Find(id);

        /// <summary>Adds the specified game.</summary>
        /// <param name="game">Specified game we want to add</param>
        public void Add(GameDataForDB game) => _db.Games.Add(game);

        /// <summary>Updates the info about the specified game in DB.</summary>
        /// <param name="game">Specified game which info we want to update</param>
        public void Update(GameDataForDB game) => _db.Update(game);

        /// <summary>Deletes the game with the specified identifier.</summary>
        /// <param name="id">The identifier of game we want to delete.</param>
        public void Delete(int id)
        {
            GameDataForDB? game = _db.Games.Find(id);
            if (game != null)       
                _db.Games.Remove(game);
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

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged GamesRepository resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
