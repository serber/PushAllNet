using System.Collections.Generic;

namespace PushAll.Models
{
    /// <summary>
    /// PUSH message parameters model for multicast notifications
    /// </summary>
    public sealed class MulticastParameters : PushParameters
    {
        /// <summary>
        /// PUSH message recipients ids
        /// </summary>
        public ulong[] Recipients { get; set; }

        /// <summary>
        /// Converts <see cref="MulticastParameters"/> to dictionary
        /// </summary>
        /// <param name="parameters">Push message parameters</param>
        public static implicit operator Dictionary<string, string>(MulticastParameters parameters)
        {
            Dictionary<string, string> parametersDictionary = (PushParameters)parameters;

            parametersDictionary.Add("uids", $"[{string.Join(",", parameters.Recipients)}]");

            return parametersDictionary;
        }
    }
}