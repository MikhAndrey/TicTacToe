using TicTacToe.Launchers;

namespace TicTacToe
{
    /// <summary>
    ///   Class which manages app work.
    /// </summary>
    class Program
    {
        /// <summary>Defines the entry point of the application.</summary>
        static void Main(string[] args)
        {
            while (true)    
            {
                TicTacToeGame myGame = new();
                myGame.Play();
            }
        }
    }
}

