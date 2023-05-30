using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp_FrameWork.APIHelper;
using RestSharp_FrameWork.APIHelper.APIRequest;
using RestSharp_FrameWork.APIHelper.Command;
using RestSharp_FrameWork.APIHelper.RestAPIExecutor;
using System;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharp_FrameWork.PutEndPoints
{
    [TestClass]
    public class TestPutEndPointsWithFrameWork
    {
        private readonly string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private readonly string putUrl = "http://localhost:8080/laptop-bag/webapi/api/update";
        private readonly Random random = new Random();
        private static IClient client;
        private static RestApiExecutor executor;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            client = new DefaultClient();
            executor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            client?.Dispose();
        }

        [TestMethod]
        public void TestPutRequestWithFameWork_XMl()
        {
            int id = random.Next(1000);
            var xmlBody = new XmlModelBuilder().WithId(id).WithBrandName("BrandName Brand ").WithFeatures(new System.Collections.Generic.List<string>() { "Feature1", "Feature2" }).WithLaptopName("LaptopName").Build();

            var postRequest = new PostRequestBuilder().WithUrl(postUrl).WithHeaders(new System.Collections.Generic.Dictionary<string, string>() { { "Accept", "application/xml" } }).WithBody<Laptop>(xmlBody, RestSharp_FrameWork.APIHelper.APIRequest.RequestBodyType.XML);

            var command = new RequestCommand(postRequest,client);
             executor.SetCommand(command);
            var response = executor.ExecuteRequest<Laptop>();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            var putRequestBody = new XmlModelBuilder().WithId(id).WithLaptopName("New Name").WithBrandName("New BrandName").WithFeatures(new System.Collections.Generic.List<string>() { "NewFeatujre" }).Build();

            var putRequest = new PutRequestBuilder().WithUrl(putUrl).WithBody<Laptop>(putRequestBody, RestSharp_FrameWork.APIHelper.APIRequest.RequestBodyType.XML).WithHeaders(new System.Collections.Generic.Dictionary<string, string>() { { "Accept", "application/xml" } });

            command = new RequestCommand(putRequest, client);

            executor.SetCommand(command);
            response = executor.ExecuteRequest<Laptop>();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);


            var getRequest = new GetRequestBuilder().WithUrl(getUrl+id).WithHeader(new System.Collections.Generic.Dictionary<string, string>() { { "Accept", "application/xml" } });

            command = new RequestCommand(getRequest, client);

            executor.SetCommand(command);
            response = executor.ExecuteRequest<Laptop>();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            response.GetResponseData().BrandName.Should().Be("New BrandName");

        }




    }
}
