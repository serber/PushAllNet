using System.Collections.Generic;

namespace PushAll.Models
{
    public sealed class UnicastParameters : PushParameters
    {
        public ulong Recipient { get; set; }

        public static implicit operator Dictionary<string, string>(UnicastParameters parameters)
        {
            Dictionary<string, string> parametersDictionary = (PushParameters)parameters;

            parametersDictionary.Add("uid", parameters.Recipient.ToString());

            return parametersDictionary;
        }
    }
}