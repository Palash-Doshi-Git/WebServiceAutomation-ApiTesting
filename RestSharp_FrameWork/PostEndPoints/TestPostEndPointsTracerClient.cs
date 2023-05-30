using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp.Serializers;
using RestSharp_FrameWork.APIHelper;
using RestSharp_FrameWork.APIHelper.Client;
using RestSharp_FrameWork.APIHelper.Command;
using RestSharp_FrameWork.APIHelper.RestAPIExecutor;
using RestSharp_FrameWork.APIModel.JsonApiModel;
using RestSharp_FrameWork.APIHelper.APIRequest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WebServiceAutomation.Model.XmlModel;


namespace RestSharp_FrameWork.PostEndPoints
{
    [TestClass]
    public class TestPostEndPointsTracerClient
    {
        private static IClient _client;
        private static RestApiExecutor _executor;
        private readonly string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private readonly Random random = new Random();

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _client = new TracerClient();
            _executor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _client?.Dispose();
        }

        [TestMethod]
        public void TestPostRequestWithFramework_Json()
        {
            int id = random.Next(100);

            var payload = new JsonModelBuilder().WithId(id).WithBrandName(postUrl).WithLaptopName("Test Laptop").WithFeatures(new List<string>() { "Feature1", "Feature2" }).Build();

            // Post Request 
            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<JsonModel>(payload, RequestBodyType.JSON);

            // Command

            var command = new RequestCommand(request, _client);

            // SetCommand

            _executor.SetCommand(command);
            var response = _executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("Test Laptop");


        }

        [TestMethod]
        public void TestPostEndPointwithFrameWork_String()
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

            // Post Request 
            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<string>(jsonData, RequestBodyType.STRING);

            // Command

            var command = new RequestCommand(request, _client);

            // SetCommand

            _executor.SetCommand(command);
            var response = _executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("8GB, 2x4GB, DDR4, 2666MHz");

            var responseType = _executor.ExecuteRequest<JsonModel>();
            responseType.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var responseData = responseType.GetResponseData();
            responseData.Features.Feature.Should().Contain("8GB, 2x4GB, DDR4, 2666MHz");
            Debug.WriteLine(responseData);
        }

        [TestMethod]
        public void TestPostEndPointsWithXML()
        {
            int id = random.Next(100);


            var payload = new XmlModelBuilder().WithBrandName("Alienware").WithFeatures(new List<string>() { "8th Generation Intel® Core™ i5 - 8300H", "Windows 10 Home 64 - bit English", ">NVIDIA® GeForce® GTX 1660 Ti 6GB GDDR6", "8GB, 2x4GB, DDR4, 2666MHz" }).WithId(id).WithLaptopName("Palash Laptop").Build();
            //

            // Post Request 
            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<Laptop>(payload, RequestBodyType.XML).WithHeaders(new Dictionary<string, string>() { { "Accept", "application/xml" } });

            // Command

            var command = new RequestCommand(request, _client);

            // SetCommand

            _executor.SetCommand(command);
            var response = _executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("8GB, 2x4GB, DDR4, 2666MHz");

            Debug.WriteLine(response.GetResponseData());
        }


        [TestMethod]
        public void TestPostEndPointsWithXML_String()
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

            // Post Request 
            var request = new PostRequestBuilder().WithUrl(postUrl).WithBody<string>(xmlData, RequestBodyType.STRING, ContentType.Xml).WithHeaders(new Dictionary<string, string>() { { "Accept", "application/xml" } });

            // Command

            var command = new RequestCommand(request, _client);

            // SetCommand

            _executor.SetCommand(command);
            var response = _executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().Should().Contain("8GB, 2x4GB, DDR4, 2666MHz");

            var responseType = _executor.ExecuteRequest<Laptop>();
            responseType.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var responseData = responseType.GetResponseData();
            responseData.Features.Feature.Should().Contain("8GB, 2x4GB, DDR4, 2666MHz");
            Debug.WriteLine(responseData);

        }

    }
}
