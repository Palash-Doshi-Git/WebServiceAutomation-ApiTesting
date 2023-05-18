using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model;

namespace WebServiceAutomation.Helper.Request
{
    public class HttpClientHelper
    {
        private static HttpClient httpClient;
        private static HttpRequestMessage httpRequestMessage;
        private static RestResponse restResponse;

        private static HttpClient AddHeadersCreateClient(Dictionary<string, string> httpHeader)
        {
            httpClient = new HttpClient();
            if (httpHeader != null)
                foreach (string key in httpHeader.Keys)
                    httpClient.DefaultRequestHeaders.Add(key, httpHeader[key]);
            return httpClient;
        }

        private static HttpRequestMessage CreateRequestMessage(string requestUrl, HttpMethod method, HttpContent content)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, requestUrl);

            if (!(method == HttpMethod.Get))
                requestMessage.Content = content;
            return requestMessage;
        }

        private static RestResponse SendRequest( string requestUrl, HttpMethod method, HttpContent content, Dictionary<string, string> httpHeader)
        {
            httpClient = AddHeadersCreateClient(httpHeader);

            httpRequestMessage = CreateRequestMessage(requestUrl, method, content);
            try
            {
                Task<HttpResponseMessage> responseMessage = httpClient.SendAsync(httpRequestMessage);
                restResponse = new RestResponse((int)responseMessage.Result.StatusCode, responseMessage.Result.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {  restResponse = new RestResponse(500, ex.Message);   }
            finally
            {
                httpRequestMessage?.Dispose();
                httpClient?.Dispose();
            }
            return restResponse;
        }

        public static RestResponse GetRequest(string requestUrl, Dictionary<string,string> httpHeaders)
        {
            return SendRequest(requestUrl, HttpMethod.Get, null, httpHeaders);
        }

        public static RestResponse PostRequest(string requestUrl, HttpContent content, Dictionary<string, string> httpHeaders)
        {
            return SendRequest(requestUrl, HttpMethod.Post, content, httpHeaders);
        }

        public static RestResponse PostRequest(string requestUrl, string data,string mediatype, Dictionary<string, string> httpHeaders) 
        {
            HttpContent httpContent = new StringContent(data, Encoding.UTF8, mediatype);
                return PostRequest(requestUrl, httpContent, httpHeaders);

        }
    }
}

