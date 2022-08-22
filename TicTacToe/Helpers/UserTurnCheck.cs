namespace TicTacToe.Helpers
{
    /// <summary>
    ///   This class provides method that checks if user entered row number and column number properly
    /// </summary>
    public static class UserTurnCheck
    {
        /// <summary>Determines whether user turn input proper.</summary>
        /// <param name="input">The input string.</param>
        /// <param name="fieldSize">Size of the field. We need it to check if the column or row number is proper</param>
        /// <param name="separator">The separator for input.</param>
        /// <param name="rowNumber">The row number.</param>
        /// <param name="columnNumber">The column number.</param>
        /// <param name="fieldSymbols">The current game field symbols.</param>
        /// <param name="initialFieldSymbol">The initial field symbol that is in each cell of game field when game starts.</param>
        /// <returns>
        ///   <c>true</c> if is user turn input proper; otherwise, <c>false</c>.</returns>
        public static bool IsUserTurnInputProper(string input, 
            int fieldSize, 
            char separator, 
            ref int rowNumber, 
            ref int columnNumber, 
            char[,] fieldSymbols,
            char initialFieldSymbol)
        {
            string[] properValues = new string[fieldSize];
            for (int i = 0; i < fieldSize; i++)     //Fill properValues with string conversions of required column/row number 
                properValues[i] = (i + 1).ToString();
            input = input.Trim();
            int firstSeparatorIndex = input.IndexOf(separator);     
            if (firstSeparatorIndex == -1)      //If there is no separators in input, it's improper
                return false;
            int lastSeparatorIndex = input.LastIndexOf(separator);
            //The part of input that can be considered as a row number
            string possibleStringRowNumber = input.Substring(0, firstSeparatorIndex);       
            possibleStringRowNumber = possibleStringRowNumber.Trim();
            //The part of input that can be considered as a column number
            string possibleStringColumnNumber = input.Substring(lastSeparatorIndex + 1, input.Length - 1 - lastSeparatorIndex);
            possibleStringColumnNumber = possibleStringColumnNumber.Trim();
            //The string before first and last separator. If the separator is space it's allowed to be WhiteSpace string, otherwise it should be equal to separator.toString()
            string stringBeforeColumnAndRowNumber = input.Substring(firstSeparatorIndex, lastSeparatorIndex - firstSeparatorIndex + 1);
            //Check additionally if row and column numbers are in required range
            bool isInputProper = (stringBeforeColumnAndRowNumber == separator.ToString() || string.IsNullOrWhiteSpace(stringBeforeColumnAndRowNumber)) && properValues.Contains(possibleStringRowNumber) && properValues.Contains(possibleStringColumnNumber);
            if (!isInputProper)
                return false;
            else
            {
                //If everything is OK we just convert specific input parts into numbers and additionally check, if required cell is already occupied
                rowNumber = int.Parse(possibleStringRowNumber);
                columnNumber = int.Parse(possibleStringColumnNumber);
                if (fieldSymbols[rowNumber - 1, columnNumber - 1] == initialFieldSymbol)
                    return true;        //If no, input is proper. Otherwise - no.
                else
                    return false;
            }
        }
    }
}
