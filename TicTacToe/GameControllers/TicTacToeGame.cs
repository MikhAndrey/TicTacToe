namespace TicTacToe.GameControllers;
using TicTacToe.Storage;
public class TicTacToeGame
{
    private PlayerData[] _players;

    private char _fieldSymbol;

    private char[,] _gameFieldSymbols;

    private int _fieldSize;

    private int _maxNameLength;

    private int _maxRetriesCount;

    private string _userSymbols;

    private int _playersCount;

    private int _userNumberForTurn = 0;

    private int _successfulTurnsCount = 0;

    private int _rowNumberForTurn;

    private int _columnNumberForTurn;

    private char _horizontalSeparator;
    
    private char _verticalSeparator;
    
    private char _inputSeparator;

    public TicTacToeGame(int playersCount = GameConstants.PlayersCount,
        int fieldSize = GameConstants.GameFieldSize,
        int maxNameLength = GameConstants.MaxAllowedNameLength,
        int maxRetriesCount = GameConstants.MaxAllowedRetriesCount,
        string symbols = GameConstants.TicTacToeSymbols,
        char fieldSymbol = GameConstants.FieldSymbol,   
        char verticalSeparator = GameConstants.VerticalFieldSeparator,
        char horizontalSeparator = GameConstants.HorizontalFieldSeparator,
        char inputSeparator = GameConstants.UserTurnInputSeparator)
    {
        _playersCount = playersCount;
        _fieldSize = fieldSize;
        _maxNameLength = maxNameLength;
        _maxRetriesCount = maxRetriesCount;
        _userSymbols = symbols;
        _fieldSymbol = fieldSymbol;
        _verticalSeparator = verticalSeparator;
        _horizontalSeparator = horizontalSeparator;
        _inputSeparator = inputSeparator;
        _players = new PlayerData[_playersCount];   
        _gameFieldSymbols = new char[_fieldSize, _fieldSize];
        for (int i = 0; i < _fieldSize; i++)
            for (int j = 0; j < _fieldSize; j++)
                _gameFieldSymbols[i, j] = _fieldSymbol;
        SetUserNames();
        SetUserSymbols();
    }
    public void LaunchGame()
    {
        while (_successfulTurnsCount < _fieldSize * _fieldSize)
        {
            WriteUserNumberForTurn();
            DrawGameField();
            PerformOneUserTurn();
            if (IsSomeoneWon())
            {
                DrawGameField();
                Console.WriteLine($"Player {_players[_userNumberForTurn].Name} won!");
                return;
            }
            _userNumberForTurn = _userNumberForTurn == _playersCount - 1 ? 0 : _userNumberForTurn + 1;
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
        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize - 1; j++)
                Console.Write($"{_gameFieldSymbols[i, j]}{_verticalSeparator}");
            Console.WriteLine(_gameFieldSymbols[i, _fieldSize - 1]);
            if (i != _fieldSize - 1)
            {
                for (int j = 0; j < _fieldSize - 1; j++)
                    Console.Write($"{_horizontalSeparator}{_horizontalSeparator}");
                Console.WriteLine($"{_horizontalSeparator}{_horizontalSeparator}");
            }
        }
    }
    private void SetUserSymbols()
    {
        for (int i = 0; i < _playersCount; i++)
            _players[i].Symbol = _userSymbols[i];
    }
    private void SetUserNames()
    {
        bool wrongNameLength;
        string? possiblePlayerName;
        for (int i = 0; i < _playersCount; i++)
        {
            do
            {
                Console.WriteLine($"Player {i + 1}, enter your name: ");
                possiblePlayerName = Console.ReadLine();
                wrongNameLength = string.IsNullOrEmpty(possiblePlayerName) || possiblePlayerName.Length > _maxNameLength;
                if (wrongNameLength)
                {
                    Console.WriteLine($"Your name is too short or too long. Your name can't be longer than {_maxNameLength} symbols");
                    Console.ReadKey();
                }
                else
                    _players[i] = new(possiblePlayerName);
            } while (wrongNameLength);
        }
    }
    private void WriteUserNumberForTurn()
    {
        Console.WriteLine($"Player {_players[_userNumberForTurn].Name}'s turn");
    }

    private bool IsUserInputProper(string input)
    {
        string[] properValues = new string[_fieldSize];
        for (int i = 0; i < _fieldSize; i++)
            properValues[i] = (i + 1).ToString();
        input = input.Trim();
        int firstSpaceIndex = input.IndexOf(_inputSeparator);
        if (firstSpaceIndex < 0)
            return false;
        int lastSpaceIndex = input.LastIndexOf(_inputSeparator);
        string possibleStringRowNumber = input.Substring(0, firstSpaceIndex);
        string possibleStringColumnNumber = input.Substring(lastSpaceIndex + 1, input.Length - 1 - lastSpaceIndex);
        string stringBeforeColumnAndRowNumber = input.Substring(firstSpaceIndex, lastSpaceIndex - firstSpaceIndex + 1);
        bool isInputProper = string.IsNullOrWhiteSpace(stringBeforeColumnAndRowNumber) && properValues.Contains(possibleStringRowNumber) && properValues.Contains(possibleStringColumnNumber);
        if (!isInputProper)
            return false;
        else
        {
            _rowNumberForTurn = int.Parse(possibleStringRowNumber);
            _columnNumberForTurn = int.Parse(possibleStringColumnNumber);
            if (_gameFieldSymbols[_rowNumberForTurn - 1, _columnNumberForTurn - 1] == _fieldSymbol)
                return true;
            else
                return false;
        }
    }
    private void PerformOneUserTurn()
    {
        string? userInput;
        bool wrongFieldNumberInput;
        int attemptsLeftCount = _maxRetriesCount;
        do
        {
            Console.WriteLine($"Player {_players[_userNumberForTurn].Name}, enter the row number and column number of the cell you would like to occupy, separated by a space");
            userInput = Console.ReadLine();
            if (string.IsNullOrEmpty(userInput))
                wrongFieldNumberInput = true;
            else
                wrongFieldNumberInput = !IsUserInputProper(userInput);
            if (wrongFieldNumberInput)
            {
                attemptsLeftCount--;
                Console.WriteLine($"You entered the data in the wrong format or the cell you wanted to occupy is already occupied. There are {attemptsLeftCount} attempts left before the move goes to the opponent");
            }
        } while (wrongFieldNumberInput && attemptsLeftCount != 0);
        if (attemptsLeftCount != 0)
        {
            _gameFieldSymbols[_rowNumberForTurn - 1, _columnNumberForTurn - 1] = _players[_userNumberForTurn].Symbol;
            _successfulTurnsCount++;
        }
    }

    private bool IsSomeoneWon()
    {
        for (int i = 0; i < _fieldSize; i++)
        {
            char firstCharInRow = _gameFieldSymbols[i, 0];
            if (firstCharInRow == _fieldSymbol)
                continue;
            for (int j = 1; j < _fieldSize; j++)
            {
                if (_gameFieldSymbols[i, j] != firstCharInRow)
                    break;
                if (j == _fieldSize - 1)
                    return true;
            }
        }
        for (int j = 0; j < _fieldSize; j++)
        {
            char firstCharInColumn = _gameFieldSymbols[0, j];
            if (firstCharInColumn == _fieldSymbol)
                continue;
            for (int i = 1; i < _fieldSize; i++)
            {
                if (_gameFieldSymbols[i, j] != firstCharInColumn)
                    break;
                if (i == _fieldSize - 1)
                    return true;
            }
        }
        char firstCharInMainDiagonal = _gameFieldSymbols[0, 0];
        if (firstCharInMainDiagonal != _fieldSymbol)
            for (int i = 1; i < _fieldSize; i++)
            {
                if (_gameFieldSymbols[i, i] != firstCharInMainDiagonal)
                    break;
                if (i == _fieldSize - 1)
                    return true;
            }
        char firstCharInSecondaryDiagonal = _gameFieldSymbols[0, _fieldSize - 1];
        if (firstCharInSecondaryDiagonal != _fieldSymbol)
            for (int i = 1; i < _fieldSize; i++)
            {
                if (_gameFieldSymbols[i, _fieldSize - 1 - i] != firstCharInSecondaryDiagonal)
                    break;
                if (i == _fieldSize - 1)
                    return true;
            }
        return false;
    }
};