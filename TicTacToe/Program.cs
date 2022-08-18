using TicTacToe.GameControllers;
using TicTacToe.Helpers;
namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            UICultureSettings.SetInitialUICulture();
            UICultureSettings.SetUICulture();
            while (true)
            {
                TicTacToeGame myGame = new();
                myGame.LaunchGame();
                myGame.GenerateJSONReports();
                myGame.ConfirmGameRepeat();
            }
        }
    }
}

