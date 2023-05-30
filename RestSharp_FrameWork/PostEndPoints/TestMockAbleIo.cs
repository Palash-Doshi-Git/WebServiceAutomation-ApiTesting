using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp_FrameWork.APIHelper;
using RestSharp_FrameWork.APIHelper.APIRequest;
using RestSharp_FrameWork.APIHelper.Client;
using RestSharp_FrameWork.APIHelper.Command;
using RestSharp_FrameWork.APIHelper.RestAPIExecutor;
using System.Diagnostics;

namespace RestSharp_FrameWork.PostEndPoints
{
    [TestClass]
    public class TestBigData
    {
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/find/";
        private static IClient _client;

        private static RestApiExecutor restApiExecutor;

        // Mockable IO to be Updated in the Course and should give explaination
        // For Now add data thought WebServie -> TEstPostEndPoints
        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            _client = new TracerClient();
            restApiExecutor = new RestApiExecutor();

        }


        [TestMethod]
        public void ParseAndValidateTheJson()
        {
           

           int id = 30;
            var getRequest = new GetRequestBuilder().WithUrl(getUrl+id);

            var command = new RequestCommand(getRequest, _client);

            restApiExecutor.SetCommand(command);
            var response = restApiExecutor.ExecuteRequest();

            response.GetHttpStatusCode().Should().Be(System.Net.HttpStatusCode.OK);
            var responseData = response.GetResponseData();
            Debug.WriteLine(responseData);
            

            // Parse The give JSon Document
            JObject jsonValidate = JObject.Parse(responseData);

          /*  var BrandName = jsonValidate.SelectToken("$["+id+"].BrandName").ToString();

            BrandName.Should().Be("Alienware" + id);*/
            //Use the Json Path Query on the json Document

            // Type casr the result of the query if needed

        }



        [ClassCleanup]
        public static void TearDown()
        {
            _client?.Dispose();
        }



    }
}
