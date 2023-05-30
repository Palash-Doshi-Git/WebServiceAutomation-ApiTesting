using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp_FrameWork.APIHelper;
using RestSharp_FrameWork.APIHelper.APIRequest;
using RestSharp_FrameWork.APIHelper.Client;
using RestSharp_FrameWork.APIHelper.Command;
using RestSharp_FrameWork.APIHelper.RestAPIExecutor;
using RestSharpLatest.APIHelper.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RestSharp_FrameWork.GetEndPoints
{
    [TestClass]
    public class TestGetSecureEndPoints
    {
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/secure/all";


        private static IClient _client;
        private static IClient authClient;
        private static RestApiExecutor _executor;
        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _client = new TracerClient();
            authClient = new AuthDecorator(_client, new HttpBasicAuthenticator("admin", "welcome")); // USing Framework authDecorator class
            // authClient = new BasicAuthDecorator(_client); Using BasicAuth CLass
            _executor = new RestApiExecutor();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            authClient?.Dispose();
        }

        [TestMethod]
        public void TestGetSecurewithBasicAuth()
        {
            RestClient restClient = new RestClient()
            {
                Authenticator = new HttpBasicAuthenticator("admin", "welcome")
            };

            //Create request

            RestRequest request = new RestRequest()
            {
                Method = Method.Get,
                Resource = getUrl
            };

            var response = restClient.ExecuteGet(request);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }


        [TestMethod]
        public void TestSecureGetUsingDecorator()
        {
            var getReq = new GetRequestBuilder().WithUrl(getUrl);
            var comand = new RequestCommand(getReq,authClient);
            _executor.SetCommand(comand);
            var response = _executor.ExecuteRequest();
            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);

        }

    }
}
