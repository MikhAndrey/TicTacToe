void PlayTicTacToe()
{
    while (true)
    {
        TicTacToeGame myGame = new TicTacToeGame();
        myGame.LaunchGame();
        ConsoleKey confirm_key;
        do
        {
            Console.WriteLine("Хотите ли сыграть ещё? (Y - да, N - нет)");
            confirm_key = Console.ReadKey().Key;
            Console.Clear();
            if (confirm_key == ConsoleKey.N)
                Environment.Exit(0);
        } while (confirm_key != ConsoleKey.Y);
    }    
}

PlayTicTacToe();




