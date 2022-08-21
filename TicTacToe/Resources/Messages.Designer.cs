namespace TicTacToe.Resources {
    using System;


    /// <summary>
    ///   A resource class with strict typing for searching localized strings.
    /// </summary>
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
        ///   Returns the cached instance of ResourceManager used by this class.
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
        ///   Overwrites the CurrentUICulture property of the current stream for all
        /// resource accesses using this strongly typed resource class.
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
        ///   This method and all methods below are looking for a localized string similar to the one specified inside the method.
        /// </summary>
        public static string AllGamesSaveMessage {
            get {
                return ResourceManager.GetString("AllGamesSaveMessage", resourceCulture);
            }
        }
       
        public static string AskForDataInputMessage {
            get {
                return ResourceManager.GetString("AskForDataInputMessage", resourceCulture);
            }
        }
        
        public static string AskForEnterCommandMessage {
            get {
                return ResourceManager.GetString("AskForEnterCommandMessage", resourceCulture);
            }
        }
       
        public static string AskForPerformCurrentTurnMessage {
            get {
                return ResourceManager.GetString("AskForPerformCurrentTurnMessage", resourceCulture);
            }
        }
       
        public static string BookedIdErrorMessage {
            get {
                return ResourceManager.GetString("BookedIdErrorMessage", resourceCulture);
            }
        }
      
        public static string CurrentTurnPlayerDeclarationMessage {
            get {
                return ResourceManager.GetString("CurrentTurnPlayerDeclarationMessage", resourceCulture);
            }
        }
       
        public static string DrawMessage {
            get {
                return ResourceManager.GetString("DrawMessage", resourceCulture);
            }
        }
       
        public static string FirstCommandMessage {
            get {
                return ResourceManager.GetString("FirstCommandMessage", resourceCulture);
            }
        }
       
        public static string FourthCommandMessage {
            get {
                return ResourceManager.GetString("FourthCommandMessage", resourceCulture);
            }
        }
       
        public static string GameGetConnectionErrorMessage {
            get {
                return ResourceManager.GetString("GameGetConnectionErrorMessage", resourceCulture);
            }
        }
       
        public static string GameSetConnectionErrorMessage {
            get {
                return ResourceManager.GetString("GameSetConnectionErrorMessage", resourceCulture);
            }
        }
      
        public static string GamesWithCurrentPlayersSaveMessage {
            get {
                return ResourceManager.GetString("GamesWithCurrentPlayersSaveMessage", resourceCulture);
            }
        }
     
        public static string LanguagesNames {
            get {
                return ResourceManager.GetString("LanguagesNames", resourceCulture);
            }
        }
      
        public static string LastGameSaveMessage {
            get {
                return ResourceManager.GetString("LastGameSaveMessage", resourceCulture);
            }
        }
       
        public static string NonIntegerAgeMessage {
            get {
                return ResourceManager.GetString("NonIntegerAgeMessage", resourceCulture);
            }
        }
        
        public static string NonIntegerIdMessage {
            get {
                return ResourceManager.GetString("NonIntegerIdMessage", resourceCulture);
            }
        }
     
        public static string PlayerConnectionErrorMessage {
            get {
                return ResourceManager.GetString("PlayerConnectionErrorMessage", resourceCulture);
            }
        }
      
        public static string RepeatConfirmMessage {
            get {
                return ResourceManager.GetString("RepeatConfirmMessage", resourceCulture);
            }
        }
      
        public static string SaveToFileErrorMessage {
            get {
                return ResourceManager.GetString("SaveToFileErrorMessage", resourceCulture);
            }
        }
     
        public static string SecondCommandMessage {
            get {
                return ResourceManager.GetString("SecondCommandMessage", resourceCulture);
            }
        }
    
        public static string SelectLanguageMessage {
            get {
                return ResourceManager.GetString("SelectLanguageMessage", resourceCulture);
            }
        }
        
        public static string SelectLanguageRetryMessage {
            get {
                return ResourceManager.GetString("SelectLanguageRetryMessage", resourceCulture);
            }
        }
      
        public static string ThirdCommandMessage {
            get {
                return ResourceManager.GetString("ThirdCommandMessage", resourceCulture);
            }
        }
     
        public static string WinnerDeclarationMessage {
            get {
                return ResourceManager.GetString("WinnerDeclarationMessage", resourceCulture);
            }
        }
       
        public static string WrongAgeMessage {
            get {
                return ResourceManager.GetString("WrongAgeMessage", resourceCulture);
            }
        }
       
        public static string WrongCellMessage {
            get {
                return ResourceManager.GetString("WrongCellMessage", resourceCulture);
            }
        }
      
        public static string WrongCommandMessage {
            get {
                return ResourceManager.GetString("WrongCommandMessage", resourceCulture);
            }
        }
       
        public static string WrongInputFormatMessage {
            get {
                return ResourceManager.GetString("WrongInputFormatMessage", resourceCulture);
            }
        }
      
        public static string WrongNameLengthMessage {
            get {
                return ResourceManager.GetString("WrongNameLengthMessage", resourceCulture);
            }
        }
    }
}
