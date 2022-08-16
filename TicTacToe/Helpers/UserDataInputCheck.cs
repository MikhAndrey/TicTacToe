namespace TicTacToe.Helpers
{
    public static class UserDataInputCheck
    {
        public static bool isUserDataInputProper(string? inputString,
            char separator,
            int maxNameLength,
            int minAge,
            int maxAge,
            ref bool isInputCorrect,
            ref int id,
            ref string name,
            ref int age)
        {
            void ShowErrorMessage(string message)
            {
                Console.WriteLine(message);
                Console.ReadKey();
            }

            if (string.IsNullOrEmpty(inputString))
            {
                ShowErrorMessage("Your input's format is wrong. Please, try again");
                return false;
            }
            inputString = inputString.Trim();
            int firstSeparatorIndex = inputString.IndexOf(separator);
            int lastSeparatorIndex = inputString.LastIndexOf(separator);
            if (firstSeparatorIndex == -1 || lastSeparatorIndex == -1 || firstSeparatorIndex == lastSeparatorIndex)
            {
                ShowErrorMessage("Your input's format is wrong. Please, try again");
                return false;
            }
            bool isIdANumber = int.TryParse(inputString.Substring(0, firstSeparatorIndex), out int possibleId);
            if (!isIdANumber)
            {
                ShowErrorMessage("Your id must be an integer. Please, try again");
                return false;
            }
            id = possibleId;
            bool isAgeANumber = int.TryParse(inputString.Substring(lastSeparatorIndex + 1, inputString.Length - 1 - lastSeparatorIndex),
                out int possibleAge);
            if (!isAgeANumber)
            {
                ShowErrorMessage("Your age must be an integer. Please, try again");
                return false;
            }
            bool isAgeProper = possibleAge > minAge && possibleAge < maxAge;
            if (!isAgeProper)
            {
                ShowErrorMessage($"Your age must be between {minAge} and {maxAge}");
                return false;
            }
            age = possibleAge;
            string possiblePlayerName = inputString.Substring(firstSeparatorIndex + 1, lastSeparatorIndex - firstSeparatorIndex - 1);
            bool isNameProper = !string.IsNullOrEmpty(possiblePlayerName) && possiblePlayerName.Length <= maxNameLength;
            if (!isNameProper)
            {
                ShowErrorMessage($"Your name shouldn't be empty and its length can't exceed {maxNameLength} symbols");
                return false;
            }
            name = possiblePlayerName;
            return true;
        }
    }
}
