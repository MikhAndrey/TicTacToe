namespace Players;
public class PlayerData
{
    public char Symbol{ get; set; }
    public string Name { get; private set; }

    public PlayerData(string name)
    {
        Name = name;
    }   
}