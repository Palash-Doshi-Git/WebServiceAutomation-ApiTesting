using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp_FrameWork.APIHelper;
using RestSharp_FrameWork.APIHelper.APIRequest;
using RestSharp_FrameWork.APIHelper.Command;
using RestSharp_FrameWork.APIHelper.RestAPIExecutor;
using System.Collections.Generic;
using System.Diagnostics;
using WebServiceAutomation.Model.JsonModel;

namespace RestSharp_FrameWork.GetEndPoints
{
    [TestClass]
    public class TestGetWithFramework
    {
        private static IClient client;
        private static RestApiExecutor executor;
        private  readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";



        //ClassInitialize - a method that contains code that must be used before any of the tests in the test class have run
        /*        [ClassInitialize]
                public static void SetUp(TestContext testContext)
                {
                    // Create the Default Client
                    client = new DefaultClient();
                    // Create the Executor
                    executor = new RestApiExecutor();
                }
        */

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            client = new DefaultClient();
            executor = new RestApiExecutor();
        }

        [TestMethod]
        public void GetRequest()
        {
            AbstractRequest request = new GetRequestBuilder().WithUrl(getUrl);
            ICommand getCommand = new RequestCommand(request, client);
            executor.SetCommand(getCommand);
            var response = executor.ExecuteRequest<List<JsonRootObject>>();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.GetResponseData());

            request = new GetRequestBuilder().WithUrl("http://www.google.com");
            getCommand = new RequestCommand(request, client);
            executor.SetCommand(getCommand);
            response = executor.ExecuteRequest<List<JsonRootObject>>();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.GetResponseData());
        }

        
        [TestMethod]
        public void GetRequestWithJsonData()
        {
            // Create the GET request.
            AbstractRequest request = new GetRequestBuilder().WithUrl(getUrl);

            // Create the Command for the GET request.
            ICommand getCommand = new RequestCommand(request, client);

            // Set the command for the RestApiExecutor.
            executor.SetCommand(getCommand);
            // Send the GET request and De-Serialize the response to an object.
            var response = executor.ExecuteRequest<List<JsonRootObject>>();

            // Validate the status code using Fluent API.
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

            // Extract a single object.
            var entry = response.GetResponseData().Find((item) => { return item.Id == 1; });

            // Validate the BrandName property.
            entry.BrandName.Should().NotBeNull();
            // Validate the LaptopName property.
            entry.LaptopName.Should().NotBeNull();

        }

        //ClassCleanup - a method that contains code to be used after all the tests in the test class have run
        [ClassCleanup]
        public static void TearDown()
        {
            // Release the resource acquired by the client.
            client?.Dispose();
        }

    }
}
