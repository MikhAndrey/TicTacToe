namespace TicTacToe.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class Player
{
    [NotMapped]
    public char Symbol{ get; set; }
    public string Name { get;set; }
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public int Id { get; private set; }
    public int Age { get; set; }    

    public Player(string name, int id, int age)
    {
        Name = name;
        Id = id;
        Age = age;
    }   
}