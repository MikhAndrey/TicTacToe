using TicTacToe.GameControllers;
using TicTacToe.Helpers;
namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            UICultureSettings.SetUICulture();
            while (true)
            {
                TicTacToeGame myGame = new();
                myGame.Play();
            }
        }
    }
}

