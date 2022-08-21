using TicTacToe.Launchers;

namespace TicTacToe
{
    /// <summary>
    ///   Class which in fact manages the app.
    /// </summary>
    class Program
    {
        /// <summary>Defines the entry point of the application.</summary>
        static void Main(string[] args)
        {
            while (true)    //Play a new TicTacToe game until user exits a console
            {
                TicTacToeGame myGame = new();
                myGame.Play();
            }
        }
    }
}

