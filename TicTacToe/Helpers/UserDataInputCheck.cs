using TicTacToe.Resources;

namespace TicTacToe.Helpers
{
    /// <summary>
    ///   This class provides method to check, was the player data input proper
    /// </summary>
    public static class UserDataInputCheck
    {
        /// <summary>Determines whether is user data input proper.</summary>
        /// <param name="inputString">The player input string.</param>
        /// <param name="separator">The symbol that separates data clusters in input.</param>
        /// <param name="maxNameLength">Maximum length of the player name.</param>
        /// <param name="minAge">The minimum required player age.</param>
        /// <param name="maxAge">The maximum required player age.</param>
        /// <param name="id">The player identifier.</param>
        /// <param name="name">The player name.</param>
        /// <param name="age">The player age.</param>
        /// <param name="currentIds">The current taken player IDs.</param>
        /// <returns>
        ///   <c>true</c> if [is user data input proper] [the specified input string]; otherwise, <c>false</c>.</returns>
        public static bool isUserDataInputProper(string? inputString,
            char separator,
            int maxNameLength,
            int minAge,
            int maxAge,
            ref int id,
            ref string name,
            ref int age,
            List<int> currentIds)
        {
            //Shows error message about what actually wrong with user input.
            //Values are additional params (if any) that correspond to placeholders in message
            void ShowErrorMessage(string message, params object[] values)
            {
                Console.WriteLine(message, values);
            }

            if (string.IsNullOrEmpty(inputString))      //If input is null or empty, it obviously wrong
            {
                ShowErrorMessage(Messages.WrongInputFormatMessage);
                return false;
            }
            inputString = inputString.Trim();
            int firstSeparatorIndex = inputString.IndexOf(separator);
            //If there is no required separators in input (should be at least two), the input is wrong
            if (firstSeparatorIndex == -1)
            {
                ShowErrorMessage(Messages.WrongInputFormatMessage);
                return false;
            }
            int lastSeparatorIndex = inputString.LastIndexOf(separator);
            //The same thing if the separator in input is only one
            if (firstSeparatorIndex == lastSeparatorIndex)
            {
                ShowErrorMessage(Messages.WrongInputFormatMessage);
                return false;
            }
            //The string that pretends to be a user name
            string possiblePlayerName = inputString.Substring(firstSeparatorIndex + 1, lastSeparatorIndex - 1 - firstSeparatorIndex);
            //If it's null the input is incorrect 
            if (string.IsNullOrEmpty(possiblePlayerName))
            {
                ShowErrorMessage(Messages.WrongInputFormatMessage);
                return false;
            }
            possiblePlayerName = possiblePlayerName.Trim();
            //If trimmed string contains one more separator inside, the input is wrong too. 
            int firstSeparatorIndexBetweenIdAndAge = possiblePlayerName.IndexOf(separator);
            if (firstSeparatorIndexBetweenIdAndAge != -1)
            {
                ShowErrorMessage(Messages.WrongInputFormatMessage);
                return false;
            }
            //The last test for name is its length. If it's improper then input is incorrect. 
            bool isNameLengthProper = possiblePlayerName.Length <= maxNameLength;
            if (!isNameLengthProper)
            {
                ShowErrorMessage(Messages.WrongNameLengthMessage, maxNameLength);
                return false;
            }
            name = possiblePlayerName;
            //There we try to convert to integer the first part of our input
            bool isIdANumber = int.TryParse(inputString.Substring(0, firstSeparatorIndex), out int possibleId);
            //If conversion wasn't successful, input is incorrect
            if (!isIdANumber)
            {
                ShowErrorMessage(Messages.NonIntegerIdMessage);
                return false;
            }
            //Also we need to check that id isn't taken by someone else
            bool idIsUnique = !currentIds.Contains(possibleId);
            if (!idIsUnique)
            {
                ShowErrorMessage(Messages.BookedIdErrorMessage);
                return false;
            }
            id = possibleId;
            //Here we check if the last part of user input is a number
            bool isAgeANumber = int.TryParse(inputString.Substring(lastSeparatorIndex + 1, inputString.Length - 1 - lastSeparatorIndex),
                out int possibleAge);
            //If no, input is improper
            if (!isAgeANumber)
            {
                ShowErrorMessage(Messages.NonIntegerAgeMessage);
                return false;
            }
            //The last test is if the age less then minimum required or greater then maximum required
            bool isAgeProper = possibleAge > minAge && possibleAge < maxAge;
            if (!isAgeProper)
            {
                ShowErrorMessage(Messages.WrongAgeMessage,minAge,maxAge);
                return false;
            }
            age = possibleAge;
            //If all tests were passed, the input is proper
            return true;
        }
    }
}
