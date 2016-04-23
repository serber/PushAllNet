using System.Collections.Generic;
using PushAll.Enums;

namespace PushAll.Models
{
    public class PushParameters
    {
        #region C-tor

        public PushParameters()
        {
            Priority = PushPriority.Default;
            Ttl = Constants.DefaultTtl;
        }

        #endregion

        #region Public properties

        public string Title { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public PushPriority Priority { get; set; }

        public ulong Ttl { get; set; }

        #endregion

        #region Public methods

        public static implicit operator Dictionary<string, string>(PushParameters parameters)
        {
            Dictionary<string, string> parametersDictionary = new Dictionary<string, string>
            {
                {"title", parameters.Title},
                {"text", parameters.Text}
            };

            if (!string.IsNullOrEmpty(parameters.Icon))
                parametersDictionary.Add("icon", parameters.Icon);

            if (!string.IsNullOrEmpty(parameters.Url))
                parametersDictionary.Add("url", parameters.Url);

            parametersDictionary.Add("priority", ((int)parameters.Priority).ToString());

            if (parameters.Ttl > 0)
                parametersDictionary.Add("ttl", parameters.Ttl.ToString());
            else
                parametersDictionary.Add("ttl", Constants.DefaultTtl.ToString());

            return parametersDictionary;
        }
        
        #endregion
    }
}