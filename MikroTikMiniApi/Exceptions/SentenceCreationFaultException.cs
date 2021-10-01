using System;

namespace MikroTikMiniApi.Exceptions
{
    public class SentenceCreationFaultException : Exception
    {
        public SentenceCreationFaultException(string message)
            : base(message)
        {
        }
    }
}