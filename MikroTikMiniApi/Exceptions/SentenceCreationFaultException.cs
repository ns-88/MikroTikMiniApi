using System;

namespace MikroTikMiniApi.Exceptions
{
    /// <summary>
    /// An exception is thrown if an error occurs when creating an API response sentence.
    /// </summary>
    public class SentenceCreationFaultException : Exception
    {
        public SentenceCreationFaultException(string message)
            : base(message)
        {
        }
    }
}