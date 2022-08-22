using TicTacToe.Helpers;
using TicTacToe.Resources;
using TicTacToe.Model.ViewModel;
using TicTacToe.Services;
using System.Globalization;

namespace TicTacToe.Launchers {
    /// <summary>
    ///   This class describes all the main actions that take place during the game.
    /// </summary>
    public class TicTacToeGame
    {
        /// <summary>The players who actually play current game.</summary>
        private Player[] _players;
        /// <summary>The symbol that initially stands in each cell of the field.</summary>
        private char _fieldSymbol;
        /// <summary>This parameter describes current state of the game field (i.e. what character is currently in a particular cell of the field).</summary>
        private char[,] _gameFieldSymbols;
        /// <summary>Game field size.</summary>
        private int _fieldSize;
        /// <summary>Maximum allowed player name length.</summary>
        private int _maxNameLength;
        /// <summary>The maximum allowed number of attempts to enter the row and column numbers of the cell in which the user wants to put his character. 
        /// If after this number of attempts the correct input has not been made, the turn passes to the opponent.</summary>
        private int _maxRetriesCount;
        /// <summary>Symbols with which players fill the playing field (each player has its own symbol).</summary>
        private string _userSymbols;
        /// <summary>Number of players taking part in the game.</summary>
        private int _playersCount;
        /// <summary>The number of the player who is currently take a turn.</summary>
        private int _userNumberForTurn = 0;
        /// <summary>The number of turns, after each of which the field was supplemented with one new symbol. 
        /// When it becomes equal to the number of rows of the field times the number of columns of the field, and neither player has won, a draw is declared.</summary>
        private int _successfulTurnsCount = 0;
        /// <summary>The row number of sell that the current player wants to iccuoy.</summary>
        private int _rowNumberForTurn;
        /// <summary>The column number of sell that current player wants to occupy.</summary>
        private int _columnNumberForTurn;
        /// <summary>The symbol that separates adjacent horizontals of the game field.</summary>
        private char _horizontalSeparator;
        /// <summary>The symbol that separates adjacent verticals of the game field.</summary>
        private char _verticalSeparator;
        /// <summary>The symbol that separates the row and column numbers of the cell where current player wants to make a turn.</summary>
        private char _turnInputSeparator;
        /// <summary>The symbol that separates player id, name and age when input is performed.</summary>
        private char _userDataInputSeparator;
        /// <summary>The minimum allowed player age.</summary>
        private int _minAllowedAge;
        /// <summary>The maximum allowed player age.</summary>
        private int _maxAllowedAge;
        /// <summary>The game start date</summary>
        private DateTime _gameStartDate;
        /// <summary>The game end date</summary>
        private DateTime _gameEndDate;
        /// <summary>Abbreviations of supported languages. 
        /// We need to know them in order to allow the user to enter one of them to select the language in which the game should be played.</summary>
        private string[] _languagesAbbreviations;
        /// <summary>Here we initialize private properties with corresponding params that are taken from constants by default.</summary>
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
            _gameStartDate = DateTime.Now;      //Saving game start date as an exact time of now.
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
            _languagesAbbreviations = languagesAbbreviations.Split(',');        //As it is actually a string, we need to convert it to array of abbreviations
            for (int i = 0; i < _fieldSize; i++)        //Filling the game field by default symbol
                for (int j = 0; j < _fieldSize; j++)
                    _gameFieldSymbols[i, j] = _fieldSymbol;
            SetUICulture();         //Selecting game language
            SetUsersPersonalData();     //Setting player data such as ID, name and age
            PlayersDBService.UpdatePlayersDB(_playersCount,_players);       //Using PlayersDBService we update info about players in DB
        }
        /// <summary>This method is called when we want to play a new game and it contains all necessary method calls inside to complete one game.</summary>
        public void Play()
        {
            LaunchGame();       //Play current game until someone wins, otherwise draw is declared
            JsonService.GenerateJsonReports(GamesDBService._gamesDB, _players);         //Optionally, players can generate certain types of game reports in JSON format
            ConfirmGameRepeat();        //By calling this method we are asking the players if they want to restart the game or exit the console.
        }
        /// <summary>This method sets game UI language.</summary>
        private void SetUICulture()
        {
            string languagesNamesString = Messages.LanguagesNames;      //Full languages names.
                                                                        //We store them in resx languages files to display them in current UI culture language or in English, if current UI culture isn't supported.
            string[] languagesNames = languagesNamesString.Split(' ');  //Languages names are stored as a string, so we need to separate each language name
            int languagesCount = languagesNames.Length;
            while (true)
            {
                Console.WriteLine(Messages.SelectLanguageMessage);      //Displaying language select options for players
                for (int i = 0; i < languagesCount; i++)
                    Console.WriteLine($"{_languagesAbbreviations[i]} - {languagesNames[i]};");
                string? userLanguage = Console.ReadLine();
                if (!string.IsNullOrEmpty(userLanguage))        //If user input is null or empty string we show him following message and the whole process repeats
                {
                    userLanguage = userLanguage.Trim();
                    if (Array.IndexOf(_languagesAbbreviations, userLanguage.ToLower()) != -1)       //Otherwise we try to convert user input to one of languages abbreviations
                    {
                        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(userLanguage);        //If convertion was successful we set UI culture and exit the method
                        return;
                    }
                }
                Console.WriteLine("\n" + Messages.SelectLanguageRetryMessage + "\n");       //This message is shown when user input is null or empty or does not match any language abbreviation
            }
        }
        /// <summary>This method plays current game until someone wins, otherwise draw is declared.</summary>
        private void LaunchGame()
        {
            /// <summary>The internal method that describes what will happen after someone won or the draw was declared </summary>
            /// <param name = "message">The string that displays info about winner or draw.</param>
            /// <param name = "winnerName">The name of game winner or null if the current game was drawn.</param>
            /// <param name = "winnerId">The ID of game winner or null if the current game was drawn.</param>
            void FinishCurrentGame(string message, string? winnerName, int? winnerId)
            {
                _gameEndDate = DateTime.Now;        //Save game end date as a time of now
                DrawGameField();        //Draw game field to show visually who won the game
                Console.WriteLine(message, winnerName);     //Write a message about who won the game
                GamesDBService.UpdateGamesDB(_gameStartDate, _gameEndDate, _userSymbols, _players, winnerId);       //Add info about current game to database
            }
            while (_successfulTurnsCount < _fieldSize * _fieldSize)     //It means while there are left some cell not occupied by a player
            {
                WriteUserNumberForTurn();       //Write info about what player currently takes a turn
                DrawGameField();        //Draw current game field state    
                PerformOneUserTurn();       //One of players makes a turn       
                if (IsSomeoneWon())     //If someone won after turn we finish current game and display ID and name of a player who took the last turn
                {
                    FinishCurrentGame(Messages.WinnerDeclarationMessage, _players[_userNumberForTurn].Name, _players[_userNumberForTurn].Id);
                    return;
                }
                _userNumberForTurn = _userNumberForTurn == _playersCount - 1 ? 0 : _userNumberForTurn + 1;      //If the game continues, we update number of a player who takes the next turn
            }
            FinishCurrentGame(Messages.DrawMessage, null, null);    //If all field is covered with player symbols and none of players won, the draw is declared
        }
        /// <summary>This method sets personal data of each player.</summary>
        private void SetUsersPersonalData()
        {
            bool correctUserInput;      //We need it to check whether the player entered requested info correctly
            string? possiblePlayerPersonalData;         //the string that stores current user input
            //We need playerId, playerName and playerAge params to save info about player and later create a new player object by these params 
            //We set them some default values, because if user enters required data in a correct form, it immediately saved to these variables.
            //So that's why we need to use ref to these variables while checking if user input is proper. And it requires some default values to these variables.
            string playerName = "";     
            int playerId = 0;
            int playerAge = 0;
            List<int> bookedIds = new();        //Here we store ID's of players that were already taken
            for (int i = 0; i < _playersCount; i++)
            {
                do
                {
                    Console.WriteLine(Messages.AskForDataInputMessage, i + 1);      //Here we ask user to enter his info
                    possiblePlayerPersonalData = Console.ReadLine();
                    correctUserInput = UserDataInputCheck.isUserDataInputProper(possiblePlayerPersonalData,     //Here we check if user input is proper
                        _userDataInputSeparator,
                        _maxNameLength,
                        _minAllowedAge,
                        _maxAllowedAge,
                        ref playerId,
                        ref playerName,
                        ref playerAge,
                        bookedIds);
                } while (!correctUserInput);
                _players[i] = new(_userSymbols[i], playerName, playerId, playerAge);        //If user input is proper we add new player
                bookedIds.Add(playerId);        //And mark his ID as taken
            }
        }
        /// <summary>This method writes in the console message about which player currently takes the turn.</summary>
        private void WriteUserNumberForTurn()
        {
            Console.WriteLine(Messages.CurrentTurnPlayerDeclarationMessage, _players[_userNumberForTurn].Name);
        }
        /// <summary>This method draws current state of the game field.</summary>
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
        /// <summary>This method contains all actions of one player turn.</summary>
        private void PerformOneUserTurn()
        {
            string? userInput;      //Stores user input
            bool wrongFieldNumberInput;         //Determines if user input is proper
            int attemptsLeftCount = _maxRetriesCount;       //sets attempts left count to a default value of maximum retries number
            do
            {
                //Here we ask user to enter info about his turn
                Console.WriteLine(Messages.AskForPerformCurrentTurnMessage, _players[_userNumberForTurn].Name);         
                userInput = Console.ReadLine();
                //And if user input is just null or empty string we immediately set improper input flag to 
                if (string.IsNullOrEmpty(userInput))
                    wrongFieldNumberInput = true;
                else
                    //Otherwise we check if user input is proper. And if it is then we just fill required cell by player symbol
                    wrongFieldNumberInput = !UserTurnCheck.IsUserTurnInputProper(userInput,
                        _fieldSize,
                        _turnInputSeparator,
                        ref _rowNumberForTurn,
                        ref _columnNumberForTurn,
                        _gameFieldSymbols,
                        _fieldSymbol);
                if (wrongFieldNumberInput)      //If the input wasn't proper we throw following message to user and decrease the number of attempts left
                {
                    attemptsLeftCount--;
                    Console.WriteLine(Messages.WrongCellMessage, attemptsLeftCount);
                }
            } while (wrongFieldNumberInput && attemptsLeftCount != 0);      //Exit do...while when there are no attempts left or user input is proper
            if (attemptsLeftCount != 0)     //i.e. if user input is proper
            {
                //Fill one more game field cell and mark this turn as successful
                _gameFieldSymbols[_rowNumberForTurn - 1, _columnNumberForTurn - 1] = _players[_userNumberForTurn].Symbol;
                _successfulTurnsCount++;
            }
        }
        /// <summary>This method determines if one of player won after current turn.</summary>
        /// <returns>Boolean value that actually is <c>true</c> when someone won after current turn and <c>false</c> otherwise</returns>
        private bool IsSomeoneWon()
        {
            for (int i = 0; i < _fieldSize; i++)        //Game field horizontals lookup
            {
                char firstCharInRow = _gameFieldSymbols[i, 0];      //Remember first symbol in row 
                if (firstCharInRow == _fieldSymbol)         //If this symbol equals to default symbol then this row can't bring the win for someone
                    continue;
                for (int j = 1; j < _fieldSize; j++)        //Otherwise we have a look at other cells in a row 
                {
                    //When a cell is occupied by symbol that doesn't match first symbol in a row then this row isn't lucky
                    if (_gameFieldSymbols[i, j] != firstCharInRow)          
                        break;
                    if (j == _fieldSize - 1)        //If we looked up all cells in a row and didn't break the lookup, it means that someone won
                        return true;
                }
            }
            //The same lookup for verticals
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
            //Lookup for diagonals has the same principle.
            //If the first symbol in diagonal doesn't belong to any player then no one won.
            //Otherwise if we find some symbol that doesn't match first diagonal symbol, the diagonal turns out to be unlucky
            //Otherwise we completed diagonal lookup, and all symbol of diagonals are belong to someone and are equal. E.g. someone won
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
        /// <summary>In this method we ask players do they want to play the game again or exit.</summary>
        private void ConfirmGameRepeat()
        {
            Console.WriteLine(Messages.RepeatConfirmMessage);
            ConsoleKey confirmKey = Console.ReadKey().Key;     //Here we read key that user have pressed
            if (confirmKey != ConsoleKey.Enter)         //If this key isn't Enter we exit the app. Otherwise new game starts
                Environment.Exit(0);
            else
                return;
        }
    };
}