namespace Players;
public class PlayerData
{
    private char symbol;
    public char Symbol{
        set
        {
            symbol = value; 
        }
        get { 
            return symbol; 
        }  
    }

    private string name;
    public string Name
    {
        set { 
            name = value; 
        }
        get { 
            return name; 
        }    
    }
}