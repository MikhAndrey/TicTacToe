namespace TicTacToe.Model.DBModels
{
    /// <summary>
    ///  This class provides required game info separation to be saved further to database 
    /// </summary>
    public class GameDataForDB
    {
        /// <summary>The game's identifier. It has autoincrement field in game DB</summary>
        public int Id { get; private set; }
        /// <summary>The game start time</summary>
        public DateTime GameStart { get; private set; }
        /// <summary>The game end time</summary>
        public DateTime GameEnd { get; private set; }
        /// <summary>The first player symbol</summary>
        public char FirstPlayerSymbol { get; private set; }
        /// <summary>The second player symbol</summary>
        public char SecondPlayerSymbol { get; private set; }
        /// <summary>The first player ID</summary>
        public int FirstPlayerId { get; private set; }
        /// <summary>The second player ID</summary>
        public int SecondPlayerId { get; private set; }
        /// <summary>The winner id or null if the game was drawn</summary>
        public int? WinnerId { get; private set; }
        /// <summary>Initialazing the game DB data object by params corresponding to class fields</summary>
        public GameDataForDB(DateTime gameStart,
            DateTime gameEnd,
            char firstPlayerSymbol,
            char secondPlayerSymbol,
            int firstPlayerId,
            int secondPlayerId,
            int? winnerId)
        {
            GameStart = gameStart;
            GameEnd = gameEnd;
            FirstPlayerSymbol = firstPlayerSymbol;
            SecondPlayerSymbol = secondPlayerSymbol;
            FirstPlayerId = firstPlayerId;
            SecondPlayerId = secondPlayerId;
            WinnerId = winnerId;
        }
    }
}
