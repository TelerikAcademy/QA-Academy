using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace ITeBooks
{
    public static class HttpHelper
    {       
        /// <summary>
        /// Get request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="authorizationHeader"></param>
        /// <param name="authorizationType"></param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage Get(string uri, string authorizationHeader = null, string authorizationType = "Bearer")
        {
            return SendRequest(HttpMethod.Get, uri, authorizationHeader, authorizationType, null, null);
        }

        /// <summary>
        /// Post request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="authorizationHeader"></param>
        /// <param name="authorizationType"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage Post(string uri, string authorizationHeader = null, string authorizationType = "Bearer", string content = null, string contentType = @"application/json")
        {
            return SendRequest(HttpMethod.Post, uri, authorizationHeader, authorizationType, content, contentType);
        }

        /// <summary>
        /// Put request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="authorizationHeader"></param>
        /// <param name="authorizationType"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage Put(string uri, string authorizationHeader = null, string authorizationType = "Bearer", string content = null, string contentType = @"application/json")
        {
            return SendRequest(HttpMethod.Put, uri, authorizationHeader, authorizationType, content, contentType);
        }

        /// <summary>
        /// Delete request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="authorizationHeader"></param>
        /// <param name="authorizationType"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns>HttpResponseMessage</returns>
        public static HttpResponseMessage Delete(string uri, string authorizationHeader = null, string authorizationType = "Bearer", string content = null, string contentType = @"application/x-www-form-urlencoded")
        {
            return SendRequest(HttpMethod.Delete, uri, authorizationHeader, authorizationType, content, contentType);
        }

        /// <summary>
        /// Execute http request
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="uri"></param>
        /// <param name="authorizationHeader"></param>
        /// <param name="authorizationType"></param>
        /// <param name="content"></param>
        /// <param name="contentType"></param>
        /// <returns>HttpResponseMessage</returns>
        private static HttpResponseMessage SendRequest(HttpMethod requestType, string uri, string authorizationHeader, string authorizationType, string content, string contentType)
        {
            var httpRequestMessage = new HttpRequestMessage(requestType, new Uri(uri));

            if ((authorizationHeader != null) && (authorizationType != null))
            {
                httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationType, authorizationHeader);
            }

            if (content != null)
            {
                httpRequestMessage.Content = new StringContent(content, Encoding.UTF8, contentType);
            }

            using (var httpClient = new HttpClient())
            {
                // Sleep for 1 second between ITeBooks API calls.
                // Otherwise we will hit the limits. 
                // Please read this page: http://it-ebooks-api.info/
                Thread.Sleep(1000);

                httpClient.Timeout = new TimeSpan(0, 10, 0);
                return httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseContentRead).Result;
            }
        }
    }
}
