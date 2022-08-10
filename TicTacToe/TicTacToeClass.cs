public class TicTacToeGame
{
    private PlayersData playersInfo;

    private GameConstants gameConstants = new();

    private char[,] gameFieldSymbols;

    private int playersCount;

    private int userNumberForTurn = 0;

    private int successfulTurnsCount = 0;

    private int rowNumberForTurn;

    private int columnNumberForTurn;

    public TicTacToeGame(int playersCount = 2)
    {
        this.playersCount = playersCount;
        playersInfo = new(playersCount);
        gameFieldSymbols = new char[gameConstants.GameFieldSize, gameConstants.GameFieldSize];
        for (int i = 0; i < gameConstants.GameFieldSize; i++)
            for (int j = 0; j < gameConstants.GameFieldSize; j++)
                gameFieldSymbols[i, j] = '.';
        SetUserNames();
        SetUserSymbols();
    }
    public void LaunchGame()
    {
        while (successfulTurnsCount < gameConstants.GameFieldSize * gameConstants.GameFieldSize)
        {
            WriteUserNumberForTurn();
            DrawGameField();
            PerformOneUserTurn();
            if (IsSomeoneWon())
            {
                DrawGameField();
                Console.WriteLine($"Player {playersInfo.GetPlayerName(userNumberForTurn)} won!");
                return;
            }
            userNumberForTurn = userNumberForTurn == playersCount - 1 ? 0 : userNumberForTurn + 1;
        }
        DrawGameField();
        Console.WriteLine("Draw!");
    }
    public void ConfirmGameRepeat()
    {
        ConsoleKey confirmKey;
        do
        {
            Console.WriteLine("Do you want to play again? (Enter - yes, any other key - no)");
            confirmKey = Console.ReadKey().Key;
            if (confirmKey != ConsoleKey.Enter)
                Environment.Exit(0);
        } while (confirmKey != ConsoleKey.Enter);
    }
    private void DrawGameField()
    {
        for (int i = 0; i < gameConstants.GameFieldSize; i++)
        {
            for (int j = 0; j < gameConstants.GameFieldSize - 1; j++)
                Console.Write($"{gameFieldSymbols[i, j]}|");
            Console.WriteLine(gameFieldSymbols[i, gameConstants.GameFieldSize - 1]);
            if (i != gameConstants.GameFieldSize - 1)
                Console.WriteLine("------");
        }
    }
    private void SetUserSymbols()
    {
        for (int i = 0; i < playersCount; i++)
            playersInfo.SetPlayerSymbol(gameConstants.TicTacToeSymbols[i], i);
    }
    private void SetUserNames()
    {
        int nameLength;
        bool wrongNameLength;
        string possiblePlayerName;
        for (int i = 0; i < playersCount; i++)
        {
            do
            {
                Console.WriteLine($"Player {i + 1}, enter your name: ");
                possiblePlayerName = Console.ReadLine();
                nameLength = possiblePlayerName.Length;
                wrongNameLength = nameLength > gameConstants.MaxAllowedNameLength || nameLength == 0;
                if (wrongNameLength)
                {
                    Console.WriteLine($"Your name is too short or too long. Your name can't be longer than {gameConstants.MaxAllowedNameLength} symbols");
                    Console.ReadKey();
                }
                else
                    playersInfo.SetPlayerName(possiblePlayerName, i);
            } while (wrongNameLength);
        }
    }
    private void WriteUserNumberForTurn()
    {
        Console.WriteLine($"Player {playersInfo.GetPlayerName(userNumberForTurn)}'s turn");
    }

    private bool IsUserInputProper(string input)
    {
        string[] properValues = new string[gameConstants.GameFieldSize];
        for (int i = 0; i < gameConstants.GameFieldSize; i++)
            properValues[i] = (i + 1).ToString();
        input = input.Trim();
        int firstSpaceIndex = input.IndexOf(' ');
        if (firstSpaceIndex < 0)
            return false;
        int lastSpaceIndex = input.LastIndexOf(' ');
        string possibleStringRowNumber = input.Substring(0, firstSpaceIndex);
        string possibleStringColumnNumber = input.Substring(lastSpaceIndex + 1, input.Length - 1 - lastSpaceIndex);
        string stringBeforeColumnAndRowNumber = input.Substring(firstSpaceIndex, lastSpaceIndex - firstSpaceIndex + 1);
        bool isInputProper = string.IsNullOrWhiteSpace(stringBeforeColumnAndRowNumber) && properValues.Contains(possibleStringRowNumber) && properValues.Contains(possibleStringColumnNumber);
        if (!isInputProper)
            return false;
        else
        {
            rowNumberForTurn = int.Parse(possibleStringRowNumber);
            columnNumberForTurn = int.Parse(possibleStringColumnNumber);
            if (gameFieldSymbols[rowNumberForTurn - 1, columnNumberForTurn - 1] == '.')
                return true;
            else
                return false;
        }
    }
    private void PerformOneUserTurn()
    {
        string userInput;
        bool wrongFieldNumberInput;
        int attemptsLeftCount = gameConstants.MaxAllowedRetriesCount;
        do
        {
            Console.WriteLine($"Player {playersInfo.GetPlayerName(userNumberForTurn)}, enter the row number and column number of the cell you would like to occupy, separated by a space");
            userInput = Console.ReadLine();
            wrongFieldNumberInput = !IsUserInputProper(userInput);
            if (wrongFieldNumberInput)
            {
                attemptsLeftCount--;
                Console.WriteLine($"You entered the data in the wrong format or the cell you wanted to occupy is already occupied. There are {attemptsLeftCount} attempts left before the move goes to the opponent");
            }
        } while (wrongFieldNumberInput && attemptsLeftCount != 0);
        if (attemptsLeftCount != 0)
        {
            gameFieldSymbols[rowNumberForTurn - 1, columnNumberForTurn - 1] = playersInfo.GetPlayerSymbol(userNumberForTurn);
            successfulTurnsCount++;
        }
    }

    private bool IsSomeoneWon()
    {
        for (int i = 0; i < gameConstants.GameFieldSize; i++)
        {
            char firstCharInRow = gameFieldSymbols[i, 0];
            if (firstCharInRow == '.')
                continue;
            for (int j = 1; j < gameConstants.GameFieldSize; j++)
            {
                if (gameFieldSymbols[i, j] != firstCharInRow)
                    break;
                if (j == gameConstants.GameFieldSize - 1)
                    return true;
            }
        }
        for (int j = 0; j < gameConstants.GameFieldSize; j++)
        {
            char firstCharInColumn = gameFieldSymbols[0, j];
            if (firstCharInColumn == '.')
                continue;
            for (int i = 1; i < gameConstants.GameFieldSize; i++)
            {
                if (gameFieldSymbols[i, j] != firstCharInColumn)
                    break;
                if (i == gameConstants.GameFieldSize - 1)
                    return true;
            }
        }
        char firstCharInMainDiagonal = gameFieldSymbols[0, 0];
        if (firstCharInMainDiagonal != '.')
            for (int i = 1; i < gameConstants.GameFieldSize; i++)
            {
                if (gameFieldSymbols[i, i] != firstCharInMainDiagonal)
                    break;
                if (i == gameConstants.GameFieldSize - 1)
                    return true;
            }
        char firstCharInSecondaryDiagonal = gameFieldSymbols[0, gameConstants.GameFieldSize - 1];
        if (firstCharInSecondaryDiagonal != '.')
            for (int i = 1; i < gameConstants.GameFieldSize; i++)
            {
                if (gameFieldSymbols[i, gameConstants.GameFieldSize - 1 - i] != firstCharInSecondaryDiagonal)
                    break;
                if (i == gameConstants.GameFieldSize - 1)
                    return true;
            }
        return false;
    }
};