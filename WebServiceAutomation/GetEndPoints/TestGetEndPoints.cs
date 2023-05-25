using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebServiceAutomation.Authentication;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.Response_Data;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;

namespace WebServiceAutomation.GetEndPoints
{
    [TestClass]
    public class TestGetEndPoints
    {
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
        private readonly string secureGetUrl = "http://localhost:8080/laptop-bag/webapi/secure/all";


        [TestMethod]
        public void TestGetAllEndPoint()
        {
            HttpClient hClient = new HttpClient(); // Create HttpCLient
            hClient.GetAsync(getUrl); // Create request and execute
            hClient.Dispose();
        }

        [TestMethod]
        public void TestGetAllEndPointWithUri()
        {
            HttpClient hClient = new HttpClient(); // Create HttpCLient
            Uri getUri = new Uri(getUrl); // create a uri to pass with request created below
            Task<HttpResponseMessage> msg = hClient.GetAsync(getUri); // Create request and execute
            HttpResponseMessage responseMessage = msg.Result; // Get Response in variable 
            Console.WriteLine(responseMessage.ToString());

            // Get status code
            HttpStatusCode Status = responseMessage.StatusCode;
            Console.WriteLine("Status Code  " + Status);
            Console.WriteLine("Status Code  " + (int)Status);

            //Get Response Data
            HttpContent reponseContent = responseMessage.Content;
            Task<string> responseData = reponseContent.ReadAsStringAsync();

            string data = responseData.Result;
            Console.WriteLine(data);
            hClient.Dispose();
            hClient.Dispose();

        }

        [TestMethod]
        public void TestGetAllEndPointWithInvalidUrl()
        {
            HttpClient hClient = new HttpClient(); // Create HttpCLient
            Uri getUri = new Uri(getUrl); // create a uri to pass with request created below
            Task<HttpResponseMessage> msg = hClient.GetAsync(getUri + "/Palash"); // Create request and execute
            HttpResponseMessage responseMessage = msg.Result; // Get Response in variable 
            Console.WriteLine(responseMessage.ToString());

            // Get Status code
            HttpStatusCode Status = responseMessage.StatusCode; //Get Status code
            Console.WriteLine("Status Code  " + Status);
            Console.WriteLine("Status Code  " + (int)Status);

            //Get Response Data
            HttpContent reponseContent = responseMessage.Content;
            Task<string> responseData = reponseContent.ReadAsStringAsync();

            string data = responseData.Result;
            Console.WriteLine(data);
            hClient.Dispose();
        }

        [TestMethod]
        public void TestGetAllEndPointInJsonFormat()
        {
            HttpClient hClient = new HttpClient(); // Create HttpCLient
            HttpRequestHeaders requestHeaders = hClient.DefaultRequestHeaders;
            requestHeaders.Add("Accept", "application/json");
            Uri getUri = new Uri(getUrl); // create a uri to pass with request created below
            Task<HttpResponseMessage> msg = hClient.GetAsync(getUri);

            HttpResponseMessage responseMessage = msg.Result; // Get Response in variable 
            Console.WriteLine(responseMessage.ToString());

            // Get Status code
            HttpStatusCode Status = responseMessage.StatusCode; //Get Status code
            Console.WriteLine("Status - " + Status);
            Console.WriteLine("Status Code - " + (int)Status);

            //Get Response Data
            HttpContent reponseContent = responseMessage.Content;
            Task<string> responseData = reponseContent.ReadAsStringAsync();

            string data = responseData.Result;
            Console.WriteLine(data);
            hClient.Dispose();
        }


        [TestMethod]
        public void TestGetAllEndPointInXMLFormat()
        {
            HttpClient hClient = new HttpClient(); // Create HttpCLient
            HttpRequestHeaders requestHeaders = hClient.DefaultRequestHeaders;
            requestHeaders.Add("Accept", "application/xml");
            Uri getUri = new Uri(getUrl); // create a uri to pass with request created below
            Task<HttpResponseMessage> msg = hClient.GetAsync(getUri);

            HttpResponseMessage responseMessage = msg.Result; // Get Response in variable 
            Console.WriteLine(responseMessage.ToString());

            // Get Status code
            HttpStatusCode Status = responseMessage.StatusCode; //Get Status code
            Console.WriteLine("Status - " + Status);
            Console.WriteLine("Status Code - " + (int)Status);

            //Get Response Data
            HttpContent reponseContent = responseMessage.Content;
            Task<string> responseData = reponseContent.ReadAsStringAsync();

            string data = responseData.Result;
            Console.WriteLine(data);
            hClient.Dispose();
        }


        [TestMethod]
        public void TestGetAllEndPointInJsonFormatWithAcceptHeader()
        {
            MediaTypeWithQualityHeaderValue jsonHeader = new MediaTypeWithQualityHeaderValue("application/json");
            HttpClient hClient = new HttpClient(); // Create HttpCLient
            HttpRequestHeaders requestHeaders = hClient.DefaultRequestHeaders;
            requestHeaders.Accept.Add(jsonHeader);
            Uri getUri = new Uri(getUrl); // create a uri to pass with request created below
            Task<HttpResponseMessage> msg = hClient.GetAsync(getUri);

            HttpResponseMessage responseMessage = msg.Result; // Get Response in variable 
            Console.WriteLine(responseMessage.ToString());

            // Get Status code
            HttpStatusCode Status = responseMessage.StatusCode; //Get Status code
            Console.WriteLine("Status - " + Status);
            Console.WriteLine("Status Code - " + (int)Status);

            //Get Response Data
            HttpContent reponseContent = responseMessage.Content;
            Task<string> responseData = reponseContent.ReadAsStringAsync();

            string data = responseData.Result;
            Console.WriteLine(data);
            hClient.Dispose();
        }

        [TestMethod]
        public void TestGetAllEndPointUsingSendAsync() //SendAsync can add Header to url
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(getUrl),
                Method = HttpMethod.Get
            };
            requestMessage.Headers.Add("Accept", "application/json");
            HttpClient hClient = new HttpClient(); // Create HttpCLient
            Task<HttpResponseMessage> msg = hClient.SendAsync(requestMessage);

            HttpResponseMessage responseMessage = msg.Result; // Get Response in variable 
            Console.WriteLine(responseMessage.ToString());

            // Get Status code
            HttpStatusCode Status = responseMessage.StatusCode; //Get Status code
            Console.WriteLine("Status - " + Status);
            Console.WriteLine("Status Code - " + (int)Status);

            //Get Response Data
            HttpContent reponseContent = responseMessage.Content;
            Task<string> responseData = reponseContent.ReadAsStringAsync();

            string data = responseData.Result;
            Console.WriteLine(data);


            hClient.Dispose();
        }

        [TestMethod]
        public void TestGetUsingStatement()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage requestMessage = new HttpRequestMessage())
                {
                    requestMessage.RequestUri = new Uri(getUrl);
                    requestMessage.Method = HttpMethod.Get;
                    requestMessage.Headers.Add("Accept", "application/json");
                    Task<HttpResponseMessage> msg = httpClient.SendAsync(requestMessage);
                    using (HttpResponseMessage responseMessage = msg.Result) // Get Response in variable 
                    {
                        Console.WriteLine(responseMessage.ToString());

                        // Get Status code
                        HttpStatusCode Status = responseMessage.StatusCode; //Get Status code
                        /*Console.WriteLine("Status - " + Status);
                        Console.WriteLine("Status Code - " + (int)Status);*/

                        //Get Response Data
                        HttpContent reponseContent = responseMessage.Content;
                        Task<string> responseData = reponseContent.ReadAsStringAsync();

                        string data = responseData.Result;
                        //Console.WriteLine(data);
                        RestResponse restResponse = new RestResponse((int)Status, responseData.Result);
                        Console.WriteLine(restResponse.ToString());

                    }
                }

            }
        }

        [TestMethod]
        public void TestDeserilizationOfJsonResponse()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage requestMessage = new HttpRequestMessage())
                {
                    requestMessage.RequestUri = new Uri(getUrl);
                    requestMessage.Method = HttpMethod.Get;
                    requestMessage.Headers.Add("Accept", "application/json");
                    Task<HttpResponseMessage> msg = httpClient.SendAsync(requestMessage);
                    using (HttpResponseMessage responseMessage = msg.Result) // Get Response in variable 
                    {
                        Console.WriteLine(responseMessage.ToString());

                        // Get Status code
                        HttpStatusCode Status = responseMessage.StatusCode; //Get Status code
                        /*Console.WriteLine("Status - " + Status);
                        Console.WriteLine("Status Code - " + (int)Status);*/

                        //Get Response Data
                        HttpContent reponseContent = responseMessage.Content;
                        Task<string> responseData = reponseContent.ReadAsStringAsync();

                        string data = responseData.Result;
                        //Console.WriteLine(data);
                        RestResponse restResponse = new RestResponse((int)Status, responseData.Result);
                        //Console.WriteLine(restResponse.ToString());
                        List<JsonRootObject> jsonRootObject = JsonConvert.DeserializeObject<List<JsonRootObject>>(restResponse.ResponseContent);

                        Console.WriteLine(jsonRootObject[0].ToString());

                    }
                }

            }

        }

        [TestMethod]
        public void TestDeserilizationOfXmlResponse()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage requestMessage = new HttpRequestMessage())
                {
                    requestMessage.RequestUri = new Uri(getUrl);
                    requestMessage.Method = HttpMethod.Get;
                    requestMessage.Headers.Add("Accept", "application/xml");
                    Task<HttpResponseMessage> msg = httpClient.SendAsync(requestMessage);
                    using (HttpResponseMessage responseMessage = msg.Result) // Get Response in variable 
                    {
                        Console.WriteLine(responseMessage.ToString());

                        // Get Status code
                        HttpStatusCode Status = responseMessage.StatusCode; //Get Status code
                        /*Console.WriteLine("Status - " + Status);
                        Console.WriteLine("Status Code - " + (int)Status);*/

                        //Get Response Data
                        HttpContent reponseContent = responseMessage.Content;
                        Task<string> responseData = reponseContent.ReadAsStringAsync();

                        string data = responseData.Result;
                        //Console.WriteLine(data);
                        RestResponse restResponse = new RestResponse((int)Status, responseData.Result);
                        //Console.WriteLine(restResponse.ToString());

                        //  Step 1.

                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(LaptopDetailss));

                        //Step 2.
                        TextReader text = new StringReader(restResponse.ResponseContent);

                        //Step 3. 
                        LaptopDetailss xmlData = (LaptopDetailss)xmlSerializer.Deserialize(text);
                        Console.WriteLine(xmlData.ToString());

                        // Check status code
                        Assert.AreEqual(200, restResponse.StatusCode);
                        // check responsedata is not null
                        Assert.IsNotNull(restResponse.ResponseContent);
                        // 3rd Assertion with Condition
                        Assert.IsTrue(xmlData.Laptop[0].Features.Feature.Contains("8GB, 2x4GB, DDR4, 2666MHz"), "Item not found");
                        // 4th Assert BrandName
                        Assert.AreEqual("Alienware", xmlData.Laptop[0].BrandName);

                    }
                }

            }

        }


        [TestMethod]
        public void TestGetEndPointUsingHelperMethod()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");

            RestResponse restResponse = HttpClientHelper.GetRequest(getUrl, httpHeader);
            List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJSonResponse<List<JsonRootObject>>(restResponse.ResponseContent);
            Console.WriteLine(jsonData.ToString());

        }

        [TestMethod]
        public void TestSecureGetEndPoint()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();            
            string authHeader = "Basic "+ Base64StringConvertor.GetBase64String("admin", "welcome");
            httpHeader.Add("Authorization", authHeader);
            RestResponse restResponse = HttpClientHelper.GetRequest(secureGetUrl, httpHeader);

            Assert.AreEqual(200, restResponse.StatusCode);


            /*List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJSonResponse<List<JsonRootObject>>(restResponse.ResponseContent);
            Console.WriteLine(jsonData.ToString());*/

        }

    }
}
