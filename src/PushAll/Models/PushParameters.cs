using System.Collections.Generic;
using PushAll.Enums;

namespace PushAll.Models
{
    /// <summary>
    /// PUSH message parameters model
    /// </summary>
    public class PushParameters
    {
        #region C-tor

        /// <summary>
        /// Create instance <see cref="PushParameters"/>
        /// </summary>
        public PushParameters()
        {
            Priority = PushPriority.Default;
            Ttl = Constants.DefaultTtl;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// PUSH message title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// PUSH message text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// PUSH message url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// PUSH message icon url
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// PUSH message priority
        /// </summary>
        public PushPriority Priority { get; set; }

        /// <summary>
        /// PUSH message TTL
        /// </summary>
        public ulong Ttl { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Converts <see cref="PushParameters"/> to dictionary
        /// </summary>
        /// <param name="parameters">Push message parameters</param>
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