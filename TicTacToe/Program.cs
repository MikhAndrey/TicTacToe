﻿using TicTacToe.Launchers;

namespace TicTacToe
{
    class Program
    {
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

