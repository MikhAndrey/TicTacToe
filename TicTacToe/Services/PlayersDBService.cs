using TicTacToe.Repositories.Interfaces;
using TicTacToe.Model.ViewModel;
using TicTacToe.Repositories;
using TicTacToe.Resources;

namespace TicTacToe.Services
{
    /// <summary>
    ///   This service provides opportunity to interact with players database. 
    ///   At this moment it helps to add player with new ID or update info about player with taken id.
    /// </summary>
    public static class PlayersDBService
    {
        /// <summary>The games database. This static property is set to new() once when app starts</summary>
        private static IRepository<Player> _playersDB = new PlayersRepository();
        /// <summary>Updates the players database by adding a new player if there is no player with following ID or updating player's info otherwise.</summary>
        /// <param name="playersCount">Current game's players count.</param>
        /// <param name="players">The players.</param>
        public static void UpdatePlayersDB(int playersCount, Player[] players)
        {
            for (int i = 0; i < playersCount; i++)
            {
                try
                {
                    Player? possiblePlayerFromDB = _playersDB.GetItem(players[i].Id);
                    if (possiblePlayerFromDB == null)       //The returned null by GetItem means that there is no player with such ID in DB
                        _playersDB.Add(players[i]);         // So just add the new one
                    else        //Otherwise we update info about existing player if there are any mismatches between current player's info and DB info
                    {                                  
                        if (possiblePlayerFromDB.Name != players[i].Name)
                            possiblePlayerFromDB.Name = players[i].Name;
                        if (possiblePlayerFromDB.Age != players[i].Age)
                            possiblePlayerFromDB.Age = players[i].Age;
                    }
                    _playersDB.Save();      //After all actions just saving changes 
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
