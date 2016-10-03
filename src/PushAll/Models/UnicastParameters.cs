using System.Collections.Generic;

namespace PushAll.Models
{
    /// <summary>
    /// PUSH message parameters model for unicast notifications
    /// </summary>
    public sealed class UnicastParameters : PushParameters
    {
        /// <summary>
        /// PUSH message recipient id
        /// </summary>
        public ulong Recipient { get; set; }

        /// <summary>
        /// Converts <see cref="UnicastParameters"/> to dictionary
        /// </summary>
        /// <param name="parameters">Push message parameters</param>
        public static implicit operator Dictionary<string, string>(UnicastParameters parameters)
        {
            Dictionary<string, string> parametersDictionary = (PushParameters)parameters;

            parametersDictionary.Add("uid", parameters.Recipient.ToString());

            return parametersDictionary;
        }
    }
}