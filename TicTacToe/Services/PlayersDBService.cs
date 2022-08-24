using TicTacToe.Repositories.Interfaces;
using TicTacToe.Model.ViewModel;
using TicTacToe.Repositories;
using TicTacToe.Resources;
using TicTacToe.Utils;

namespace TicTacToe.Services
{
    /// <summary>
    ///   This service provides opportunity to interact with players database. 
    /// </summary>
    public static class PlayersDBService
    {
       
        /// <summary>The games database. This static property is set to new() once when app starts</summary>
        private static IRepository<Player> _playersDB = new PlayersRepository();

        /// <summary>Updates the players database by adding a new player if there is no player with following ID or updating player's info otherwise.</summary>
        /// <param name="playersCount">Current game's players count.</param>
        /// <param name="players">The current game players.</param>
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
                    ConsoleHandler.WriteLine(Messages.PlayerConnectionErrorMessage);
                    return;
                }
            }
        }
    }
}
