using System.Runtime.CompilerServices;
using MikroTikMiniApi.Interfaces.Models.Settings;

namespace MikroTikMiniApi.Models.Settings
{
    ///<inheritdoc cref="IExecutionSettings"/>
    public class ExecutionSettings : IExecutionSettings
    {
        public static readonly ExecutionSettings Default;

        ///<inheritdoc/>
        public bool IsFlushResponseStream { get; private set; }

        ///<inheritdoc/>
        public uint AttemptsCount { get; private set; }

        ///<inheritdoc/>
        public bool FlushBeforeDoneSentence { get; private set; }

        static ExecutionSettings()
        {
            Default = new ExecutionSettings(true);
        }

        public ExecutionSettings(bool isFlushResponseStream)
        {
            IsFlushResponseStream = isFlushResponseStream;
            AttemptsCount = isFlushResponseStream ? 1U : 0;
            FlushBeforeDoneSentence = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static IExecutionSettings GetValueOrDefault(IExecutionSettings? settings) => settings ?? Default;

        #region Builder

        public ExecutionSettingsBuilder Builder => new(this);

        public class ExecutionSettingsBuilder
        {
            private readonly ExecutionSettings _settings;

            public ExecutionSettingsBuilder(IExecutionSettings settings)
            {
                _settings = new ExecutionSettings(settings.IsFlushResponseStream)
                {
                    AttemptsCount = settings.AttemptsCount,
                    FlushBeforeDoneSentence = settings.FlushBeforeDoneSentence
                };
            }

            public ExecutionSettingsBuilder WithIsFlushResponseStream(bool isFlushResponseStream)
            {
                _settings.IsFlushResponseStream = isFlushResponseStream;
                return this;
            }

            public ExecutionSettingsBuilder WithAttemptsCount(uint attemptsCount)
            {
                _settings.AttemptsCount = attemptsCount;
                return this;
            }

            public ExecutionSettingsBuilder WithFlushBeforeDoneSentence(bool flushBeforeDoneSentence)
            {
                _settings.FlushBeforeDoneSentence = flushBeforeDoneSentence;
                return this;
            }

            public IExecutionSettings Build()
            {
                return _settings;
            }
        }

        #endregion
    }
}