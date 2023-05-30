using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp_FrameWork.APIModel.JsonApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.PutEndPoints
{   
    [TestClass]
    public class TestPutEndPointscs
    {


        private readonly string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private readonly string putUrl = "http://localhost:8080/laptop-bag/webapi/api/update";
        private readonly Random random = new Random();

        [TestMethod]
        public void TestPutReqWithJson()
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
            var restResponse = restClient.ExecutePost<JsonModel>(request);
            restResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            jsonData = "{" +
                                    "\"BrandName\": \"Alienware Updates\"," +
                                    "\"Features\": {" +
                                    "\"Feature\": [" +
                                    "\"8th Generation Intel® Core™ i5-8300H\"," +
                                    "\"Windows 10 Home 64-bit English\"," +
                                    "\"NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6\"," +
                                    "\"8GB, 2x4GB, DDR4, 2666MHz\"" +
                                    "]" +
                                    "}," +
                                    "\"Id\": " + id + ", " +
                                    "\"LaptopName\": \"Alienware khg\"" +
                                "}";

            request = new RestRequest()
            {
                Resource = putUrl,
                Method = Method.Put
            };

            request.AddStringBody(jsonData, DataFormat.Json);
            restResponse =  restClient.ExecutePut<JsonModel>(request);
            restResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            request = new RestRequest()
            {
                Resource = getUrl +id,
                Method = Method.Get
            };

            restResponse = restClient.ExecuteGet<JsonModel>(request);
            restResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            restResponse.Data.LaptopName.Should().Be("Alienware khg");


        }



    }
}
