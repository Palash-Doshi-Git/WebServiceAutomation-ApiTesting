using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.Response_Data;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;

namespace WebServiceAutomation.PostEndPoints
{

    [TestClass]
    public class TestPostEndPoints
    {
        private readonly string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private RestResponse restResponse;
        private RestResponse getrestResponse;
        private string jsonMediaType = "application/json";
        private string xmlMediaType = "application/xml";
        private Random random = new Random();

        [TestMethod]
        public void TestPostEndPointWithJson()
        {
            //Method - POST
            // Body along with request -- HttpContedt class
            //Header- info about data format

            int id = random.Next(100);
            string jsonData = "{" +
                                    "\"BrandName\": \"Alienware\"," +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                                    "\"Windows 10 Home 64-bit English\"," +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " + id + ", " +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";



            using (HttpClient hClient = new HttpClient())
            {
                hClient.DefaultRequestHeaders.Add("Accept", jsonMediaType);
                HttpContent httpContent = new StringContent(jsonData, Encoding.UTF8, jsonMediaType);
                Task<HttpResponseMessage> postMessage = hClient.PostAsync(postUrl, httpContent);
                HttpStatusCode statusCode = postMessage.Result.StatusCode;
                HttpContent responseContent = postMessage.Result.Content;
                string responseData = responseContent.ReadAsStringAsync().Result;

                restResponse = new RestResponse((int)statusCode, responseData);

                Assert.AreEqual(200, restResponse.StatusCode);
                Assert.IsNotNull(restResponse.ResponseContent, "Response Data In Null/Empty");

                Assert.AreEqual(200, (int)statusCode);
                Assert.IsNotNull(responseContent, "Response Data In Null/Empty");

                Task<HttpResponseMessage> responseMessage = hClient.GetAsync(getUrl + id);
                getrestResponse = new RestResponse((int)responseMessage.Result.StatusCode, responseMessage.Result.Content.ReadAsStringAsync().Result);
                statusCode = responseMessage.Result.StatusCode;
                responseContent = responseMessage.Result.Content;

                Assert.AreEqual(200, (int)statusCode);
                Assert.IsNotNull(responseContent, "Response Data In Null/Empty");

                JsonRootObject json = JsonConvert.DeserializeObject<JsonRootObject>(getrestResponse.ResponseContent);

                Assert.AreEqual(id, json.Id);
                Assert.AreEqual("Alienware", json.BrandName);
            }


        }

        [TestMethod]
        public void TestPostEndPointWithXml()
        {
            int id = random.Next(100);
            string xmlData = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                     "</Features>" +
                                  "<Id>" + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";



            using (HttpClient hClient = new HttpClient())
            {
                hClient.DefaultRequestHeaders.Add("Accept", xmlMediaType);
                HttpContent httpContent = new StringContent(xmlData, Encoding.UTF8, xmlMediaType);
                Task<HttpResponseMessage> postMessage = hClient.PostAsync(postUrl, httpContent);
                restResponse = new RestResponse((int)postMessage.Result.StatusCode, postMessage.Result.Content.ReadAsStringAsync().Result);

                Assert.AreEqual(200, restResponse.StatusCode);
                Assert.IsNotNull(restResponse.ResponseContent, "Response Data In Null/Empty");

                postMessage = hClient.GetAsync(getUrl + id);
                if (!postMessage.Result.IsSuccessStatusCode)
                    Assert.Fail("Http response not success , statuscode : " + (int)postMessage.Result.StatusCode);

                restResponse = new RestResponse((int)postMessage.Result.StatusCode, postMessage.Result.Content.ReadAsStringAsync().Result);

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Laptop));
                TextReader textReader = new StringReader(restResponse.ResponseContent);
                Laptop xmlContent = (Laptop)xmlSerializer.Deserialize(textReader);

                Assert.IsTrue(xmlContent.Features.Feature.Contains("8GB, 2x4GB, DDR4, 2666MHz"), "Item Is  not Preset in the list");
            }


        }

        [TestMethod]
        public void TestPostEndPointWithSendAsyncWithJson()
        {
            int id = random.Next(100);
            string jsonData = "{" +
                                    "\"BrandName\": \"Alienware\"," +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                                    "\"Windows 10 Home 64-bit English\"," +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " + id + ", " +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";

            using (HttpClient hClient = new HttpClient())
            {
                using (HttpRequestMessage requestMessage = new HttpRequestMessage())
                {
                    requestMessage.Method = HttpMethod.Post;
                    requestMessage.RequestUri = new Uri(postUrl);
                    requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, jsonMediaType);

                    Task<HttpResponseMessage> responseMessage = hClient.SendAsync(requestMessage);

                    restResponse = new RestResponse((int)responseMessage.Result.StatusCode, responseMessage.Result.Content.ReadAsStringAsync().Result);
                    Assert.AreEqual(200, restResponse.StatusCode);

                 
                }
            }
        }


        [TestMethod]
        public void TestPostEndPointWithSendAsyncXml()
        {
            int id = random.Next(100);
            string xmlDatastring = "<Laptop>" +
                                    "<BrandName>Alienware</BrandName>" +
                                    "<Features>" +
                                       "<Feature>8th Generation Intel® Core™ i5 - 8300H</Feature>" +
                                       "<Feature>Windows 10 Home 64 - bit English</Feature>" +
                                       "<Feature>NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6</Feature>" +
                                       "<Feature>8GB, 2x4GB, DDR4, 2666MHz</Feature>" +
                                     "</Features>" +
                                  "<Id>" + id + "</Id>" +
                                  "<LaptopName>Alienware M17</LaptopName>" +
                               "</Laptop>";



            using (HttpClient hClient = new HttpClient())
            {
                using (HttpRequestMessage requestMessage = new HttpRequestMessage())
                {
                    requestMessage.Method = HttpMethod.Post;
                    requestMessage.RequestUri = new Uri(postUrl);
                    requestMessage.Content = new StringContent(xmlDatastring, Encoding.UTF8, xmlMediaType);



                    Task<HttpResponseMessage> responseMessage = hClient.SendAsync(requestMessage);

                    restResponse = new RestResponse((int)responseMessage.Result.StatusCode, responseMessage.Result.Content.ReadAsStringAsync().Result);
                    Assert.AreEqual(200, restResponse.StatusCode);
                    Laptop xmlData = ResponseDataHelper.DeserializeXmlResponse<Laptop>(restResponse.ResponseContent);
                    Console.WriteLine(xmlData.ToString());

                }
            }
        }


        [TestMethod]
        public void TestPostEndPointsUsingHelperClass()
        {
            int id = random.Next(100);
            string jsonDatastring = "{" +
                                    "\"BrandName\": \"Alienware\"," +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                                    "\"Windows 10 Home 64-bit English\"," +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " + id + ", " +
                                    "\"LaptopName\": \"Alienware M17\"" +
                                "}";

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept",jsonMediaType }
            };
            //restResponse = HttpClientHelper.PostRequest(postUrl, jsonData, jsonMediaType, headers);
          
            HttpContent content = new StringContent(jsonDatastring, Encoding.UTF8, jsonMediaType);
            restResponse = HttpClientHelper.PostRequest(postUrl, content, headers);

            Assert.AreEqual(200, restResponse.StatusCode);

            List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJSonResponse<List<JsonRootObject>>(restResponse.ResponseContent);
            Console.WriteLine(jsonDatastring.ToString());
        }
    }
}
