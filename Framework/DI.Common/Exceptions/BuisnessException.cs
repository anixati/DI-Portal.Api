using System;

namespace DI.Exceptions
{
    public class BuisnessException : Exception
    {
        public BuisnessException(string message) : base(message)
        {
        }
    }
}