using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PushAll.Exceptions;
using PushAll.Models;
using PushAll.Utils;

namespace PushAll
{
    /// <summary>
    /// PushAll API client implemetation
    /// </summary>
    public sealed class PushAllApi
    {
        #region Private fields

        private readonly PushAllOptions _options;

        #endregion

        #region Public methods

        public PushAllApi(PushAllOptions options)
        {
            _options = options;
        }

        public async Task<ulong> SendMulticastAsync(MulticastParameters parameters)
        {
            return await Execute("multicast", parameters);
        }

        public async Task<ulong> SendBroadcastAsync(PushParameters parameters)
        {
            return await Execute("broadcast", parameters);
        }

        public async Task<ulong> SendUnicastAsync(UnicastParameters parameters)
        {
            return await Execute("unicast", parameters);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Execute API request
        /// </summary>
        /// <param name="method">HTTP method</param>
        /// <param name="parameters">Request parameters</param>
        private async Task<ulong> Execute(string method, Dictionary<string, string> parameters)
        {
            parameters.Add("id", _options.ChannelId.ToString());
            parameters.Add("key", _options.ApiKey);
            parameters.Add("type", method);

            string responseText = await HttpUtils.SendPost(Constants.ApiHost, parameters);

            /* 
             {"success":1,"unfilt":1,"all":1,"lid":1635732,"status":"Run in background"}

             {"error":"wrong key"}
            */

            return ParseResponseOrThrow(responseText);
        }

        /// <summary>
        /// Parse API response or throw exception
        /// </summary>
        /// <param name="response">API response text</param>
        private ulong ParseResponseOrThrow(string response)
        {
            Dictionary<string, string> responseDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

            if (responseDictionary.ContainsKey("lid"))
            {
                ulong lid;
                if (ulong.TryParse(responseDictionary["lid"], out lid))
                {
                    return lid;
                }
            }

            if (responseDictionary.ContainsKey("error"))
            {
                throw new PushAllApiException(responseDictionary["error"]);
            }

            throw new PushAllApiException("Unkown error");
        }

        #endregion
    }
}