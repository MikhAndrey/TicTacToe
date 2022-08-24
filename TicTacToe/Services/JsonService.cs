using TicTacToe.Model.DBModels;
using TicTacToe.Resources;
using TicTacToe.Repositories.Interfaces;
using TicTacToe.Model.ViewModel;
using System.Text.Json;
using System.Text;

namespace TicTacToe.Services {

    /// <summary>
    ///   This service implements the generation of JSON files based on the command entered by the user 
    ///   and the selection of game information corresponding to this command from the database.
    /// </summary>
    public static class JsonService
    {
        /// <summary>
        /// JSON files names.
        /// </summary>
        private static string[] _fileNames = Constants.Constants.JSONFilesNames.Split(Constants.Constants.FileNamesSeparator);

        /// <summary>
        /// The adjacent records in file separator
        /// </summary>
        private static string _adjacentRecordsSeparator = Constants.Constants.AdjacentJSONRecordsSeparator;

        /// <summary>The user files directory path</summary>
        private static string _userFilesDirectoryPath = Constants.Constants.UserFilesDirectoryPath;

        private const string _filePathSeparator = "/";

        /// <summary>
        /// Generates the file with report of the current game.
        /// </summary>
        /// <param name="gamesDB">Еhe database where the data of the current game will be saved.</param>
        /// <param name="reportGeneratingState">State of the report generating.
        /// Describes whether the report was created successfully or some errrors occured.</param>
        /// <param name="additionalParams">The supplementing parameters for reportGeneratingState.</param>
        public static void GenerateCurrentGameReport(IRepository<GameDataForDB> gamesDB, 
            out string reportGeneratingState, 
            out object[]? additionalParams)
        {
            additionalParams = null;    
            List<GameDataForDB> gamesList;
            string jsonData;
            string filePath;
            int gamesCount;
            try
            {
                gamesList = gamesDB.GetList();
            }
            catch
            {
                reportGeneratingState = Messages.GameGetConnectionErrorMessage;
                return;
            }
            gamesCount = gamesList.Count();
            try
            {
                if (!Directory.Exists(_userFilesDirectoryPath))
                    Directory.CreateDirectory(_userFilesDirectoryPath);
                filePath = _userFilesDirectoryPath + _filePathSeparator + _fileNames[0];
                using (FileStream lastGameFile = new FileStream(filePath, FileMode.Create))
                {
                    jsonData = JsonSerializer.Serialize(gamesList[gamesCount - 1]) + _adjacentRecordsSeparator;
                    lastGameFile.Write(Encoding.Default.GetBytes(jsonData));
                    lastGameFile.Close();
                }
                reportGeneratingState = "\n" + Messages.LastGameSaveMessage + "\n";
                additionalParams = new object[] { _fileNames[0] };
            }
            catch
            {
                reportGeneratingState = "\n" + Messages.SaveToFileErrorMessage + "\n";
            }
        }

        /// <summary>
        /// Generates file with report about all games in which current players took part.
        /// </summary>
        /// <param name="gamesDB">The database with game data.</param>
        /// <param name="players">The current game players.</param>
        /// <param name="reportGeneratingState">State of the report generating.
        /// Describes whether the report was created successfully or some errrors occured.</param>
        /// <param name="additionalParams">The supplementing parameters for reportGeneratingState.</param>
        public static void GenerateCurrentPlayerGamesReport(IRepository<GameDataForDB> gamesDB, 
            Player[] players,
            out string reportGeneratingState,
            out object[]? additionalParams)
        {
            List<GameDataForDB> gamesList;
            string jsonData;
            string filePath;
            additionalParams = null;
            try
            {
                gamesList = gamesDB.GetList();
            }
            catch
            {
                reportGeneratingState = Messages.GameGetConnectionErrorMessage;
                return;
            }
            try
            {
                if (!Directory.Exists(_userFilesDirectoryPath))
                     Directory.CreateDirectory(_userFilesDirectoryPath);
                filePath = _userFilesDirectoryPath + _filePathSeparator + _fileNames[1];
                using (FileStream currentPlayersGamesFile = new FileStream(filePath, FileMode.Create))
                {
                    foreach (GameDataForDB game in gamesList)
                    {
                        bool isTheGameOfRequiredTwoPlayers = game.FirstPlayerId == players[0].Id && game.SecondPlayerId == players[1].Id;
                        bool isTheGameOfRequiredTwoPlayersReverse = game.FirstPlayerId == players[1].Id && game.SecondPlayerId == players[0].Id;
                        if (isTheGameOfRequiredTwoPlayers || isTheGameOfRequiredTwoPlayersReverse)
                        {
                            jsonData = JsonSerializer.Serialize(game) + _adjacentRecordsSeparator;
                            currentPlayersGamesFile.Write(Encoding.Default.GetBytes(jsonData));
                        }
                    }
                    currentPlayersGamesFile.Close();
                }
                reportGeneratingState = "\n" + Messages.GamesWithCurrentPlayersSaveMessage + "\n"; 
                additionalParams = new object[] { _fileNames[1] };
            }
            catch
            {
                reportGeneratingState = "\n" + Messages.SaveToFileErrorMessage + "\n";
            }
        }

        /// <summary>
        /// Generates file with report about all games.
        /// </summary>
        /// <param name="gamesDB">The database with games data.</param>
        /// <param name="reportGeneratingState">State of the report generating.
        /// Describes whether the report was created successfully or some errrors occured.</param>
        /// <param name="additionalParams">The supplementing parameters for reportGeneratingState.</param>
        public static void GenerateAllGamesReport(IRepository<GameDataForDB> gamesDB,
            out string reportGeneratingState,
            out object[]? additionalParams)
        {
            List<GameDataForDB> gamesList;
            string jsonData;
            string filePath;
            additionalParams = null;
            try
            {
                gamesList = gamesDB.GetList();
            }
            catch
            {
                reportGeneratingState = Messages.GameGetConnectionErrorMessage;
                return;
            }
            try
            {
                if (!Directory.Exists(_userFilesDirectoryPath))
                    Directory.CreateDirectory(_userFilesDirectoryPath);
                filePath = _userFilesDirectoryPath + _filePathSeparator + _fileNames[2];
                using (FileStream allGamesFile = new FileStream(filePath, FileMode.Create))
                {
                    foreach (GameDataForDB game in gamesList)
                    {
                        jsonData = JsonSerializer.Serialize(game) + _adjacentRecordsSeparator;
                        allGamesFile.Write(Encoding.Default.GetBytes(jsonData));
                    }
                    allGamesFile.Close();
                }
                reportGeneratingState = "\n" + Messages.AllGamesSaveMessage + "\n";
                additionalParams = new object[] { _fileNames[2] };
            }
            catch
            {
                reportGeneratingState = "\n" + Messages.SaveToFileErrorMessage + "\n";
            }
        }
    }
}
