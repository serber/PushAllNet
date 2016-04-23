using System;

namespace PushAll.Exceptions
{
    public class PushAllApiException : Exception
    {
        public PushAllApiException(string message) : base(message)
        {
        }

        public PushAllApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}