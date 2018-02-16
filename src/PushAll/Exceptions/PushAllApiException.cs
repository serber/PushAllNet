using System;

namespace PushAll.Exceptions
{
    /// <summary>
    /// PushAll exception
    /// </summary>
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