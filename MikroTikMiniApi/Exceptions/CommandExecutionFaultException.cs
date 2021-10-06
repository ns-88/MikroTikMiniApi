using System;

namespace MikroTikMiniApi.Exceptions
{
    /// <summary>
    /// An exception thrown in case of an error when executing a command.
    /// </summary>
    public class CommandExecutionFaultException : Exception
    {
        public CommandExecutionFaultException(string message)
            : base(message)
        {
        }

        public CommandExecutionFaultException(string message, Exception exception) 
            : base(message, exception)
        {
        }
    }
}