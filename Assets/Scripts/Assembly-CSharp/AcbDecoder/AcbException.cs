using System;

namespace AcbDecoder
{
    public sealed class AcbException : Exception
    {
        public AcbException(string message) : base(message)
        {
        }

        public AcbException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
