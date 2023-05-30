using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp_FrameWork.APIModel.JsonApiModel;
using System;
using System.Diagnostics;
using WebServiceAutomation.Model.JsonModel;

namespace RestSharp_FrameWork.PostEndPoints
{
    [TestClass]
    public class TestPostEndPoints
    {
       
        private readonly string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private readonly Random random = new Random();


        [TestMethod]
        public void TestPostEndPointwithRestSharp()
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
            Debug.WriteLine(restResponse.Content);
        }

        [TestMethod]
        public void TestPostEndPointwithJsonBuilderClass()
        {

            int id = random.Next(100);
/*            var payload = new JsonRootObjectBuilder().WithId(id).WithBrandName("Test Brand").WithLaptopName("Test LaptopName").WithFeatures(new System.Collections.Generic.List<string>() { "Features", "Feature2" }).Build();
*/
             var payload = new JsonModelBuilder().WithId(id).WithBrandName("Test Brand").WithLaptopName("Test LaptopName").WithFeatures(new System.Collections.Generic.List<string>() { "Features", "Feature2" }).Build();

            RestClient restClient = new RestClient();

            //Create request

            RestRequest request = new RestRequest()
            {
                Method = Method.Post,
                Resource = postUrl
            };

            request.AddJsonBody(payload);
            var response = restClient.ExecutePost(request);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(response.Content);

        }

        [TestMethod]
        public void TestPostEndPointwithRestSharpwithXml_String()
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

            //Create Client 

            RestClient restClient = new RestClient();

            //Create request

            RestRequest request = new RestRequest()
            {
                Method = Method.Post,
                Resource = postUrl
            };

            // Add Body

            request.AddStringBody(xmlData, DataFormat.Xml) ;
            request.AddHeader("Accept", "application/xml");
            // send request
            RestResponse restResponse = restClient.ExecutePost(request);
            restResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            Debug.WriteLine(restResponse.Content);
            restClient.Dispose();
        }


    }
}
