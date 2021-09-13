using System;

namespace MikroTikMiniApi.Exceptions
{
    public class AuthenticationFaultException : Exception
    {
        public AuthenticationFaultException(string message)
            : base(message)
        {
        }
    }
}