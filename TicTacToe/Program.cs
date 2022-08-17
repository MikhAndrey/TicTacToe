namespace TicTacToe;
using TicTacToe.GameControllers;
class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            TicTacToeGame myGame = new();
            myGame.LaunchGame();
            myGame.GenerateJSONReports();
            myGame.ConfirmGameRepeat();
        }
    }
}

