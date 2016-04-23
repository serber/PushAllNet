using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PushAll.Exceptions;
using PushAll.Models;
using PushAll.Utils;

namespace PushAll
{
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

        /// <summary>
        /// Oтправляет уведомление всем перечисленным подписчикам с учетом их фильтров
        /// </summary>
        public async Task<ulong> SendMulticastAsync(MulticastParameters parameters)
        {
            return await Execute("multicast", parameters);
        }

        /// <summary>
        /// Oтправляет уведомление всем подписчикам с учетом их фильтров
        /// </summary>
        public async Task<ulong> SendBroadcastAsync(PushParameters parameters)
        {
            return await Execute("broadcast", parameters);
        }

        /// <summary>
        /// Отправляет уведомление одному подписчику канала без учета фильтров
        /// </summary>
        public async Task<ulong> SendUnicastAsync(UnicastParameters parameters)
        {
            return await Execute("unicast", parameters);
        }

        #endregion

        #region Private methods

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