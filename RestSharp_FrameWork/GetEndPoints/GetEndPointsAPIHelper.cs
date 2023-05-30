using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp_FrameWork.APIHelper.APIRequest;
using RestSharp_FrameWork.APIHelper.Command;
using RestSharp_FrameWork.APIHelper.RestAPIExecutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using FluentAssertions;

namespace RestSharp_FrameWork.APIHelper
{
    [TestClass]
    public class GetEndPointsAPIHelper
    {
        private IClient _client; 
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";
        private RestApiExecutor executor;

        [TestInitialize]
        public void setup()
        {
            _client = new DefaultClient();
            executor = new RestApiExecutor();
        }


        [TestMethod]
        public void GetReqwithApliHelper()
        {
            var header = new Dictionary<string, string>()
            {
                { "Accept", "application/json" }
            };
            AbstractRequest request = new GetRequestBuilder().WithUrl(getUrl).WithHeader(header);

            ICommand getCommand = new RequestCommand(request, _client);
            executor.SetCommand(getCommand);
            var  response = executor.ExecuteRequest();
            // Add Assertion
            response.GetHttpStatusCode().Should().Be(HttpStatusCode.OK);
          
        }

        [TestCleanup]
        public void TearDown()
        {
            _client?.Dispose();
        }

    }
}
