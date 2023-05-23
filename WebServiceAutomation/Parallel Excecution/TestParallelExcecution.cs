using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Authentication;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.Response_Data;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;

namespace WebServiceAutomation.Parallel_Excecution
{   [TestClass]
    public class TestParallelExcecution
    {
        private readonly string delayGetUrl = "http://localhost:8080/laptop-bag/webapi/delay/all";
        private readonly string delayGetWithId = "http://localhost:8080/laptop-bag/webapi/delay/find/";
        private readonly string delayedPostUrl = "http://localhost:8080/laptop-bag/webapi/delay/add";
        private readonly string delayedPutUrl = "http://localhost:8080/laptop-bag/webapi/delay/update";

        private string jsonMediaType = "application/json";
        private string xmlMediaType = "application/xml";
        private Random random = new Random();

        private void SendGetRequest()
        {
            Dictionary<string, string> httpHeader = new Dictionary<string, string>();
            httpHeader.Add("Accept", "application/json");

            RestResponse restResponse = HttpClientHelper.GetRequest(delayGetUrl, httpHeader);
            List<JsonRootObject> jsonData = ResponseDataHelper.DeserializeJSonResponse<List<JsonRootObject>>(restResponse.ResponseContent);
            Console.WriteLine(jsonData.ToString());
        }

        private void SendPostRequest()
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

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Accept","application/json" }
            };
            RestResponse restResponse = HttpClientHelper.PostRequest(delayedPostUrl, jsonData, jsonMediaType, headers);

            Assert.AreEqual(200, restResponse.StatusCode);

            List<JsonRootObject> jsonObj = ResponseDataHelper.DeserializeJSonResponse<List<JsonRootObject>>(restResponse.ResponseContent);
            Console.WriteLine(jsonData.ToString());
        }




        [TestMethod]
        public void TestTask()
        {
            Task get = new Task(() =>
            {
                SendGetRequest();
            });
            get.Start();

            Task post = new Task(() =>
            {
                SendPostRequest();
            });
            post.Start();

            get.Wait();
            post.Wait();

        }


    }
}
