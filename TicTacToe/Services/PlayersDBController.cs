using TicTacToe.Repositories.Interfaces;
using TicTacToe.Model.ViewModel;
using TicTacToe.Repositories;
using TicTacToe.Resources;

namespace TicTacToe.Services
{
    public static class PlayersDBController
    {
        private static IRepository<Player> _playersDB = new PlayersRepository();
        public static void UpdatePlayersDB(int playersCount, Player[] players)
        {
            for (int i = 0; i < playersCount; i++)
            {
                try
                {
                    Player? possiblePlayerFromDB = _playersDB.GetItem(players[i].Id);
                    if (possiblePlayerFromDB == null)
                        _playersDB.Add(players[i]);
                    else
                    {
                        if (possiblePlayerFromDB.Name != players[i].Name)
                            possiblePlayerFromDB.Name = players[i].Name;
                        if (possiblePlayerFromDB.Age != players[i].Age)
                            possiblePlayerFromDB.Age = players[i].Age;
                    }
                    _playersDB.Save();
                }
                catch
                {
                    Console.WriteLine(Messages.PlayerConnectionErrorMessage);
                    return;
                }
            }
        }
    }
}
