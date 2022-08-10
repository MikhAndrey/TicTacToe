public class TicTacToeGame
{
    private char[,] gameFieldSymbols = new char[3, 3];

    private string[] userNames = new string[2];

    private char[] cellLabels = { 'x', 'o' };

    private int userNumberForTurn = 0;

    private int successfulTurnsCount = 0;

    private int rowNumberForTurn;

    private int columnNumberForTurn;

    public TicTacToeGame()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                gameFieldSymbols[i, j] = '.';
        SetUserNames();
    }
    private void DrawGameField()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
                Console.Write($"{gameFieldSymbols[i, j]}|");
            Console.WriteLine(gameFieldSymbols[i, 2]);
            if (i != 2)
                Console.WriteLine("------");
        }
    }
    private void SetUserNames()
    {
        int nameLength;
        bool wrongNameLength;
        for (int i = 0; i < 2; i++)
        {
            do
            {
                Console.Clear();
                Console.Write($"Игрок {i + 1}, введите своё имя: ");
                userNames[i] = Console.ReadLine();
                nameLength = userNames[i].Length;
                wrongNameLength = nameLength > 25 || nameLength == 0;
                if (wrongNameLength)
                {
                    Console.Write("Введенное имя слишком длинное или слишком короткое. Длина вашего имени не должна быть больше 25 символов");
                    Console.ReadKey();
                }
            } while (wrongNameLength);
        }
    }
    private void WriteUserNumberForTurn()
    {
        Console.WriteLine($"Ход игрока {userNames[userNumberForTurn]}");
    }

    private bool IsUserInputProper(string input)
    {
        char[] properValues = { '0', '1', '2' };
        bool isInputProper = input.Length == 3 && input[1] == ' ' && properValues.Contains(input[0]) && properValues.Contains(input[2]);
        if (!isInputProper)
            return false;
        else
        {
            rowNumberForTurn = int.Parse(input.Substring(0, 1));
            columnNumberForTurn = int.Parse(input.Substring(2, 1));
            if (gameFieldSymbols[rowNumberForTurn, columnNumberForTurn] == '.')
                return true;
            else
                return false;
        }
    }
    private void PerformOneUserTurn()
    {
        string userInput;
        bool wrongFieldNumberInput;
        int attemptsLeftCount = 3;
        do
        {
            Console.WriteLine($"Игрок {userNames[userNumberForTurn]}, введите через пробел номер строки и номер столбца клетки, которую вы бы хотели занять");
            userInput = Console.ReadLine();
            wrongFieldNumberInput = !IsUserInputProper(userInput);
            if (wrongFieldNumberInput)
            {
                attemptsLeftCount--;
                Console.WriteLine($"Вы ввели данные в неверном формате или клетка, которую вы хотели занять, уже занята. Осталось {attemptsLeftCount} попыток, прежде чем ход перейдет к сопернику");
            }
        } while (wrongFieldNumberInput && attemptsLeftCount != 0);
        if (attemptsLeftCount != 0)
        {
            gameFieldSymbols[rowNumberForTurn, columnNumberForTurn] = cellLabels[userNumberForTurn];
            successfulTurnsCount++;
        }
    }

    private bool IsSomeoneWon()
    {
        for (int i = 0; i < 3; i++)
        {
            char firstCharInRow = gameFieldSymbols[i, 0];
            if (firstCharInRow == '.')
                continue;
            for (int j = 1; j < 3; j++)
            {
                if (gameFieldSymbols[i, j] != firstCharInRow)
                    break;
                if (j == 2)
                    return true;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            char firstCharInColumn = gameFieldSymbols[0, j];
            if (firstCharInColumn == '.')
                continue;
            for (int i = 1; i < 3; i++)
            {
                if (gameFieldSymbols[i, j] != firstCharInColumn)
                    break;
                if (i == 2)
                    return true;
            }
        }
        char firstCharInMainDiagonal = gameFieldSymbols[0, 0];
        if (firstCharInMainDiagonal != '.')
            for (int i = 1; i < 3; i++)
            {
                if (gameFieldSymbols[i, i] != firstCharInMainDiagonal)
                    break;
                if (i == 2)
                    return true;
            }
        char firstCharInSecondaryDiagonal = gameFieldSymbols[0, 2];
        if (firstCharInSecondaryDiagonal != '.')
            for (int i = 1; i < 3; i++)
            {
                if (gameFieldSymbols[i, 2 - i] != firstCharInSecondaryDiagonal)
                    break;
                if (i == 2)
                    return true;
            }
        return false;
    }
    public void LaunchGame()
    {
        while (successfulTurnsCount < 9)
        {
            Console.Clear();
            WriteUserNumberForTurn();
            DrawGameField();
            PerformOneUserTurn();
            if (IsSomeoneWon())
            {
                Console.Clear();
                DrawGameField();
                Console.WriteLine($"Победил игрок {userNames[userNumberForTurn]} !");
                return;
            }
            userNumberForTurn = 1 - userNumberForTurn;
        }
        Console.Clear();
        DrawGameField();
        Console.WriteLine("Победила дружба!");
    }
};