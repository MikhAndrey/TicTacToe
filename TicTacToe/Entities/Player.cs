using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Entities
{
    public class Player
    {
        [NotMapped]
        public char Symbol { get; private set; }
        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; private set; }
        public int Age { get; set; }
        public Player(char symbol, string name, int id, int age)
        {
            Symbol = symbol;    
            Name = name;
            Id = id;
            Age = age;
        }
    }
}