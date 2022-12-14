//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicTacToe.Resources {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TicTacToe.Resources.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Information about all games saved to file {0}.
        /// </summary>
        public static string AllGamesSaveMessage {
            get {
                return ResourceManager.GetString("AllGamesSaveMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Player {0}, enter your id, name and age, separated by space.
        /// </summary>
        public static string AskForDataInputMessage {
            get {
                return ResourceManager.GetString("AskForDataInputMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Please enter one of the following commands:.
        /// </summary>
        public static string AskForEnterCommandMessage {
            get {
                return ResourceManager.GetString("AskForEnterCommandMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Player {0}, enter the row number and column number of the cell you would like to occupy, separated by a space.
        /// </summary>
        public static string AskForPerformCurrentTurnMessage {
            get {
                return ResourceManager.GetString("AskForPerformCurrentTurnMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на This ID is already taken. Try again.
        /// </summary>
        public static string BookedIdErrorMessage {
            get {
                return ResourceManager.GetString("BookedIdErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Player {0}&apos;s turn.
        /// </summary>
        public static string CurrentTurnPlayerDeclarationMessage {
            get {
                return ResourceManager.GetString("CurrentTurnPlayerDeclarationMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Draw!.
        /// </summary>
        public static string DrawMessage {
            get {
                return ResourceManager.GetString("DrawMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на generate file with last game&apos;s review.
        /// </summary>
        public static string FirstCommandMessage {
            get {
                return ResourceManager.GetString("FirstCommandMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на go to the choice between ending the game and starting a new game.
        /// </summary>
        public static string FourthCommandMessage {
            get {
                return ResourceManager.GetString("FourthCommandMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на An error occurred while connecting to the database. Unable to get game data.
        /// </summary>
        public static string GameGetConnectionErrorMessage {
            get {
                return ResourceManager.GetString("GameGetConnectionErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на An error occurred while connecting to the database. Unable to write game data.
        /// </summary>
        public static string GameSetConnectionErrorMessage {
            get {
                return ResourceManager.GetString("GameSetConnectionErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Information about all games between current players is saved to file {0}.
        /// </summary>
        public static string GamesWithCurrentPlayersSaveMessage {
            get {
                return ResourceManager.GetString("GamesWithCurrentPlayersSaveMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Russian English.
        /// </summary>
        public static string LanguagesNames {
            get {
                return ResourceManager.GetString("LanguagesNames", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Information about the last game was successfully saved to the file {0}.
        /// </summary>
        public static string LastGameSaveMessage {
            get {
                return ResourceManager.GetString("LastGameSaveMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Your age must be an integer. Please, try again.
        /// </summary>
        public static string NonIntegerAgeMessage {
            get {
                return ResourceManager.GetString("NonIntegerAgeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Your id must be an integer. Please, try again.
        /// </summary>
        public static string NonIntegerIdMessage {
            get {
                return ResourceManager.GetString("NonIntegerIdMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на An error occurred while connecting to the database. Unable to write player data.
        /// </summary>
        public static string PlayerConnectionErrorMessage {
            get {
                return ResourceManager.GetString("PlayerConnectionErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Do you want to play again? (Enter - yes, any other key - no).
        /// </summary>
        public static string RepeatConfirmMessage {
            get {
                return ResourceManager.GetString("RepeatConfirmMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на An error occurred while writing data to file.
        /// </summary>
        public static string SaveToFileErrorMessage {
            get {
                return ResourceManager.GetString("SaveToFileErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на generate file with current players games review.
        /// </summary>
        public static string SecondCommandMessage {
            get {
                return ResourceManager.GetString("SecondCommandMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Please enter one of the suggested character combinations to select the language.
        /// </summary>
        public static string SelectLanguageMessage {
            get {
                return ResourceManager.GetString("SelectLanguageMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на You have entered the wrong combination of characters. Try again.
        /// </summary>
        public static string SelectLanguageRetryMessage {
            get {
                return ResourceManager.GetString("SelectLanguageRetryMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на generate file with all games review.
        /// </summary>
        public static string ThirdCommandMessage {
            get {
                return ResourceManager.GetString("ThirdCommandMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Player {0} won!.
        /// </summary>
        public static string WinnerDeclarationMessage {
            get {
                return ResourceManager.GetString("WinnerDeclarationMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Your age must be greater than {0} and less than {1} years.
        /// </summary>
        public static string WrongAgeMessage {
            get {
                return ResourceManager.GetString("WrongAgeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на You entered the data in the wrong format or the cell you wanted to occupy is already occupied. There are {0} attempts left before the move goes to the opponent.
        /// </summary>
        public static string WrongCellMessage {
            get {
                return ResourceManager.GetString("WrongCellMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на You have entered the wrong command.
        /// </summary>
        public static string WrongCommandMessage {
            get {
                return ResourceManager.GetString("WrongCommandMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Your input&apos;s format is wrong. Please, try again.
        /// </summary>
        public static string WrongInputFormatMessage {
            get {
                return ResourceManager.GetString("WrongInputFormatMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Your name shouldn&apos;t be empty and its length can&apos;t exceed {0} symbols.
        /// </summary>
        public static string WrongNameLengthMessage {
            get {
                return ResourceManager.GetString("WrongNameLengthMessage", resourceCulture);
            }
        }
    }
}
