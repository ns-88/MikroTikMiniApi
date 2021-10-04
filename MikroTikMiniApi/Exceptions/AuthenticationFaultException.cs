using System;

namespace MikroTikMiniApi.Exceptions
{
    /// <summary>
    /// An exception thrown in case of an authentication error or logout.
    /// </summary>
    public class AuthenticationFaultException : Exception
    {
        public AuthenticationFaultException(string message)
            : base(message)
        {
        }
    }
}