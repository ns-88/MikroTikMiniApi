﻿using System;

namespace MikroTikMiniApi.Exceptions
{
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