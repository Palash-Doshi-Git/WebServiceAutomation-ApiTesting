using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
namespace WebServiceAutomation.PutEndPoints
{
    [TestClass]
    public class TestPutEndPoints
    {

        private readonly string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private readonly string putUrl = "http://localhost:8080/laptop-bag/webapi/api/update";
        private RestResponse restResponse;
        private RestResponse getrestResponse;
        private string jsonMediaType = "application/json";
        private string xmlMediaType = "application/xml";
        private Random random = new Random();
        private Dictionary<string, string> jsonHeader = new Dictionary<string, string>(){{"Accept", "application/json"} };
        private Dictionary<string, string> xmlHeader = new Dictionary<string, string>(){ {"Accept", "application/xml"}};



        [TestMethod]
        public void TestPutUsingJsonData()
        {

            //Post - Create Record
            //Put - Update the record
            //Get - Verify the record

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


            restResponse = HttpClientHelper.PostRequest(postUrl,jsonData,jsonMediaType, jsonHeader);
            Assert.AreEqual(200, restResponse.StatusCode);

            jsonData = "{" +
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
            using(HttpClient hClient = new HttpClient())
            {
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, jsonMediaType);
                Task<HttpResponseMessage> responseMessage = hClient.PutAsync(putUrl, content);
                restResponse = new RestResponse((int)responseMessage.Result.StatusCode, responseMessage.Result.Content.ReadAsStringAsync().Result);
               
                Assert.AreEqual(200, restResponse.StatusCode);

            }

            restResponse = HttpClientHelper.GetRequest(getUrl + id, jsonHeader);

        }


    }

}
