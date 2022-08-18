namespace TicTacToe.Helpers
{
    public static class UserTurnCheck
    {
        public static bool IsUserTurnInputProper(string input, 
            int fieldSize, 
            char separator, 
            ref int rowNumber, 
            ref int columnNumber, 
            char[,] fieldSymbols,
            char initialFieldSymbol)
        {
            string[] properValues = new string[fieldSize];
            for (int i = 0; i < fieldSize; i++)
                properValues[i] = (i + 1).ToString();
            input = input.Trim();
            int firstSpaceIndex = input.IndexOf(separator);
            if (firstSpaceIndex == -1)
                return false;
            int lastSpaceIndex = input.LastIndexOf(separator);
            string possibleStringRowNumber = input.Substring(0, firstSpaceIndex);
            string possibleStringColumnNumber = input.Substring(lastSpaceIndex + 1, input.Length - 1 - lastSpaceIndex);
            string stringBeforeColumnAndRowNumber = input.Substring(firstSpaceIndex, lastSpaceIndex - firstSpaceIndex + 1);
            bool isInputProper = string.IsNullOrWhiteSpace(stringBeforeColumnAndRowNumber) && properValues.Contains(possibleStringRowNumber) && properValues.Contains(possibleStringColumnNumber);
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
