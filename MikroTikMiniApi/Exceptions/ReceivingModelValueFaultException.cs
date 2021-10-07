using System;

namespace MikroTikMiniApi.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an error occurs when getting the value of a model field.
    /// </summary>
    public class ReceivingModelValueFaultException : Exception
    {
        public ReceivingModelValueFaultException(string message)
            : base(message)
        {
        }
    }
}