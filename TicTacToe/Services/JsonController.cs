using TicTacToe.Model.DBModels;
using TicTacToe.Resources;
using TicTacToe.Repositories.Interfaces;
using TicTacToe.Model.ViewModel;
using TicTacToe.Constants;
using System.Text.Json;
using System.Text;

namespace TicTacToe.Services { 
    public static class JsonController
    {
        private static string[] _jsonCommands = Constants.Constants.JSONGenerationCommands.Split(',');
        public static void GenerateJsonReports(IRepository<GameDataForDB> gamesDB, Player[] players)
        {
            List<GameDataForDB> gamesList;
            string fileName;
            string jsonData;
            while (true)
            {
                Console.WriteLine(Messages.AskForEnterCommandMessage +
                    $"\n{_jsonCommands[0]}:" + Messages.FirstCommandMessage +
                    $"\n{_jsonCommands[1]}:" + Messages.SecondCommandMessage +
                    $"\n{_jsonCommands[2]}:" + Messages.ThirdCommandMessage +
                    $"\n{_jsonCommands[3]}:" + Messages.FourthCommandMessage);
                string? userGenerationCommand = Console.ReadLine();
                int userGenerationCommandIndex = Array.IndexOf(_jsonCommands, userGenerationCommand);
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
                            using (FileStream lastGameFile = new FileStream(fileName, FileMode.OpenOrCreate))
                            {
                                lastGameFile.SetLength(0);
                                jsonData = JsonSerializer.Serialize(gamesList[gamesCount - 1]) + "\n";
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
                            using (FileStream currentPlayersGamesFile = new FileStream(fileName, FileMode.OpenOrCreate))
                            {
                                currentPlayersGamesFile.SetLength(0);
                                foreach (GameDataForDB game in gamesList)
                                {
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
                            using (FileStream allGamesFile = new FileStream(fileName, FileMode.OpenOrCreate))
                            {
                                allGamesFile.SetLength(0);
                                foreach (GameDataForDB game in gamesList)
                                {
                                    jsonData = JsonSerializer.Serialize(game) + "\n";
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
