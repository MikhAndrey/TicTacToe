namespace Players;
public class PlayerData
{
    private char _symbol;
    public char Symbol{
        set => _symbol = value;
        get => _symbol;   
    }

    private string _name;
    public string Name
    {
        set => _name = value; 
        get => _name;    
    }
}