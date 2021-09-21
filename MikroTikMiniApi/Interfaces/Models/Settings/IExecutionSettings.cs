namespace MikroTikMiniApi.Interfaces.Models.Settings
{
    public interface IExecutionSettings
    {
        bool IsFlushResponseStream { get; }
        uint AttemptsCount { get; }
        bool FlushBeforeDoneSentence { get; }
    }
}