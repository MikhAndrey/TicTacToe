using TicTacToe.Interfaces;
using TicTacToe.Entities;
using TicTacToe.Contexts;
using Microsoft.EntityFrameworkCore;
namespace TicTacToe.DBRepositories
{
    public class SQLPlayersRepository : IRepository<Player>
    {
        private PlayerContext _db;
        public SQLPlayersRepository()
        {
            _db = new();
        }
        public IEnumerable<Player> GetList()
        {
            return _db.Players;
        }
        public Player GetItem(int id)
        {
            return _db.Players.Find(id);
        }
        public void Add(Player player)
        {
            _db.Players.Add(player);
        }
        public void Update(Player player)
        {
            _db.Entry(player).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Player player = _db.Players.Find(id);
            if (player != null)
                _db.Players.Remove(player);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
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
