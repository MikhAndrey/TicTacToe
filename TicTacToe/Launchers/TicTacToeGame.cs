using TicTacToe.Helpers;
using TicTacToe.Resources;
using TicTacToe.Model.ViewModel;
using TicTacToe.Services;
using TicTacToe.Utils;
using System.Globalization;

namespace TicTacToe.Launchers {

    /// <summary>
    ///   This class describes all the main actions that take place during the game.
    /// </summary>
    public class TicTacToeGame
    {
        
        /// <summary>
        /// The game players
        /// </summary>
        private Player[] _players;

        /// <summary>
        /// Default field symbol that shows unoccupied cell
        /// </summary>
        private char _fieldSymbol;

        /// <summary>
        /// The game field symbols. Describes current game field state
        /// </summary>
        private char[,] _gameFieldSymbols;

        /// <summary>
        /// The game field size
        /// </summary>
        private int _fieldSize;

        /// <summary>
        /// The maximum player's name length
        /// </summary>
        private int _maxNameLength;

        /// <summary>
        /// The maximum retries for one turn count. After this number of tries turn goes to the opponent.
        /// </summary>
        private int _maxRetriesCount;

        /// <summary>
        /// The player symbols.
        /// </summary>
        private string _userSymbols;

        /// <summary>
        /// The number of players in current game.
        /// </summary>
        private int _playersCount;

        /// <summary>
        /// The number of user who is taking the turn at the moment.
        /// </summary>
        private int _userNumberForTurn = 0;

        /// <summary>
        /// This property describes how many turns ended by placing a new symbol on the game field.
        /// </summary>
        private int _successfulTurnsCount = 0;

        /// <summary>
        /// The row number of cell player wants to occupy.
        /// </summary>
        private int _rowNumberForTurn;

        /// <summary>
        /// The column number of cell player wants to occupy.
        /// </summary>
        private int _columnNumberForTurn;

        /// <summary>
        /// The symbol that separates two adjacent rows of the game field.
        /// </summary>
        private char _horizontalSeparator;

        /// <summary>
        /// The symbol that separates two adjacent columns of the game field.
        /// </summary>
        private char _verticalSeparator;

        /// <summary>
        /// The symbol that separates row and column number in player's input.
        /// </summary>
        private char _turnInputSeparator;

        /// <summary>
        /// The symbol that separates player's personal data clusters when input.
        /// </summary>
        private char _userDataInputSeparator;

        /// <summary>
        /// The minimum allowed player age.
        /// </summary>
        private int _minAllowedAge;

        /// <summary>
        /// The maximum allowed player age.
        /// </summary>
        private int _maxAllowedAge;

        /// <summary>
        /// The game start date.
        /// </summary>
        private DateTime _gameStartDate;

        /// <summary>
        /// The game end date.
        /// </summary>
        private DateTime _gameEndDate;

        /// <summary>
        /// Supported languages abbreviations.
        /// </summary>
        private string[] _languagesAbbreviations;

        /// <summary>The json commands. By default we take them from constants</summary>
        private string[] _commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicTacToeGame"/> class.
        /// </summary>
        /// <param name="playersCount">The players count.</param>
        /// <param name="fieldSize">Size of the field.</param>
        /// <param name="maxNameLength">Maximum length of the name.</param>
        /// <param name="maxRetriesCount">The maximum retries count.</param>
        /// <param name="playerSymbols">The player symbols.</param>
        /// <param name="fieldSymbol">The symbol that fills all game field by default.</param>
        /// <param name="verticalSeparator">The symbol that separates two adjacent columns.</param>
        /// <param name="horizontalSeparator">The symbol that separates two adjacent rows.</param>
        /// <param name="turnInputSeparator">The symbol that separates row number and column number while turn.</param>
        /// <param name="userDataInputSeparator">The player personal data's input separator.</param>
        /// <param name="minAllowedAge">The minimum allowed player's age.</param>
        /// <param name="maxAllowedAge">The maximum allowed player's age.</param>
        /// <param name="languagesAbbreviations">The languages abbreviations.</param>
        /// <param name="languagesAbbreviationsSeparator">The languages abbreviations separator.</param>
        /// <param name="jsonCommandsString">The string that contains commands to interact with JSON files.</param>
        /// /// <param name="jsonCommandsSeparator">Separates two adjacent commands that interact with JSON files.</param>
        public TicTacToeGame(int playersCount = Constants.Constants.PlayersCount,
            int fieldSize = Constants.Constants.GameFieldSize,
            int maxNameLength = Constants.Constants.MaxAllowedNameLength,
            int maxRetriesCount = Constants.Constants.MaxAllowedRetriesCount,
            string playerSymbols = Constants.Constants.PlayerSymbols,
            char fieldSymbol = Constants.Constants.FieldSymbol,
            char verticalSeparator = Constants.Constants.VerticalFieldSeparator,
            char horizontalSeparator = Constants.Constants.HorizontalFieldSeparator,
            char turnInputSeparator = Constants.Constants.UserTurnInputSeparator,
            char userDataInputSeparator = Constants.Constants.UserDataInputSeparator,
            int minAllowedAge = Constants.Constants.MinAllowedAge,
            int maxAllowedAge = Constants.Constants.MaxAllowedAge,
            string languagesAbbreviations = Constants.Constants.SupportedLanguagesAbbreviations,
            char languagesAbbreviationsSeparator = Constants.Constants.LanguagesAbbreviationsSeparator,
            string commandsString = Constants.Constants.Commands,
            char commandsSeparator = Constants.Constants.CommandsSeparator)
        {
            _gameStartDate = DateTime.Now;
            _playersCount = playersCount;
            _fieldSize = fieldSize;
            _maxNameLength = maxNameLength;
            _maxRetriesCount = maxRetriesCount;
            _userSymbols = playerSymbols;
            _fieldSymbol = fieldSymbol;
            _verticalSeparator = verticalSeparator;
            _horizontalSeparator = horizontalSeparator;
            _turnInputSeparator = turnInputSeparator;
            _userDataInputSeparator = userDataInputSeparator;
            _minAllowedAge = minAllowedAge;
            _maxAllowedAge = maxAllowedAge;
            _players = new Player[_playersCount];
            _gameFieldSymbols = new char[_fieldSize, _fieldSize];
            _languagesAbbreviations = languagesAbbreviations.Split(languagesAbbreviationsSeparator);        
            _commands = commandsString.Split(commandsSeparator); 
            for (int i = 0; i < _fieldSize; i++)
                for (int j = 0; j < _fieldSize; j++)
                    _gameFieldSymbols[i, j] = _fieldSymbol;
        }

        /// <summary>Plays a new game, contains all necessary methods calls inside to complete one game.</summary>
        public void Play()
        {
            SetGameLocalSettings();
            PlayersDBService.UpdatePlayersDB(_playersCount, _players);
            LaunchGame(out int? winnerId);
            GamesDBService.UpdateGamesDB(_gameStartDate, _gameEndDate, _userSymbols, _players, winnerId);
            WorkWithUserCommands();
            ConfirmGameRepeat();        
        }

        /// <summary>
        /// Sets game local settings such as player personal data and game language.
        /// </summary>
        private void SetGameLocalSettings()
        {
            string userLanguage = SelectGameLanguage();
            SetUILanguage(userLanguage);
            SetPlayersPersonalData();
        }

        /// <summary>
        /// This method plays current game until someone wins, otherwise draw is declared.
        /// </summary>
        /// <param name="winnerId">Id of a player who won the game or null in case of draw.</param>
        private void LaunchGame(out int? winnerId)
        {
            while (_successfulTurnsCount < _fieldSize * _fieldSize)     //I.e. while there are left some cells not occupied by a player
            {
                ConsoleHandler.WriteLine(Messages.CurrentTurnPlayerDeclarationMessage, _players[_userNumberForTurn].Name);
                DrawGameField();
                PerformOneUserTurn();
                if (IsSomeoneWon())
                {
                    _gameEndDate = DateTime.Now;
                    winnerId = _players[_userNumberForTurn].Id;
                    DrawGameEndInfo(Messages.WinnerDeclarationMessage, _players[_userNumberForTurn].Name);
                    return;
                }
                //Current player number's equality to playersCount - 1 means that player with number 0 takes the next turn
                _userNumberForTurn = _userNumberForTurn == _playersCount - 1 ? 0 : _userNumberForTurn + 1;
            }
            _gameEndDate = DateTime.Now;
            winnerId = null;
            DrawGameEndInfo(Messages.DrawMessage, null);
        }

        /// <summary>
        /// Supports stage of the game when user enters the commands.
        /// </summary>
        private void WorkWithUserCommands()
        {
            int generationCommandsCount = _commands.Length;
            int commandIndex;
            do
            {
                commandIndex = GetUserCommandIndex();
                ProcessUserCommand(commandIndex, out string reportState, out object[]? additionalParams);
                ConsoleHandler.WriteLine(reportState, additionalParams);
            } while (commandIndex != generationCommandsCount - 1);
        }

        /// <summary>Asks players do they want to play the game again or exit.</summary>
        private void ConfirmGameRepeat()
        {
            ConsoleHandler.WriteLine(Messages.RepeatConfirmMessage);
            ConsoleKey confirmKey = ConsoleHandler.ReadKey().Key;
            if (confirmKey != ConsoleKey.Enter)
                Environment.Exit(0);
            else
                return;
        }

        /// <summary>This method asks the user to select game language until he enters language abbreviation that corresponds to one of given.</summary>
        private string SelectGameLanguage()
        {
            string languagesNamesString = Messages.LanguagesNames;                                                                  
            string[] languagesNames = languagesNamesString.Split(Constants.Constants.LanguagesNamesSeparator);  //Languages names are stored as a string, so we need to separate each language name
            int languagesCount = languagesNames.Length;
            while (true)
            {
                ConsoleHandler.WriteLine(Messages.SelectLanguageMessage);      
                for (int i = 0; i < languagesCount; i++)
                    ConsoleHandler.WriteLine($"{_languagesAbbreviations[i]} - {languagesNames[i]};");
                string? userLanguage = ConsoleHandler.ReadLine();
                if (!string.IsNullOrEmpty(userLanguage))        
                {
                    userLanguage = userLanguage.Trim();
                    if (Array.IndexOf(_languagesAbbreviations, userLanguage.ToLower()) != -1)       
                    {      
                        return userLanguage;
                    }
                }
                ConsoleHandler.WriteLine("\n" + Messages.SelectLanguageRetryMessage + "\n");      
            }
        }

        /// <summary>
        /// Sets the UI
        /// language by the given language abbreviation.
        /// </summary>
        /// <param name="userLanguage">The given language abbreviation</param>
        private void SetUILanguage(string userLanguage)
        {
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(userLanguage);
        }

        /// <summary>
        /// Asks player to enter his personal data until his input matches the required pattern. 
        /// </summary>
        /// <param name="playerNumber">Current player number</param>
        /// <param name="takenIds">The list of ID's that have already been taken</param>
        /// <param name="playerId">Result player's ID</param>
        /// <param name="playerName">Result player's name</param>
        /// <param name="playerAge">Result player's age</param>
        private void InputPlayerPersonalData(int playerNumber,
            List<int> takenIds, out int playerId, out string playerName, out int playerAge)
        {
            bool isInputCorrect;
            do
            {
                ConsoleHandler.WriteLine(Messages.AskForDataInputMessage, playerNumber);
                string? possiblePlayerPersonalData = ConsoleHandler.ReadLine();
                isInputCorrect = UserDataInputCheck.IsUserDataInputProper(possiblePlayerPersonalData,
                    _userDataInputSeparator,
                    _maxNameLength,
                    _minAllowedAge,
                    _maxAllowedAge,
                    out playerId,
                    out playerName,
                    out playerAge,
                    takenIds,
                    out string? description,
                    out object[]? additionalParams);
                if (!isInputCorrect)
                    ConsoleHandler.WriteLine(description, additionalParams);
            } while (!isInputCorrect);
        }

        /// <summary>Sets personal data of each player depending on his input.</summary>
        private void SetPlayersPersonalData()
        {
            int playerId;
            int playerAge;
            string playerName;
            List<int> takenIds = new();
            for (int i = 0; i < _playersCount; i++)
            {
                InputPlayerPersonalData(i + 1, takenIds, out playerId, out playerName, out playerAge);
                _players[i] = new(_userSymbols[i], playerName, playerId, playerAge);
                takenIds.Add(playerId);
            }
        }

        /// <summary>Draws current state of the game field.</summary>
        private void DrawGameField()
        {
            for (int i = 0; i < _fieldSize; i++)
            {
                for (int j = 0; j < _fieldSize - 1; j++)
                    ConsoleHandler.Write($"{_gameFieldSymbols[i, j]}{_verticalSeparator}");
                ConsoleHandler.WriteLine(_gameFieldSymbols[i, _fieldSize - 1]);
                if (i != _fieldSize - 1)
                {
                    for (int j = 0; j < _fieldSize - 1; j++)
                        ConsoleHandler.Write($"{_horizontalSeparator}{_horizontalSeparator}");
                    ConsoleHandler.WriteLine($"{_horizontalSeparator}{_horizontalSeparator}");
                }
            }
        }

        /// <summary>Processes all actions of one player turn.</summary>
        private void PerformOneUserTurn()
        {
            string? userInput;
            bool isCellNumberWrong;
            int attemptsLeftCount = _maxRetriesCount;
            do
            {
                ConsoleHandler.WriteLine(Messages.AskForPerformCurrentTurnMessage, _players[_userNumberForTurn].Name);
                userInput = ConsoleHandler.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                    isCellNumberWrong = true;
                else
                    isCellNumberWrong = !UserTurnCheck.IsUserTurnInputProper(userInput,
                        _fieldSize,
                        _turnInputSeparator,
                        ref _rowNumberForTurn,
                        ref _columnNumberForTurn,
                        _gameFieldSymbols,
                        _fieldSymbol);
                if (isCellNumberWrong)
                {
                    attemptsLeftCount--;
                    ConsoleHandler.WriteLine(Messages.WrongCellMessage, attemptsLeftCount);
                }
            } while (isCellNumberWrong && attemptsLeftCount != 0);
            if (attemptsLeftCount != 0)
            {
                _gameFieldSymbols[_rowNumberForTurn - 1, _columnNumberForTurn - 1] = _players[_userNumberForTurn].Symbol;
                _successfulTurnsCount++;
            }
        }

        /// <summary>
        /// Asks user to input one of suggested commands.
        /// </summary>
        /// <returns>The index of entered command among suggested commands</returns>
        private int GetUserCommandIndex()
        {
            ConsoleHandler.WriteLine(Messages.AskForEnterCommandMessage +
            $"\n{_commands[0]}:" + Messages.FirstCommandMessage +
            $"\n{_commands[1]}:" + Messages.SecondCommandMessage +
            $"\n{_commands[2]}:" + Messages.ThirdCommandMessage +
            $"\n{_commands[3]}:" + Messages.FourthCommandMessage);
            string? userGenerationCommand = ConsoleHandler.ReadLine();
            int userGenerationCommandIndex = Array.IndexOf(_commands, userGenerationCommand);
            return userGenerationCommandIndex;
        }

        /// <summary>
        /// Performs actions that correspond the command entered by the user.
        /// </summary>
        /// <param name="commandIndex">Index of command among all commands for user</param>
        /// <param name="reportState">String that represents state of report (if it was compiled successfully or some errors occured)</param>
        /// <param name="additionalOutputParams">Supplementing params for report state</param>
        private void ProcessUserCommand(int commandIndex, out string reportState, out object[]? additionalOutputParams)
        {
            reportState = null;
            additionalOutputParams = null;
            switch (commandIndex)
            {
                case 0:
                    JsonService.GenerateCurrentGameReport(GamesDBService._gamesDB, out reportState, out additionalOutputParams);
                    break;
                case 1:
                    JsonService.GenerateCurrentPlayerGamesReport(GamesDBService._gamesDB, _players, out reportState, out additionalOutputParams);
                    break;
                case 2:
                    JsonService.GenerateAllGamesReport(GamesDBService._gamesDB, out reportState, out additionalOutputParams);
                    break;
                case -1:
                    reportState = Messages.WrongCommandMessage;
                    additionalOutputParams = null;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Draws the final state of the game field and writes message about who won the game.
        /// </summary>
        /// <param name="message">Text of the message about winner</param>
        /// <param name="winnerName">Name of the winner of the game or null if the game was drawn</param>
        private void DrawGameEndInfo(string message, string? winnerName)
        {
            DrawGameField();
            ConsoleHandler.WriteLine(message, winnerName);
        }

        /// <summary>Determines if one of player won after current turn.</summary>
        /// <returns>Boolean value that actually is <c>true</c> when someone won after current turn and <c>false</c> otherwise</returns>
        private bool IsSomeoneWon()
        {
            for (int i = 0; i < _fieldSize; i++)       
            {
                char firstCharInRow = _gameFieldSymbols[i, 0];     
                if (firstCharInRow == _fieldSymbol)         
                    continue;       //If first symbol in row equals to default field symbol this row can't bring the win for someone
                for (int j = 1; j < _fieldSize; j++)        
                {
                    if (_gameFieldSymbols[i, j] != firstCharInRow)          
                        break;      //When a cell is occupied by symbol that doesn't match first symbol in a row then this row isn't lucky too
                    if (j == _fieldSize - 1)        
                        return true;        //If we looked up all cells in a row and didn't break the lookup, someone won.
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
                        break;      //If some symbol in a diagonal doesn't equals the first symbol of this diagonal, diagonal isn't lucky for player
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