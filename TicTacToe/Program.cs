void PlayTicTacToe()
{
    while (true)
    {
        TicTacToeGame myGame = new();
        myGame.LaunchGame();
        myGame.ConfirmGameRepeat();
    }    
}

PlayTicTacToe();




