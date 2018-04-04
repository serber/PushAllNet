using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                {
                   "ttl":2160000,
                   "unfuid":[],
                   "filtuid":[],
                   "benchmark":{
                      "start":"0.98532600 1522866444"
                   },
                   "success":1,
                   "sl":"a2c46a55be",
                   "lid":15436094,
                   "status":"Run in background"
                }
            */

            return ParseResponseOrThrow(responseText);
        }

        /// <summary>
        /// Parse API response or throw exception
        /// </summary>
        /// <param name="response">API response text</param>
        private ulong ParseResponseOrThrow(string response)
        {
            JObject jobject = JsonConvert.DeserializeObject<JObject>(response);

            if (jobject.TryGetValue("success", StringComparison.OrdinalIgnoreCase, out JToken successToken))
            {
                bool success = successToken.Value<bool>();
                if (success)
                {
                    if (jobject.TryGetValue("lid", StringComparison.OrdinalIgnoreCase, out JToken lidToken))
                    {
                        return lidToken.Value<ulong>();
                    }
                }
                else
                {
                    if (jobject.TryGetValue("error", StringComparison.OrdinalIgnoreCase, out JToken errorToken))
                    {
                        throw new PushAllApiException(errorToken.Value<string>());
                    }
                }
            }
            
            throw new PushAllApiException("Unkown error");
        }

        #endregion
    }
}