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
        /// <param name="rowNumber">The expected player cell's row number</param>
        /// <param name="columnNumber">The expected player cell's column number</param>
        /// <param name="fieldSymbols">Symbols that describe game field's current state</param>
        /// <param name="initialFieldSymbol">The initial field symbol</param>
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
            string[] properRowAndColumnValues = new string[fieldSize];
            for (int i = 0; i < fieldSize; i++)
                properRowAndColumnValues[i] = (i + 1).ToString();
            input = input.Trim();
            int firstSeparatorIndex = input.IndexOf(separator);     
            if (firstSeparatorIndex == -1)      
                return false;      
            int lastSeparatorIndex = input.LastIndexOf(separator);
            string possibleStringRowNumber = input.Substring(0, firstSeparatorIndex);       
            possibleStringRowNumber = possibleStringRowNumber.Trim();
            string possibleStringColumnNumber = input.Substring(lastSeparatorIndex + 1, input.Length - 1 - lastSeparatorIndex);
            possibleStringColumnNumber = possibleStringColumnNumber.Trim();
            string stringBeforeColumnAndRowNumber = input.Substring(firstSeparatorIndex, lastSeparatorIndex - firstSeparatorIndex + 1);

            /*The part of input before first separator should be a row number,
            The part of input after last separator should be a column number,
            The rest of input should be just a separator or whitespace if separator is a whitespace*/
            bool isInputProper = (stringBeforeColumnAndRowNumber == separator.ToString() || string.IsNullOrWhiteSpace(stringBeforeColumnAndRowNumber)) 
                && properRowAndColumnValues.Contains(possibleStringRowNumber) && properRowAndColumnValues.Contains(possibleStringColumnNumber);
            if (!isInputProper)
                return false;
            else
            {
                rowNumber = int.Parse(possibleStringRowNumber);
                columnNumber = int.Parse(possibleStringColumnNumber);
                if (fieldSymbols[rowNumber - 1, columnNumber - 1] == initialFieldSymbol)
                    return true;        
                else
                    return false;
            }
        }
    }
}
