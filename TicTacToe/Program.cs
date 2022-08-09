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
                return;
        } while (confirm_key != ConsoleKey.Y);
    }    
}

PlayTicTacToe();
public class TicTacToeGame
{
    private char[,] GameFieldSymbols = new char [3,3];

    private string[] UserNames = new string [2];

    private char[] CellLabels = { 'x', 'o' };

    private int UserNumberForTurn = 0;

    private int ResultTurnsCount = 0;
    public TicTacToeGame()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                this.GameFieldSymbols[i,j] = '.';
        this.SetUserNames();
    }
    private void DrawGameField()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
                Console.Write($"{this.GameFieldSymbols[i, j]}|");
            Console.WriteLine(this.GameFieldSymbols[i, 2]);
            if (i != 2)
                Console.WriteLine("------");
        }
    }
    private void SetUserNames()
    {
        int name_length;
        bool wrong_name_length;
        for (int i = 0; i < 2; i++)
        {
            do
            {
                Console.Clear();
                Console.Write($"Игрок {i + 1}, введите своё имя: ");
                this.UserNames[i] = Console.ReadLine();
                name_length = this.UserNames[i].Length;
                wrong_name_length = name_length > 25 || name_length == 0;
                if (wrong_name_length)
                {
                    Console.Write("Введенное имя слишком длинное или слишком короткое");
                    Console.ReadKey();
                }
            } while (wrong_name_length);
        }
    }
    private void WriteUserNumberForTurn()
    {
        Console.WriteLine($"Ход игрока {this.UserNames[this.UserNumberForTurn]}");
    }

    private bool IsUserInputProper(string input, ref int row_num, ref int col_num)
    {
        char[] properValues = { '0', '1', '2' };
        bool is_input_proper =  input.Length == 3 && input[1] == ' ' && properValues.Contains(input[0]) && properValues.Contains(input[2]);
        if (!is_input_proper)
            return false;
        else
        {
            row_num = int.Parse(input.Substring(0, 1));
            col_num = int.Parse(input.Substring(2, 1));
            if (this.GameFieldSymbols[row_num, col_num] == '.')
                return true;
            else
                return false;
        }
    }
    private void PerformOneUserTurn()
    {
        string user_input;
        bool wrong_field_number_input;
        int attempts_left_count = 3;
        int row_filling_number = new int();
        int column_filling_number = new int();
        do
        {
            Console.WriteLine($"Игрок {this.UserNames[this.UserNumberForTurn]}, введите через пробел номер строки и номер столбца клетки, которую вы бы хотели занять");
            user_input = Console.ReadLine();
            wrong_field_number_input = !this.IsUserInputProper(user_input, ref row_filling_number, ref column_filling_number);
            if (wrong_field_number_input) {
                attempts_left_count--;
                Console.WriteLine($"Вы ввели данные в неверном формате или клетка, которую вы хотели занять, уже занята. Осталось {attempts_left_count} попыток, прежде чем ход перейдет к сопернику");
            }
        } while (wrong_field_number_input && attempts_left_count != 0);
        if (attempts_left_count != 0)
        {
            this.GameFieldSymbols[row_filling_number, column_filling_number] = this.CellLabels[this.UserNumberForTurn];
            this.ResultTurnsCount++;
        }
        this.UserNumberForTurn = 1 - this.UserNumberForTurn;
    }

    private bool IsSomeoneWon()
    {
        for (int i = 0; i < 3; i++)
        {
            char first_char_in_row = this.GameFieldSymbols[i, 0];
            if (first_char_in_row == '.')
                continue;
            for (int j = 1; j < 3; j++)
            {
                if (this.GameFieldSymbols[i, j] != first_char_in_row)
                    break;
                if (j == 2)
                    return true;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            char first_char_in_column = this.GameFieldSymbols[0, j];
            if (first_char_in_column == '.')
                continue;
            for (int i = 1; i < 3; i++)
            {
                if (this.GameFieldSymbols[i, j] != first_char_in_column)
                    break;
                if (i == 2)
                    return true;
            }
        }
        char first_char_in_main_diagonal = this.GameFieldSymbols[0, 0];
        if (first_char_in_main_diagonal != '.')
            for (int i = 1; i < 3; i++)
            {
                if (this.GameFieldSymbols[i, i] != first_char_in_main_diagonal)
                    break;
                if (i == 2)
                    return true;
            }
        char first_char_in_secondary_diagonal = this.GameFieldSymbols[0, 2];
        if (first_char_in_main_diagonal != '.')
            for (int i = 1; i < 3; i++)
            {
                if (this.GameFieldSymbols[i, 2 - i] != first_char_in_secondary_diagonal)
                    break;
                if (i == 2)
                    return true;
            }
        return false;
    }
    public void LaunchGame()
    {
        while (this.ResultTurnsCount < 9) { 
            Console.Clear();
            this.WriteUserNumberForTurn();
            this.DrawGameField();
            this.PerformOneUserTurn();
            if (this.IsSomeoneWon())
            {
                Console.Clear();
                this.DrawGameField();
                Console.WriteLine($"Победил игрок {this.UserNames[1 - this.UserNumberForTurn]} !");
                    return;
            }
        }
        Console.Clear();
        this.DrawGameField();
        Console.WriteLine("Победила дружба!");
    }
};



