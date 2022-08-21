namespace TicTacToe.Constants
{
    /// <summary>
    ///   This class contains all required game constants
    /// </summary>
    public static class Constants
    {
        /// <summary>The player symbols stored in one string</summary>
        public const string TicTacToeSymbols = "xo";
        /// <summary>The game field size</summary>
        public const int GameFieldSize = 3;
        /// <summary>The maximum allowed player name length</summary>
        public const int MaxAllowedNameLength = 25;
        /// <summary>The maximum allowed retries for one turn</summary>
        public const int MaxAllowedRetriesCount = 3;
        /// <summary>The players count</summary>
        public const int PlayersCount = 2;
        /// <summary>The minimum allowed player age</summary>
        public const int MinAllowedAge = 10;
        /// <summary>The maximum allowed player age</summary>
        public const int MaxAllowedAge = 90;
        /// <summary>The horizontal field separator. Separates adjacent field horizontals.</summary>
        public const char HorizontalFieldSeparator = '-';
        /// <summary>The vertical field separator. Separates adjacent field verticals.</summary>
        public const char VerticalFieldSeparator = '|';
        /// <summary>The symbol that separates the row and column numbers of the cell where the player wants to make a turn</summary>
        public const char UserTurnInputSeparator = ' ';
        /// <summary>The symbol that separates player id, name and age</summary>
        public const char UserDataInputSeparator = ' ';
        /// <summary>The symbol that indicates an unoccupied cell.</summary>
        public const char FieldSymbol = '.';
        /// <summary>Default commands for JSON files generating.</summary>
        public const string JSONGenerationCommands = "/generatelastgameresult,/generateresultsforcurrentplayers,/generateallresults,/next";
        /// <summary>Abbreviatures of game supported languages.</summary>
        public const string SupportedLanguagesAbbreviations = "ru,en,fr,de,es,it,nl,pt,ar,zh,ja";
    }
}

