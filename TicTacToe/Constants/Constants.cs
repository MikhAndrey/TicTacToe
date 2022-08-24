namespace TicTacToe.Constants
{
    
    /// <summary>
    ///   This class contains all required game constants
    /// </summary>
    public static class Constants
    {
        
        /// <summary>
        /// Symbols that players place in the field.
        /// </summary>
        public const string PlayerSymbols = "xo";

        /// <summary>
        /// The game field size.
        /// </summary>
        public const int GameFieldSize = 3;

        /// <summary>
        /// The maximum allowed name length.
        /// </summary>
        public const int MaxAllowedNameLength = 25;

        /// <summary>
        /// The maximum allowed retries count for one turn.
        /// </summary>
        public const int MaxAllowedRetriesCount = 3;

        /// <summary>
        /// The number of players in one game.
        /// </summary>
        public const int PlayersCount = 2;

        /// <summary>
        /// The minimum allowed player's age
        /// </summary>
        public const int MinAllowedAge = 10;

        /// <summary>
        /// The maximum allowed player's age
        /// </summary>
        public const int MaxAllowedAge = 90;

        /// <summary>
        /// The symbol that separates two adjacent rows of the field.
        /// </summary>
        public const char HorizontalFieldSeparator = '-';

        /// <summary>
        /// The symbol that separates two adjacent columns of the field.
        /// </summary>
        public const char VerticalFieldSeparator = '|';

        /// <summary>
        /// The symbol that separates row and column number while turn.
        /// </summary>
        public const char UserTurnInputSeparator = ' ';

        /// <summary>
        /// The symbol that separates clusters of player's personal data in input.
        /// </summary>
        public const char UserDataInputSeparator = ' ';

        /// <summary>
        /// Default field symbol.
        /// </summary>
        public const char FieldSymbol = '.';

        /// <summary>
        /// Commands that help to generate JSON reports.
        /// </summary>
        public const string Commands = "/generatelastgameresult,/generateresultsforcurrentplayers,/generateallresults,/next";

        /// <summary>
        /// The supported languages abbreviations.
        /// </summary>
        public const string SupportedLanguagesAbbreviations = "ru,en";

        /// <summary>
        /// The database connection string name.
        /// </summary>
        public const string ConnectionStringName = "DefaultConnectionString";

        /// <summary>
        /// The JSON files names. These files contain game reports.
        /// </summary>
        public const string JSONFilesNames = "lastgameresult.json currentplayersgamesresults.json allgamesresults.json";

        /// <summary>
        /// The adjacent records in JSON-file separator.
        /// </summary>
        public const string AdjacentJSONRecordsSeparator = "\n";

        /// <summary>
        /// The supported languages abbreviations separator. It separates languages abbreviations here.
        /// </summary>
        public const char LanguagesAbbreviationsSeparator = ',';

        /// <summary>
        /// The file names separator. It separates JSON file names here.
        /// </summary>
        public const char FileNamesSeparator = ' ';

        /// <summary>
        /// The JSON files generation commands separator.
        /// </summary>
        public const char CommandsSeparator = ',';

        /// <summary>
        /// The languages names separator. It separates languages names in resx files.
        /// </summary>
        public const char LanguagesNamesSeparator = ' ';

        /// <summary>The user files directory path</summary>
        public const string UserFilesDirectoryPath = "../../../User Files";
    }
}

