using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp_FrameWork.APIModel.JsonApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.QueryParameter
{
    [TestClass]
    public class TestQueryParameter
    {
        private readonly string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private readonly string queryUrl = "http://localhost:8080/laptop-bag/webapi/api/query";
        private readonly Random random = new Random();

        [TestMethod]
        public void TestGetWithQueryParameter()
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
            RestResponse restResponse = restClient.ExecutePost(request);
            restResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            request = new RestRequest()
            {
                Method = Method.Get,
                Resource = queryUrl
            };

            request.AddParameter("id", id);
            request.AddParameter("laptopName", "Alienware M17");
            //request.AddQueryParameter("id", id); For Post Put 

            var response = restClient.ExecuteGet<JsonModel>(request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Data.Id.Should().Be(id);
            response.Data.LaptopName.Should().Be("Alienware M17");


        }

    }
}
