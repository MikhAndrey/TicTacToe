using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Model.ViewModel
{
    /// <summary>
    ///   This class represents one player of a game.
    /// </summary>
    public class Player
    {
        /// <summary>The symbol that player places in the field's cell. The feature of this property is that symbol isn't saved to players DB</summary>
        [NotMapped]
        public char Symbol { get; private set; }
        /// <summary>The player's name.</summary>
        public string Name { get; set; }
        /// <summary>The player's ID.</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; private set; }
        /// <summary>The player's age.</summary>
        public int Age { get; set; }
        /// <summary>This constructor is used to create new player after collecting all info about it.</summary>
        public Player(char symbol, string name, int id, int age)
        {
            Symbol = symbol;    
            Name = name;
            Id = id;
            Age = age;
        }
        /// <summary>This constructor is used when adding new data to player database, because symbol field is omitted.</summary>
        public Player(string name, int id, int age)
        {
            Name = name;
            Id = id;
            Age = age;
        }
    }
}