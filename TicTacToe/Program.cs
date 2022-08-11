using GameProcessTool;
class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            TicTacToeGame myGame = new();
            myGame.LaunchGame();
            myGame.ConfirmGameRepeat();
        }
    }
}

