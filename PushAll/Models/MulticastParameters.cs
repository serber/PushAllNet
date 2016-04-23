using System.Collections.Generic;

namespace PushAll.Models
{
    public sealed class MulticastParameters : PushParameters
    {
        public ulong[] Recipients { get; set; }

        public static implicit operator Dictionary<string, string>(MulticastParameters parameters)
        {
            Dictionary<string, string> parametersDictionary = (PushParameters)parameters;

            parametersDictionary.Add("uids", $"[{string.Join(",", parameters.Recipients)}]");

            return parametersDictionary;
        }
    }
}