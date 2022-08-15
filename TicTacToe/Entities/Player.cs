namespace TicTacToe.Entities;
public class Player
{
    public char Symbol{ get; set; }
    public string Name { get; private set; }
    public int Id { get; private set; }
    public int Age { get; private set; }    

    public Player(string name, int id, int age)
    {
        Name = name;
        Id = id;
        Age = age;
    }   
}