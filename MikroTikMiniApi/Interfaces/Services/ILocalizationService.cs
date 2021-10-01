using System;
using MikroTikMiniApi.Interfaces.Sentences;

namespace MikroTikMiniApi.Interfaces.Services
{
    internal interface ILocalizationService : IAuthenticationLocalizationService,
                                              ICommandExecutionLocalizationService,
                                              IConnectionLocalizationService,
                                              IApiSentenceLocalizationService
    {
    }

    internal interface IAuthenticationLocalizationService
    {
        string GetAuthCmdFailedText();

        string GetAuthFailedText(IApiSentence sentence, string response);

        string GetAuthFailedIncorrectAnswerText(IApiSentence sentence);

        string GetLogoutCmdFailedText();

        string GetLogoutFailedText(IApiSentence sentence, string response);
    }

    internal interface ICommandExecutionLocalizationService
    {
        string GetCmdSendingNotCompletedText();

        string GetSendingCmdDataNotCompletedText();

        string GetResponseNotReceivedText();

        string GetResponseStreamNotClearedText();

        string GetResponseWordNotReceivedText();

        string GetSentenceNameNotReceivedText();

        string GetWordLengthValueNotReceivedText();

        string GetRecvSeqNotCompleteUnknownRespTypeText(IApiSentence sentence, string response);

        string GetRecvSeqNotCompleteText(IApiSentence sentence, string response);
    }

    internal interface IConnectionLocalizationService
    {
        string GetConnectionTimeoutText(TimeSpan timeout);

        string GetDataSendingTimeoutText(TimeSpan timeout);

        string GetDataReceivingTimeoutText(TimeSpan timeout);

        string GetConnectionLostText();
    }

    internal interface IApiSentenceLocalizationService
    {
        string GetResponseTypeNotReceivedText(string response);

        string GetUnknownResponseTypeText(string sentenceName, string responce);

        string GetResponseIsEmptyText();
    }
}