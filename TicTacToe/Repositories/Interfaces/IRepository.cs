namespace TicTacToe.Repositories.Interfaces
{
    /// <summary>
    ///   This interface provides all necessary operations with data from databases.
    /// </summary>
    /// <typeparam name="T">Specifies type of data that we want to store in DB</typeparam>
    public interface IRepository<T> : IDisposable
        where T : class
    {
        /// <summary>Gets all DB records.</summary>
        /// <returns>
        ///   List with all DB records
        /// </returns>
        List<T> GetList();
        /// <summary>Gets the item from DB by its ID.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   Item with required ID.
        /// </returns>
        T? GetItem(int id);
        /// <summary>Adds the specified item.</summary>
        /// <param name="item">The item we want to add.</param>
        void Add(T item);
        /// <summary>Updates the info about the specified item in DB.</summary>
        /// <param name="item">The item which info we want to update.</param>
        void Update(T item);
        /// <summary>Deletes the item with the specified identifier.</summary>
        /// <param name="id">The identifier of item we want to delete.</param>
        void Delete(int id);
        /// <summary>Saves DB changes.</summary>
        void Save();
    }
}
