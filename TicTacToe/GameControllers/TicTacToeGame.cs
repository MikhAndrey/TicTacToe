using TicTacToe.Entities;
using TicTacToe.Helpers;
using TicTacToe.Interfaces;
using TicTacToe.DBRepositories;
using System.Text.Json;
using TicTacToe.Resources;
namespace TicTacToe.GameControllers
{
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
        private IRepository<Player> _playersDB;
        private IRepository<GameDataForDB> _gamesDB;
        private string[] _jsonGenerationCommands;

        public TicTacToeGame(int playersCount = GameConstants.PlayersCount,
            int fieldSize = GameConstants.GameFieldSize,
            int maxNameLength = GameConstants.MaxAllowedNameLength,
            int maxRetriesCount = GameConstants.MaxAllowedRetriesCount,
            string symbols = GameConstants.TicTacToeSymbols,
            char fieldSymbol = GameConstants.FieldSymbol,
            char verticalSeparator = GameConstants.VerticalFieldSeparator,
            char horizontalSeparator = GameConstants.HorizontalFieldSeparator,
            char turnInputSeparator = GameConstants.UserTurnInputSeparator,
            char userDataInputSeparator = GameConstants.UserDataInputSeparator,
            int minAllowedAge = GameConstants.MinAllowedAge,
            int maxAllowedAge = GameConstants.MaxAllowedAge,
            string JSONGenerationCommands = GameConstants.JSONGenerationCommands)
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
            _playersDB = new SQLPlayersRepository();
            _gamesDB = new SQLGamesRepository();
            _jsonGenerationCommands = JSONGenerationCommands.Split(',');
            for (int i = 0; i < _fieldSize; i++)
                for (int j = 0; j < _fieldSize; j++)
                    _gameFieldSymbols[i, j] = _fieldSymbol;
            SetUsersPersonalData();
            SetUserSymbols();
            UpdatePlayersDB();
        }
        public void LaunchGame()
        {
            GameDataForDB thisGameData;
            void FinishCurrentGame(string message, string? winnerName, int? winnerId)
            {
                _gameEndDate = DateTime.Now;
                DrawGameField();
                Console.WriteLine(message, winnerName);
                thisGameData = new(_gameStartDate, _gameEndDate, _userSymbols[0], _userSymbols[1], _players[0].Id, _players[1].Id, winnerId);
                _gamesDB.Add(thisGameData);
                _gamesDB.Save();
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
        public void ConfirmGameRepeat()
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
        private void SetUsersPersonalData()
        {
            bool correctUserInput = false;
            string? possiblePlayerPersonalData;
            string playerName = "";
            int playerId = 0;
            int playerAge = 0;
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
                        ref playerAge);
                    if (correctUserInput)
                        _players[i] = new(playerName, playerId, playerAge);
                } while (!correctUserInput);
            }
        }
        private void WriteUserNumberForTurn()
        {
            Console.WriteLine(Messages.CurrentTurnPlayerDeclarationMessage, _players[_userNumberForTurn].Name);
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
        public void UpdatePlayersDB()
        {
            for (int i = 0; i < 2; i++)
            {
                Player? possiblePlayerFromDB = _playersDB.GetItem(_players[i].Id);
                if (possiblePlayerFromDB == null)
                    _playersDB.Add(_players[i]);
                else
                {
                    possiblePlayerFromDB.Name = _players[i].Name;
                    possiblePlayerFromDB.Age = _players[i].Age;
                }
                _playersDB.Save();
            }
        }
        public async void GenerateJSONReports()
        {
            List<GameDataForDB> gamesList = _gamesDB.GetList();
            int gamesCount = gamesList.Count();
            while (true)
            {
                FileStream lastGameFile = new FileStream("lastgameresult.json", FileMode.OpenOrCreate);
                FileStream currentPlayersGamesFile = new FileStream("currentplayersgamesresults.json", FileMode.OpenOrCreate);
                FileStream allGamesFile = new FileStream("allgamesresults.json", FileMode.OpenOrCreate);
                Console.WriteLine(Messages.AskForEnterCommandMessage +
                    $"\n{_jsonGenerationCommands[0]}:" + Messages.FirstCommandMessage +
                    $"\n{_jsonGenerationCommands[1]}:" + Messages.SecondCommandMessage +
                    $"\n{_jsonGenerationCommands[2]}:" + Messages.ThirdCommandMessage +
                    $"\n{_jsonGenerationCommands[3]}:" + Messages.FourthCommandMessage);
                string? userGenerationCommand = Console.ReadLine();
                int userGenerationCommandIndex = Array.IndexOf(_jsonGenerationCommands, userGenerationCommand);
                switch (userGenerationCommandIndex)
                {
                    case 0:
                        lastGameFile.SetLength(0);
                        await JsonSerializer.SerializeAsync<GameDataForDB>(lastGameFile, gamesList[gamesCount - 1]);
                        Console.WriteLine("\n" + Messages.LastGameSaveMessage + "\n");
                        break;
                    case 1:
                        currentPlayersGamesFile.SetLength(0);
                        foreach (GameDataForDB game in gamesList)
                        {
                            bool isTheGameOfRequiredTwoPlayers = game.FirstPlayerId == _players[0].Id && game.SecondPlayerId == _players[1].Id;
                            bool isTheGameOfRequiredTwoPlayersReverse = game.FirstPlayerId == _players[1].Id && game.SecondPlayerId == _players[0].Id;
                            if (isTheGameOfRequiredTwoPlayers || isTheGameOfRequiredTwoPlayersReverse)
                                await JsonSerializer.SerializeAsync<GameDataForDB>(currentPlayersGamesFile, game);
                        }
                        Console.WriteLine("\n" + Messages.GamesWithCurrentPlayersSaveMessage + "\n");
                        break;
                    case 2:
                        allGamesFile.SetLength(0);
                        foreach (GameDataForDB game in gamesList)
                        {
                            await JsonSerializer.SerializeAsync<GameDataForDB>(allGamesFile, game);
                        }
                        Console.WriteLine("\n" + Messages.AllGamesSaveMessage + "\n");
                        break;
                    case 3:
                        lastGameFile.Close();
                        currentPlayersGamesFile.Close();
                        allGamesFile.Close();
                        return;
                    default:
                        Console.WriteLine("\n" + Messages.WrongCommandMessage + "\n");
                        break;
                }
                lastGameFile.Close();
                currentPlayersGamesFile.Close();
                allGamesFile.Close();
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
}