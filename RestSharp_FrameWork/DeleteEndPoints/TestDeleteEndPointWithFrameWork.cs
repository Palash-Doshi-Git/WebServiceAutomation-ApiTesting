using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp_FrameWork.APIHelper;
using RestSharp_FrameWork.APIHelper.APIRequest;
using RestSharp_FrameWork.APIHelper.Client;
using RestSharp_FrameWork.APIHelper.Command;
using RestSharp_FrameWork.APIHelper.RestAPIExecutor;
using System;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharp_FrameWork.DeleteEndPoints
{
    [TestClass]
    public class TestDeleteEndPointWithFrameWork
    {
        private static IClient client;
        private static RestApiExecutor executor;
        private readonly string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private readonly string deleteUrl = "http://localhost:8080/laptop-bag/webapi/api/delete/";
        private readonly Random random = new Random();

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            client = new TracerClient();
            executor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            client?.Dispose();
        }


        [TestMethod]
        public void TestDeleteEndPointWithFramework()
        {
            int id = random.Next(1000);
            var xmlBody = new XmlModelBuilder().WithId(id).WithBrandName("BrandName Brand ").WithFeatures(new System.Collections.Generic.List<string>() { "Feature1", "Feature2" }).WithLaptopName("LaptopName").Build();

            var postRequest = new PostRequestBuilder().WithUrl(postUrl).WithHeaders(new System.Collections.Generic.Dictionary<string, string>() { { "Accept", "application/xml" } }).WithBody<Laptop>(xmlBody, RestSharp_FrameWork.APIHelper.APIRequest.RequestBodyType.XML);

            var command = new RequestCommand(postRequest, client);
            executor.SetCommand(command);
            var response = executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            var deleteRequest = new DeleteRequestBuilder().WithDefaultHeader().WithUrl(deleteUrl + id);

            command = new RequestCommand(deleteRequest, client);
            executor.SetCommand(command);
            response = executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            var getRequest = new GetRequestBuilder().WithUrl(getUrl).WithHeader(new System.Collections.Generic.Dictionary<string, string>() { { "Accept", "application/xml" } });
            command = new RequestCommand(getRequest, client);
            executor.SetCommand(command);
            response = executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
