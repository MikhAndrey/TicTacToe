namespace TicTacToe.Utils
{
    
    /// <summary>
    /// Performs basic actions with <see cref="Console"/> object. 
    /// </summary>
    public static class ConsoleHandler
    {
        
        /// <summary>
        /// Writes the specified message in the console.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="values">The supplementing objects for message.</param>
        public static void Write(object message, params object[]? values)
        {
            if (message != null)
                Console.Write(message.ToString(), values);
        }

        /// <summary>
        /// Reads one character from the console.
        /// </summary>
        /// <returns>Read character.</returns>
        public static int? Read()
        {
            int? input = Console.Read();
            return input;
        }

        /// <summary>
        /// Writes the specified message in the console ending with \n.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        /// <param name="values">The supplementing objects for message.</param>
        public static void WriteLine(object message, params object[]? values)
        {
            if (message != null)
                Console.WriteLine (message.ToString(), values);
        }

        /// <summary>
        /// Reads one line from the console.
        /// </summary>
        /// <returns>String that represents read line.</returns>
        public static string? ReadLine()
        {
            string? input = Console.ReadLine();
            return input;   
        }

        /// <summary>
        /// Reads the key pressed by the user.
        /// </summary>
        /// <returns><see cref="ConsoleKeyInfo"/> object that describes key pressed by the user.</returns>
        public static ConsoleKeyInfo ReadKey()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            return key;
        }
    }
}
