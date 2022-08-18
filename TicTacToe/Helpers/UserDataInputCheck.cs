using TicTacToe.Resources;
namespace TicTacToe.Helpers
{
    public static class UserDataInputCheck
    {
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
            void ShowErrorMessage(string message, params object[] values)
            {
                Console.WriteLine(message,values);
            }

            if (string.IsNullOrEmpty(inputString))
            {
                ShowErrorMessage(Messages.WrongInputFormatMessage);
                return false;
            }
            inputString = inputString.Trim();
            int firstSeparatorIndex = inputString.IndexOf(separator);
            int lastSeparatorIndex = inputString.LastIndexOf(separator);
            if (firstSeparatorIndex == -1 || lastSeparatorIndex == -1 || firstSeparatorIndex == lastSeparatorIndex)
            {
                ShowErrorMessage(Messages.WrongInputFormatMessage);
                return false;
            }
            bool isIdANumber = int.TryParse(inputString.Substring(0, firstSeparatorIndex), out int possibleId);
            if (!isIdANumber)
            {
                ShowErrorMessage(Messages.NonIntegerIdMessage);
                return false;
            }
            bool idIsUnique = !currentIds.Contains(possibleId);
            if (!idIsUnique)
            {
                ShowErrorMessage(Messages.BookedIdErrorMessage);
                return false;
            }
            id = possibleId;
            bool isAgeANumber = int.TryParse(inputString.Substring(lastSeparatorIndex + 1, inputString.Length - 1 - lastSeparatorIndex),
                out int possibleAge);
            if (!isAgeANumber)
            {
                ShowErrorMessage(Messages.NonIntegerAgeMessage);
                return false;
            }
            bool isAgeProper = possibleAge > minAge && possibleAge < maxAge;
            if (!isAgeProper)
            {
                ShowErrorMessage(Messages.WrongAgeMessage,minAge,maxAge);
                return false;
            }
            age = possibleAge;
            string possiblePlayerName = inputString.Substring(firstSeparatorIndex + 1, lastSeparatorIndex - firstSeparatorIndex - 1);
            bool isNameProper = !string.IsNullOrEmpty(possiblePlayerName) && possiblePlayerName.Length <= maxNameLength;
            if (!isNameProper)
            {
                ShowErrorMessage(Messages.WrongNameLengthMessage, maxNameLength);
                return false;
            }
            name = possiblePlayerName;
            return true;
        }
    }
}
