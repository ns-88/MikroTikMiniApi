using System;
using MikroTikMiniApi.Interfaces.Sentences;
using MikroTikMiniApi.Interfaces.Services;
using MikroTikMiniApi.Resources;

namespace MikroTikMiniApi.Services
{
    internal class LocalizationService : ILocalizationService
    {
        private static string GetTypeName(IApiSentence sentence) => sentence.GetType().Name;

        #region IAuthenticationLocalizationService

        public string GetAuthCmdFailedText()
        {
            return Strings.AuthCmdFailed;
        }

        public string GetAuthFailedText(IApiSentence sentence, string response)
        {
            return string.Format(Strings.AuthFailed, GetTypeName(sentence), response);
        }

        public string GetAuthFailedIncorrectAnswerText(IApiSentence sentence)
        {
            return string.Format(Strings.AuthFailedIncorrectAnswer, GetTypeName(sentence));
        }

        public string GetLogoutCmdFailedText()
        {
            return Strings.LogoutCmdFailed;
        }

        public string GetLogoutFailedText(IApiSentence sentence, string response)
        {
            return string.Format(Strings.LogoutFailed, GetTypeName(sentence), response);
        }

        #endregion

        #region ICommandExecutionLocalizationService

        public string GetCmdSendingNotCompletedText()
        {
            return Strings.CmdSendingNotCompleted;
        }

        public string GetSendingCmdDataNotCompletedText()
        {
            return Strings.SendingCmdDataNotCompleted;
        }

        public string GetResponseNotReceivedText()
        {
            return Strings.ResponseNotReceived;
        }

        public string GetResponseStreamNotClearedText()
        {
            return Strings.ResponseStreamNotCleared;
        }

        public string GetResponseWordNotReceivedText()
        {
            return Strings.ResponseWordNotReceived;
        }

        public string GetSentenceNameNotReceivedText()
        {
            return Strings.SentenceNameNotReceived;
        }

        public string GetWordLengthValueNotReceivedText()
        {
            return Strings.WordLengthValueNotReceived;
        }

        public string GetRecvSeqNotCompleteUnknownRespTypeText(IApiSentence sentence, string response)
        {
            return string.Format(Strings.RecvSeqNotCompleteUnknownRespType, sentence, response);
        }

        public string GetRecvSeqNotCompleteText(IApiSentence sentence, string response)
        {
            return string.Format(Strings.RecvSeqNotComplete, GetTypeName(sentence), response);
        }

        #endregion

        #region IConnectionLocalizationService

        public string GetConnectionTimeoutText(TimeSpan timeout)
        {
            return string.Format(Strings.ConnectionTimeout, timeout);
        }

        public string GetDataSendingTimeoutText(TimeSpan timeout)
        {
            return string.Format(Strings.DataSendingTimeout, timeout);
        }

        public string GetDataReceivingTimeoutText(TimeSpan timeout)
        {
            return string.Format(Strings.DataReceivingTimeout, timeout);
        }

        public string GetConnectionLostText()
        {
            return string.Format(Strings.ConnectionLost);
        }

        #endregion

        #region IApiSentenceLocalizationService

        public string GetResponseTypeNotReceivedText(string response)
        {
            return string.Format(Strings.ResponseTypeNotReceived, response);
        }

        public string GetUnknownResponseTypeText(string sentenceName, string responce)
        {
            return string.Format(Strings.UnknownResponseType, sentenceName, responce);
        }

        public string GetResponseIsEmptyText()
        {
            return Strings.ResponseIsEmpty;
        }

        #endregion
    }
}