using TicTacToe.Interfaces;
using TicTacToe.Contexts;
using TicTacToe.DBEntities;

namespace TicTacToe.DBRepositories
{
    public class SQLGamesRepository : IRepository<GameDataForDB>
    {
        private ApplicationContext _db;
        public SQLGamesRepository() => _db = new();
        public List<GameDataForDB> GetList() => _db.Games.ToList();
        public GameDataForDB? GetItem(int id) => _db.Games.Find(id);
        public void Add(GameDataForDB game) => _db.Games.Add(game);
        public void Update(GameDataForDB game) => _db.Update(game);
        public void Delete(int id)
        {
            GameDataForDB? game = _db.Games.Find(id);
            if (game != null)
                _db.Games.Remove(game);
        }
        public void Save() => _db.SaveChanges();
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
                _db.Dispose();
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
