using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Model.ViewModel
{
    
    /// <summary>
    ///   This class represents one player of a game.
    /// </summary>
    public class Player
    {
       
        ///<summary>The symbol which player places in the game field. This property hasn't its own field in DB</summary>
        [NotMapped]
        public char Symbol { get; private set; }

        ///<summary>The player's name</summary>
        public string Name { get; set; }

        ///<summary>The player's id</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; private set; }

        ///<summary>The player's age</summary>
        public int Age { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// This constructor is used when adding new player in game.
        /// </summary>
        public Player(char symbol, string name, int id, int age)
        {
            Symbol = symbol;    
            Name = name;
            Id = id;
            Age = age;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// This special constructor is used when adding new data to player database, because symbol field is omitted.
        /// </summary>
        public Player(string name, int id, int age)
        {
            Name = name;
            Id = id;
            Age = age;
        }
    }
}