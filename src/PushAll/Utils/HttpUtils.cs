using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using PushAll.Exceptions;

namespace PushAll.Utils
{
    /// <summary>
    /// Helper class for HTTP requests
    /// </summary>
    internal static class HttpUtils
    {
        /// <summary>
        /// Send POST HTTP request
        /// </summary>
        /// <param name="url">Target url</param>
        /// <param name="postData">POST data</param>
        public static async Task<string> SendPost(string url, IDictionary<string, string> postData)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    FormUrlEncodedContent content = new FormUrlEncodedContent(postData);

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    string responseString = await response.Content.ReadAsStringAsync();

                    return responseString;
                }
            }
            catch (HttpRequestException e)
            {
                throw new PushAllApiException(e.Message, e);
            }
        }

        /// <summary>
        /// Send GET HTTP request
        /// </summary>
        /// <param name="url">Target url</param>
        public static async Task<string> SendGet(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    
                    string responseString = await response.Content.ReadAsStringAsync();

                    return responseString;
                }
            }
            catch (HttpRequestException e)
            {
                throw new PushAllApiException(e.Message, e);
            }
        }
    }
}