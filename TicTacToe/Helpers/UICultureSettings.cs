using System.Globalization;
using TicTacToe.Resources;

namespace TicTacToe.Helpers
{
    public static class UICultureSettings
    {
        public static void SetUICulture(string languagesAbbreviationsString = GameConstants.SupportedLanguagesAbbreviations)
        {
            string languagesNamesString = Messages.LanguagesNames;
            string[] languagesNames = languagesNamesString.Split(' ');
            string[] languagesAbbreviations = languagesAbbreviationsString.Split(',');
            int languagesCount = languagesNames.Length;
            while (true)
            {
                Console.WriteLine(Messages.SelectLanguageMessage);
                for (int i = 0; i < languagesCount; i++)
                    Console.WriteLine($"{languagesAbbreviations[i]} - {languagesNames[i]};");
                string? userLanguage = Console.ReadLine();
                if (!string.IsNullOrEmpty(userLanguage))
                {
                    userLanguage = userLanguage.Trim();
                    if (Array.IndexOf(languagesAbbreviations, userLanguage.ToLower()) != -1)
                    {
                        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo(userLanguage);
                        return;
                    }
                }
                Console.WriteLine("\n" + Messages.SelectLanguageRetryMessage + "\n");
            }
        }
    }
}
