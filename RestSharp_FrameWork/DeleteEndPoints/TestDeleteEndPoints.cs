using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp_FrameWork.APIModel.JsonApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.DeleteEndPoints
{
    [TestClass]
    public class TestDeleteEndPoints
    {
        private readonly string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private readonly string deleteUrl = "http://localhost:8080/laptop-bag/webapi/api/delete/";
        private readonly Random random = new Random();

        [TestMethod]
        public void TestDeleteEndPointsWithId()
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


            //Create Client 

            RestClient restClient = new RestClient();

            //Create request

            RestRequest request = new RestRequest()
            {
                Method = Method.Post,
                Resource = postUrl
            };

            // Add Body

            request.AddStringBody(jsonData, DataFormat.Json);
            // send request
            var restResponse = restClient.ExecutePost(request);
            restResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            request = new RestRequest()
            {
                Resource = deleteUrl + id,
                Method = Method.Delete
            };
            request.AddHeader("Accept", "text/plain");
            restClient.Delete(request);
            restResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            request = new RestRequest()
            {
                Method = Method.Get,
                Resource = getUrl + id
            };

            restResponse = restClient.ExecutePost(request);
            restResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

    }
}
