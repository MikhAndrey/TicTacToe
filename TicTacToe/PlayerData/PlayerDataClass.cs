public class PlayersData
{
    private char[] playersSymbols;

    private string[] playersNames;

    public PlayersData(int playersCount)
    {
        playersSymbols = new char[playersCount];
        playersNames = new string[playersCount];
    }
    public void SetPlayerSymbol(char playerSymbol, int playerNumber)
    {
        playersSymbols[playerNumber] = playerSymbol;
    }
    public char GetPlayerSymbol(int playerNumber)
    {
        return playersSymbols[playerNumber];
    }
    public void SetPlayerName(string playerName, int playerNumber)
    {
        playersNames[playerNumber] = playerName;
    }
    public string GetPlayerName(int playerNumber)
    {
        return playersNames[playerNumber];
    }
}