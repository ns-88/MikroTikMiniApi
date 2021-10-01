﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MikroTikMiniApi.Resources {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MikroTikMiniApi.Resources.Strings", typeof(Strings).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The authentication command was not executed..
        /// </summary>
        internal static string AuthCmdFailed {
            get {
                return ResourceManager.GetString("AuthCmdFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Authentication was not performed. API response type: &quot;{0}&quot;. Response text: &quot;{1}&quot;..
        /// </summary>
        internal static string AuthFailed {
            get {
                return ResourceManager.GetString("AuthFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Authentication was not performed. The API response &quot;{0}&quot; was expected to be empty..
        /// </summary>
        internal static string AuthFailedIncorrectAnswer {
            get {
                return ResourceManager.GetString("AuthFailedIncorrectAnswer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The command sending was not completed..
        /// </summary>
        internal static string CmdSendingNotCompleted {
            get {
                return ResourceManager.GetString("CmdSendingNotCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Communication with the remote host is lost..
        /// </summary>
        internal static string ConnectionLost {
            get {
                return ResourceManager.GetString("ConnectionLost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The connection was not completed within the specified time period. Timeout: &quot;{0}&quot;..
        /// </summary>
        internal static string ConnectionTimeout {
            get {
                return ResourceManager.GetString("ConnectionTimeout", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The data was not received within the specified time period. Timeout: &quot;{0}&quot;..
        /// </summary>
        internal static string DataReceivingTimeout {
            get {
                return ResourceManager.GetString("DataReceivingTimeout", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The data was not sent within the specified time period. Timeout: &quot;{0}&quot;..
        /// </summary>
        internal static string DataSendingTimeout {
            get {
                return ResourceManager.GetString("DataSendingTimeout", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The logout command was not executed..
        /// </summary>
        internal static string LogoutCmdFailed {
            get {
                return ResourceManager.GetString("LogoutCmdFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The logout was not performed. API response type: &quot;{0}&quot;. Response text: &quot;{1}&quot;..
        /// </summary>
        internal static string LogoutFailed {
            get {
                return ResourceManager.GetString("LogoutFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Getting the sequence was not completed. API response type: &quot;{0}&quot;. Response text: &quot;{1}&quot;..
        /// </summary>
        internal static string RecvSeqNotComplete {
            get {
                return ResourceManager.GetString("RecvSeqNotComplete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Getting the sequence was not completed - unknown type of API response. Response type: &quot;{0}&quot;. Response text: &quot;{1}&quot;..
        /// </summary>
        internal static string RecvSeqNotCompleteUnknownRespType {
            get {
                return ResourceManager.GetString("RecvSeqNotCompleteUnknownRespType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The response text is empty.
        /// </summary>
        internal static string ResponseIsEmpty {
            get {
                return ResourceManager.GetString("ResponseIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The API response was not received..
        /// </summary>
        internal static string ResponseNotReceived {
            get {
                return ResourceManager.GetString("ResponseNotReceived", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The API response stream was not cleared..
        /// </summary>
        internal static string ResponseStreamNotCleared {
            get {
                return ResourceManager.GetString("ResponseStreamNotCleared", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The API response type was not received. Response text: &quot;{0}&quot;..
        /// </summary>
        internal static string ResponseTypeNotReceived {
            get {
                return ResourceManager.GetString("ResponseTypeNotReceived", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The API response word value was not received..
        /// </summary>
        internal static string ResponseWordNotReceived {
            get {
                return ResourceManager.GetString("ResponseWordNotReceived", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The sending of the command data was not completed..
        /// </summary>
        internal static string SendingCmdDataNotCompleted {
            get {
                return ResourceManager.GetString("SendingCmdDataNotCompleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The name of the sentence type was not received..
        /// </summary>
        internal static string SentenceNameNotReceived {
            get {
                return ResourceManager.GetString("SentenceNameNotReceived", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Unknown API response type. Response type: &quot;{0}&quot;. Response text: &quot;{1}&quot;..
        /// </summary>
        internal static string UnknownResponseType {
            get {
                return ResourceManager.GetString("UnknownResponseType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The API word length value was not received..
        /// </summary>
        internal static string WordLengthValueNotReceived {
            get {
                return ResourceManager.GetString("WordLengthValueNotReceived", resourceCulture);
            }
        }
    }
}