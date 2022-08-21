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
        /// <summary>The json commands. By default we take them from constants</summary>
        private static string[] _jsonCommands = Constants.Constants.JSONGenerationCommands.Split(',');
        /// <summary>Generates the json reports.</summary>
        /// <param name="gamesDB">Current games database.</param>
        /// <param name="players">Players for current game. We need their data to select proper games from database</param>
        public static void GenerateJsonReports(IRepository<GameDataForDB> gamesDB, Player[] players)
        {
            List<GameDataForDB> gamesList;
            string fileName;
            string jsonData;
            while (true)
            {
                Console.WriteLine(Messages.AskForEnterCommandMessage +      //Write in console what options related to file generation are available
                    $"\n{_jsonCommands[0]}:" + Messages.FirstCommandMessage +
                    $"\n{_jsonCommands[1]}:" + Messages.SecondCommandMessage +
                    $"\n{_jsonCommands[2]}:" + Messages.ThirdCommandMessage +
                    $"\n{_jsonCommands[3]}:" + Messages.FourthCommandMessage);
                string? userGenerationCommand = Console.ReadLine();
                int userGenerationCommandIndex = Array.IndexOf(_jsonCommands, userGenerationCommand);       //Try to find user input among available commands
                switch (userGenerationCommandIndex)
                {
                    case 0:
                        int gamesCount;
                        try
                        {
                            gamesList = gamesDB.GetList();
                        }
                        catch
                        {
                            Console.WriteLine(Messages.GameGetConnectionErrorMessage);
                            break;
                        }
                        gamesCount = gamesList.Count();
                        try
                        {
                            fileName = "lastgameresult.json";
                            using (FileStream lastGameFile = new FileStream(fileName, FileMode.Create))
                            {
                                jsonData = JsonSerializer.Serialize(gamesList[gamesCount - 1]) + "\n";      //Choose only the last game info to serialie
                                lastGameFile.Write(Encoding.Default.GetBytes(jsonData));
                                lastGameFile.Close();
                            }
                            Console.WriteLine("\n" + Messages.LastGameSaveMessage + "\n", fileName);
                        }
                        catch
                        {
                            Console.WriteLine("\n" + Messages.SaveToFileErrorMessage + "\n");
                        }
                        break;
                    case 1:
                        try
                        {
                            gamesList = gamesDB.GetList();
                        }
                        catch
                        {
                            Console.WriteLine(Messages.GameGetConnectionErrorMessage);
                            break;
                        }
                        try
                        {
                            fileName = "currentplayersgamesresults.json";
                            using (FileStream currentPlayersGamesFile = new FileStream(fileName, FileMode.Create))
                            {
                                currentPlayersGamesFile.SetLength(0);
                                foreach (GameDataForDB game in gamesList)
                                {
                                    //Find out whether the player IDs were the same (probably in reverse order)
                                    bool isTheGameOfRequiredTwoPlayers = game.FirstPlayerId == players[0].Id && game.SecondPlayerId == players[1].Id;
                                    bool isTheGameOfRequiredTwoPlayersReverse = game.FirstPlayerId == players[1].Id && game.SecondPlayerId == players[0].Id;
                                    if (isTheGameOfRequiredTwoPlayers || isTheGameOfRequiredTwoPlayersReverse)
                                    {
                                        jsonData = JsonSerializer.Serialize(game) + "\n";
                                        currentPlayersGamesFile.Write(Encoding.Default.GetBytes(jsonData));
                                    }
                                }
                                currentPlayersGamesFile.Close();
                            }
                            Console.WriteLine("\n" + Messages.GamesWithCurrentPlayersSaveMessage + "\n", fileName);
                        }
                        catch
                        {
                            Console.WriteLine("\n" + Messages.SaveToFileErrorMessage + "\n");
                        }
                        break;
                    case 2:
                        try
                        {
                            gamesList = gamesDB.GetList();
                        }
                        catch
                        {
                            Console.WriteLine(Messages.GameGetConnectionErrorMessage);
                            break;
                        }
                        try
                        {
                            fileName = "allgamesresults.json";
                            using (FileStream allGamesFile = new FileStream(fileName, FileMode.Create))
                            {
                                foreach (GameDataForDB game in gamesList)
                                {
                                    jsonData = JsonSerializer.Serialize(game) + "\n";       //Just save  every game result to file
                                    allGamesFile.Write(Encoding.Default.GetBytes(jsonData));
                                }
                                allGamesFile.Close();
                            }
                            Console.WriteLine("\n" + Messages.AllGamesSaveMessage + "\n", fileName);
                        }
                        catch
                        {
                            Console.WriteLine("\n" + Messages.SaveToFileErrorMessage + "\n");
                        }
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine(Messages.WrongCommandMessage);
                        break;
                }
            }
        }
    }
}
