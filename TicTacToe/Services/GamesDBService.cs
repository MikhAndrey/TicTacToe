using TicTacToe.Repositories.Interfaces;
using TicTacToe.Model.DBModels;
using TicTacToe.Repositories;
using TicTacToe.Model.ViewModel;
using TicTacToe.Resources;

namespace TicTacToe.Services
{
    /// <summary>
    ///   This service provides opportunity to interact with games database. So far we only need to add current game results to DB.
    /// </summary>
    public static class GamesDBService
    {
        /// <summary>Gets the games database. This static property is set to new() once when app starts</summary>
        public static IRepository<GameDataForDB> _gamesDB { get; private set; } = new GamesRepository();
        /// <summary>Updates the games database by adding current game info.</summary>
        /// <param name="gameStart">Current game start date.</param>
        /// <param name="gameEnd">Current game end date.</param>
        /// <param name="userSymbols">The current game's player symbols.</param>
        /// <param name="players">The current game's players. So far only Id is stored in DB.</param>
        /// <param name="winnerId">The winner identifier.</param>
        public static void UpdateGamesDB(DateTime gameStart, DateTime gameEnd, string userSymbols, Player[] players, int? winnerId)
        {
            GameDataForDB thisGameData;
            thisGameData = new(gameStart, gameEnd, userSymbols[0], userSymbols[1], players[0].Id, players[1].Id, winnerId);
            try
            {
                _gamesDB.Add(thisGameData);     //Just adding current game data to database and saving changes
                _gamesDB.Save();
            }
            catch
            {
                Console.WriteLine(Messages.GameSetConnectionErrorMessage);
                return;
            }
        }
    }
}
