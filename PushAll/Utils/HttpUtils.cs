using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using PushAll.Exceptions;

namespace PushAll.Utils
{
    internal static class HttpUtils
    {
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
            catch (WebException e)
            {
                throw new PushAllApiException(e.Message, e);
            }
        }

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
            catch (WebException e)
            {
                throw new PushAllApiException(e.Message, e);
            }
        }
    }
}