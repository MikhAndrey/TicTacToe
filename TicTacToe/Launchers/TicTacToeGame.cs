using TicTacToe.Helpers;
using TicTacToe.Resources;
using TicTacToe.Model.ViewModel;
using TicTacToe.Services;
using System.Globalization;

namespace TicTacToe.Launchers { 
    public class TicTacToeGame
    {
        private Player[] _players;
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
        private char _turnInputSeparator;
        private char _userDataInputSeparator;
        private int _minAllowedAge;
        private int _maxAllowedAge;
        private DateTime _gameStartDate;
        private DateTime _gameEndDate;
        private string[] _languagesAbbreviations;

        public TicTacToeGame(int playersCount = Constants.Constants.PlayersCount,
            int fieldSize = Constants.Constants.GameFieldSize,
            int maxNameLength = Constants.Constants.MaxAllowedNameLength,
            int maxRetriesCount = Constants.Constants.MaxAllowedRetriesCount,
            string symbols = Constants.Constants.TicTacToeSymbols,
            char fieldSymbol = Constants.Constants.FieldSymbol,
            char verticalSeparator = Constants.Constants.VerticalFieldSeparator,
            char horizontalSeparator = Constants.Constants.HorizontalFieldSeparator,
            char turnInputSeparator = Constants.Constants.UserTurnInputSeparator,
            char userDataInputSeparator = Constants.Constants.UserDataInputSeparator,
            int minAllowedAge = Constants.Constants.MinAllowedAge,
            int maxAllowedAge = Constants.Constants.MaxAllowedAge,
            string languagesAbbreviations = Constants.Constants.SupportedLanguagesAbbreviations)
        {
            _gameStartDate = DateTime.Now;
            _playersCount = playersCount;
            _fieldSize = fieldSize;
            _maxNameLength = maxNameLength;
            _maxRetriesCount = maxRetriesCount;
            _userSymbols = symbols;
            _fieldSymbol = fieldSymbol;
            _verticalSeparator = verticalSeparator;
            _horizontalSeparator = horizontalSeparator;
            _turnInputSeparator = turnInputSeparator;
            _userDataInputSeparator = userDataInputSeparator;
            _minAllowedAge = minAllowedAge;
            _maxAllowedAge = maxAllowedAge;
            _players = new Player[_playersCount];
            _gameFieldSymbols = new char[_fieldSize, _fieldSize];
            _languagesAbbreviations = languagesAbbreviations.Split(',');
            for (int i = 0; i < _fieldSize; i++)
                for (int j = 0; j < _fieldSize; j++)
                    _gameFieldSymbols[i, j] = _fieldSymbol;
            SetUICulture();
            SetUsersPersonalData();
            PlayersDBController.UpdatePlayersDB(_playersCount,_players); 
        }
        public void Play()
        {
            LaunchGame();
            JsonController.GenerateJsonReports(GamesDBController._gamesDB, _players);
            ConfirmGameRepeat();
        }
        private void SetUICulture()
        {
            string languagesNamesString = Messages.LanguagesNames;
            string[] languagesNames = languagesNamesString.Split(' ');
            int languagesCount = languagesNames.Length;
            while (true)
            {
                Console.WriteLine(Messages.SelectLanguageMessage);
                for (int i = 0; i < languagesCount; i++)
                    Console.WriteLine($"{_languagesAbbreviations[i]} - {languagesNames[i]};");
                string? userLanguage = Console.ReadLine();
                if (!string.IsNullOrEmpty(userLanguage))
                {
                    userLanguage = userLanguage.Trim();
                    if (Array.IndexOf(_languagesAbbreviations, userLanguage.ToLower()) != -1)
                    {
                        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(userLanguage);
                        return;
                    }
                }
                Console.WriteLine("\n" + Messages.SelectLanguageRetryMessage + "\n");
            }
        }
        private void LaunchGame()
        {
            void FinishCurrentGame(string message, string? winnerName, int? winnerId)
            {
                _gameEndDate = DateTime.Now;
                DrawGameField();
                Console.WriteLine(message, winnerName);
                GamesDBController.UpdateGamesDB(_gameStartDate, _gameEndDate, _userSymbols, _players, winnerId);
            }
            while (_successfulTurnsCount < _fieldSize * _fieldSize)
            {
                WriteUserNumberForTurn();
                DrawGameField();
                PerformOneUserTurn();
                if (IsSomeoneWon())
                {
                    FinishCurrentGame(Messages.WinnerDeclarationMessage, _players[_userNumberForTurn].Name, _players[_userNumberForTurn].Id);
                    return;
                }
                _userNumberForTurn = _userNumberForTurn == _playersCount - 1 ? 0 : _userNumberForTurn + 1;
            }
            FinishCurrentGame(Messages.DrawMessage, null, null);
        }
        private void SetUsersPersonalData()
        {
            bool correctUserInput;
            string? possiblePlayerPersonalData;
            string playerName = "";
            int playerId = 0;
            int playerAge = 0;
            List<int> bookedIds = new();
            for (int i = 0; i < _playersCount; i++)
            {
                do
                {
                    Console.WriteLine(Messages.AskForDataInputMessage, i + 1);
                    possiblePlayerPersonalData = Console.ReadLine();
                    correctUserInput = UserDataInputCheck.isUserDataInputProper(possiblePlayerPersonalData,
                        _userDataInputSeparator,
                        _maxNameLength,
                        _minAllowedAge,
                        _maxAllowedAge,
                        ref playerId,
                        ref playerName,
                        ref playerAge,
                        bookedIds);
                } while (!correctUserInput);
                _players[i] = new(_userSymbols[i], playerName, playerId, playerAge);
                bookedIds.Add(playerId);
            }
        }
        private void WriteUserNumberForTurn()
        {
            Console.WriteLine(Messages.CurrentTurnPlayerDeclarationMessage, _players[_userNumberForTurn].Name);
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
        private void PerformOneUserTurn()
        {
            string? userInput;
            bool wrongFieldNumberInput;
            int attemptsLeftCount = _maxRetriesCount;
            do
            {
                Console.WriteLine(Messages.AskForPerformCurrentTurnMessage, _players[_userNumberForTurn].Name);
                userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                    wrongFieldNumberInput = true;
                else
                    wrongFieldNumberInput = !UserTurnCheck.IsUserTurnInputProper(userInput,
                        _fieldSize,
                        _turnInputSeparator,
                        ref _rowNumberForTurn,
                        ref _columnNumberForTurn,
                        _gameFieldSymbols,
                        _fieldSymbol);
                if (wrongFieldNumberInput)
                {
                    attemptsLeftCount--;
                    Console.WriteLine(Messages.WrongCellMessage, attemptsLeftCount);
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
        private void ConfirmGameRepeat()
        {
            ConsoleKey confirmKey;
            do
            {
                Console.WriteLine(Messages.RepeatConfirmMessage);
                confirmKey = Console.ReadKey().Key;
                if (confirmKey != ConsoleKey.Enter)
                    Environment.Exit(0);
            } while (confirmKey != ConsoleKey.Enter);
        }
    };
}