namespace TicTacToe.GameControllers
{
    public class GameDataForDB
    {
        public int Id { get; private set; }
        public DateTime GameStart { get; private set; }
        public DateTime GameEnd { get; private  set; }
        public char FirstPlayerSymbol { get; private set; }
        public char SecondPlayerSymbol { get; private set; }
        public int FirstPlayerId { get; private set; }
        public int SecondPlayerId { get; private set; }
        public int? WinnerId { get; private set; }
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
