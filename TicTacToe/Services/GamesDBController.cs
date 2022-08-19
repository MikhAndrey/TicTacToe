using TicTacToe.Repositories.Interfaces;
using TicTacToe.Model.DBModels;
using TicTacToe.Repositories;
using TicTacToe.Model.ViewModel;
using TicTacToe.Resources;

namespace TicTacToe.Services
{
    public static class GamesDBController
    {
        public static IRepository<GameDataForDB> _gamesDB { get; private set; } = new GamesRepository();
        public static void UpdateGamesDB(DateTime gameStart, DateTime gameEnd, string userSymbols, Player[] players, int? winnerId)
        {
            GameDataForDB thisGameData;
            thisGameData = new(gameStart, gameEnd, userSymbols[0], userSymbols[1], players[0].Id, players[1].Id, winnerId);
            try
            {
                _gamesDB.Add(thisGameData);
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
