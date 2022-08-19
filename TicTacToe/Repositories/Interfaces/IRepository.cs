namespace TicTacToe.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        List<T> GetList();
        T? GetItem(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
