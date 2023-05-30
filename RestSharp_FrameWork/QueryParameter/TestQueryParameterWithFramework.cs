using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp_FrameWork.APIHelper;
using RestSharp_FrameWork.APIHelper.APIRequest;
using RestSharp_FrameWork.APIHelper.Client;
using RestSharp_FrameWork.APIHelper.Command;
using RestSharp_FrameWork.APIHelper.RestAPIExecutor;
using RestSharp_FrameWork.APIModel.JsonApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharp_FrameWork.QueryParameter
{   
    [TestClass]
    public class TestQueryParameterWithFramework
    {
       
        private readonly string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private readonly string queryUrl = "http://localhost:8080/laptop-bag/webapi/api/query";
        private readonly Random random = new Random();

        private static IClient _client;
        private static RestApiExecutor _executor;
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
        public void TEstGetWithQueryParameter()
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

            var getRequest = new GetRequestBuilder().WithUrl(queryUrl).WithQueryParameters(new Dictionary<string, string>()
            {
                { "id", id.ToString() },
                { "laptopName", "Test Laptop" }
            }).WithHeader(new Dictionary<string, string>(){{ "Accept","application/xml"} });

             command = new RequestCommand(getRequest, _client);

            // SetCommand

            _executor.SetCommand(command);
            var getResponse = _executor.ExecuteRequest<Laptop>();
            getResponse.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            getResponse.GetResponseData().LaptopName.Should().Be("Test Laptop");
        }

    }
}
