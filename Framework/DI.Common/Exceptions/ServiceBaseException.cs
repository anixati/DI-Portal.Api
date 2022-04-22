using System;

namespace DI.Exceptions
{
    public abstract class ServiceBaseException : Exception
    {
        protected ServiceBaseException(string message) : base(message)
        {
        }
    }
}