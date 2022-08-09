public class TicTacToeGame
{
    private char[,] gameFieldSymbols = new char[3, 3];

    private string[] userNames = new string[2];

    private char[] cellLabels = { 'x', 'o' };

    private int userNumberForTurn = 0;

    private int successfulTurnsCount = 0;
    public TicTacToeGame()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                this.gameFieldSymbols[i, j] = '.';
        this.SetUserNames();
    }
    private void DrawGameField()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
                Console.Write($"{this.gameFieldSymbols[i, j]}|");
            Console.WriteLine(this.gameFieldSymbols[i, 2]);
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
                this.userNames[i] = Console.ReadLine();
                nameLength = this.userNames[i].Length;
                wrongNameLength = nameLength > 25 || nameLength == 0;
                if (wrongNameLength)
                {
                    Console.Write("Введенное имя слишком длинное или слишком короткое");
                    Console.ReadKey();
                }
            } while (wrongNameLength);
        }
    }
    private void WriteUserNumberForTurn()
    {
        Console.WriteLine($"Ход игрока {this.userNames[this.userNumberForTurn]}");
    }

    private bool IsUserInputProper(string input, ref int rowNum, ref int colNum)
    {
        char[] properValues = { '0', '1', '2' };
        bool isInputProper = input.Length == 3 && input[1] == ' ' && properValues.Contains(input[0]) && properValues.Contains(input[2]);
        if (!isInputProper)
            return false;
        else
        {
            rowNum = int.Parse(input.Substring(0, 1));
            colNum = int.Parse(input.Substring(2, 1));
            if (this.gameFieldSymbols[rowNum, colNum] == '.')
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
        int rowFillingNumber = new int();
        int columnFillingNumber = new int();
        do
        {
            Console.WriteLine($"Игрок {this.userNames[this.userNumberForTurn]}, введите через пробел номер строки и номер столбца клетки, которую вы бы хотели занять");
            userInput = Console.ReadLine();
            wrongFieldNumberInput = !this.IsUserInputProper(userInput, ref rowFillingNumber, ref columnFillingNumber);
            if (wrongFieldNumberInput)
            {
                attemptsLeftCount--;
                Console.WriteLine($"Вы ввели данные в неверном формате или клетка, которую вы хотели занять, уже занята. Осталось {attemptsLeftCount} попыток, прежде чем ход перейдет к сопернику");
            }
        } while (wrongFieldNumberInput && attemptsLeftCount != 0);
        if (attemptsLeftCount != 0)
        {
            this.gameFieldSymbols[rowFillingNumber, columnFillingNumber] = this.cellLabels[this.userNumberForTurn];
            this.successfulTurnsCount++;
        }
    }

    private bool IsSomeoneWon()
    {
        for (int i = 0; i < 3; i++)
        {
            char firstCharInRow = this.gameFieldSymbols[i, 0];
            if (firstCharInRow == '.')
                continue;
            for (int j = 1; j < 3; j++)
            {
                if (this.gameFieldSymbols[i, j] != firstCharInRow)
                    break;
                if (j == 2)
                    return true;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            char firstCharInColumn = this.gameFieldSymbols[0, j];
            if (firstCharInColumn == '.')
                continue;
            for (int i = 1; i < 3; i++)
            {
                if (this.gameFieldSymbols[i, j] != firstCharInColumn)
                    break;
                if (i == 2)
                    return true;
            }
        }
        char firstCharInMainDiagonal = this.gameFieldSymbols[0, 0];
        if (firstCharInMainDiagonal != '.')
            for (int i = 1; i < 3; i++)
            {
                if (this.gameFieldSymbols[i, i] != firstCharInMainDiagonal)
                    break;
                if (i == 2)
                    return true;
            }
        char firstCharInSecondaryDiagonal = this.gameFieldSymbols[0, 2];
        if (firstCharInSecondaryDiagonal != '.')
            for (int i = 1; i < 3; i++)
            {
                if (this.gameFieldSymbols[i, 2 - i] != firstCharInSecondaryDiagonal)
                    break;
                if (i == 2)
                    return true;
            }
        return false;
    }
    public void LaunchGame()
    {
        while (this.successfulTurnsCount < 9)
        {
            Console.Clear();
            this.WriteUserNumberForTurn();
            this.DrawGameField();
            this.PerformOneUserTurn();
            if (this.IsSomeoneWon())
            {
                Console.Clear();
                this.DrawGameField();
                Console.WriteLine($"Победил игрок {this.userNames[this.userNumberForTurn]} !");
                return;
            }
            this.userNumberForTurn = 1 - this.userNumberForTurn;
        }
        Console.Clear();
        this.DrawGameField();
        Console.WriteLine("Победила дружба!");
    }
};