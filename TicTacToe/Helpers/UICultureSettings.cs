using System.Globalization;

namespace TicTacToe.Helpers
{
    public static class UICultureSettings
    {
        public static void SetInitialUICulture()
        {
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
        }
        public static void SetUICulture(string languagesNamesString = GameConstants.SupportedLanguagesNames,
            string languagesAbbreviationsString = GameConstants.SupportedLanguagesAbbreviations)
        {
            string[] languagesNames = languagesNamesString.Split(',');
            string[] languagesAbbreviations = languagesAbbreviationsString.Split(',');
            int languagesCount = languagesNames.Length;
            while (true)
            {
                Console.WriteLine("Please, enter one of the the suggested character sequences to choose your language:");
                for (int i = 0; i < languagesCount; i++)
                    Console.WriteLine($"{languagesAbbreviations[i]} - {languagesNames[i]};");
                string? userLanguage = Console.ReadLine();
                if (!string.IsNullOrEmpty(userLanguage) && Array.IndexOf(languagesAbbreviations, userLanguage) != -1)
                {
                    CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(userLanguage);
                    return;
                }
                Console.WriteLine("\nYou have entered wrong characters sequence. Please, try again\n");
            }
        }
    }
}
