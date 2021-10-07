namespace MikroTikMiniApi.Interfaces.Models.Settings
{
    /// <summary>
    /// Command execution settings.
    /// </summary>
    public interface IExecutionSettings
    {
        /// <summary>
        /// A sign that it is necessary to clean up the router's response stream.
        /// Clearing is called only if the type of the last API response was ApiTrapSentence.
        /// </summary>
        bool IsFlushResponseStream { get; }

        /// <summary>
        /// The number of sentences in the router's response stream that will be read if cleanup is enabled.
        /// </summary>
        uint AttemptsCount { get; }

        /// <summary>
        /// A sign that the router's response stream should be cleared before the ApiDoneSentence type of offer appears.
        /// If this parameter is "true", the value of the <see cref="AttemptsCount"/> parameter is ignored.
        /// </summary>
        bool FlushBeforeDoneSentence { get; }
    }
}