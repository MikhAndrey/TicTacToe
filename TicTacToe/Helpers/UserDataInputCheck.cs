using TicTacToe.Resources;

namespace TicTacToe.Helpers
{
    
    /// <summary>
    ///   This class provides method to check, was the player data entered properply
    /// </summary>
    public static class UserDataInputCheck
    {

        /// <summary>
        /// Determines whether [is user data input proper] [the specified input string].
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="maxAllowedPlayerNameLength">Maximum allowed length of the player name.</param>
        /// <param name="minAllowedPlayerAge">The minimum allowed player age.</param>
        /// <param name="maxAllowedPlayerAge">The maximum allowed player age.</param>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="playerAge">The player age.</param>
        /// <param name="takenPlayerIds">The taken player ID's.</param>
        /// <param name="inputFaultDescription">The string that contains info about missmatches in the input (if any).</param>
        /// <param name="additionalParams">The additional parameters that supplement inputFaultDescription.</param>
        /// <returns>
        ///   <c>true</c> if [is user data input proper] [the specified input string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUserDataInputProper(string? inputString,
            char separator,
            int maxAllowedPlayerNameLength,
            int minAllowedPlayerAge,
            int maxAllowedPlayerAge,
            out int playerId,
            out string playerName,
            out int playerAge,
            List<int> takenPlayerIds, 
            out string? inputFaultDescription,
            out object[]? additionalParams)
        {
            additionalParams = null;
            inputFaultDescription = null;
            playerId = 0;
            playerAge = 0;
            playerName = "";
            if (string.IsNullOrEmpty(inputString))      
            {
                inputFaultDescription = Messages.WrongInputFormatMessage;
                return false;
            }
            inputString = inputString.Trim();

            int firstSeparatorIndex = inputString.IndexOf(separator);
            //If there is no required separators in input (should be at least two), the input is wrong
            if (firstSeparatorIndex == -1)
            {
                inputFaultDescription = Messages.WrongInputFormatMessage;
                return false;
            }

            int lastSeparatorIndex = inputString.LastIndexOf(separator);
            if (firstSeparatorIndex == lastSeparatorIndex)
            {
                inputFaultDescription = Messages.WrongInputFormatMessage;
                return false;
            }
            
            //The string that pretends to be a user name
            string possiblePlayerName = inputString.Substring(firstSeparatorIndex + 1, lastSeparatorIndex - 1 - firstSeparatorIndex);
            if (string.IsNullOrEmpty(possiblePlayerName))
            {
                inputFaultDescription = Messages.WrongInputFormatMessage;
                return false;
            }

            possiblePlayerName = possiblePlayerName.Trim();
            int firstSeparatorIndexBetweenIdAndAge = possiblePlayerName.IndexOf(separator);
            //Trimmed player's name can't contain the separator inside. 
            if (firstSeparatorIndexBetweenIdAndAge != -1)
            {
                inputFaultDescription = Messages.WrongInputFormatMessage;
                return false;
            }

            bool isNameLengthProper = possiblePlayerName.Length <= maxAllowedPlayerNameLength;
            if (!isNameLengthProper)
            {
                inputFaultDescription = Messages.WrongNameLengthMessage;
                additionalParams = new object[]{maxAllowedPlayerNameLength};
                return false;
            }
            playerName = possiblePlayerName;

            //Part of input before first separator should be player's id
            bool isIdANumber = int.TryParse(inputString.Substring(0, firstSeparatorIndex), out int possibleId);
            if (!isIdANumber)
            {
                inputFaultDescription = Messages.NonIntegerIdMessage;
                return false;
            }

            //If id is already taken, we consider input as wrong
            bool idIsUnique = !takenPlayerIds.Contains(possibleId);
            if (!idIsUnique)
            {
                inputFaultDescription = Messages.BookedIdErrorMessage;
                return false;
            }
            playerId = possibleId;

            //Part of input after last separator should be player's age 
            bool isAgeANumber = int.TryParse(inputString.Substring(lastSeparatorIndex + 1, inputString.Length - 1 - lastSeparatorIndex),
                out int possibleAge);
            if (!isAgeANumber)
            {
                inputFaultDescription = Messages.NonIntegerAgeMessage;
                return false;
            }

            bool isAgeProper = possibleAge > minAllowedPlayerAge && possibleAge < maxAllowedPlayerAge;
            if (!isAgeProper)
            {
                additionalParams = new object[] { minAllowedPlayerAge, maxAllowedPlayerAge};
                inputFaultDescription = Messages.WrongAgeMessage;
                return false;
            }
            playerAge = possibleAge;
            return true;        
        }
    }
}
