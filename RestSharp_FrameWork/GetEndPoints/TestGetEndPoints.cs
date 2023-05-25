using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;

namespace RestSharp.Automation.GetRequest
{

    [TestClass]
    public class TestGetRequest
    {
        private readonly string getUrl = "http://localhost:8080/laptop-bag/webapi/api/all";

        [TestMethod]
        public void GetRequest()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);
            RestResponse response = client.ExecuteGet(getRequest);

            Debug.WriteLine($"Response Status code - {response.StatusCode}");
            Debug.WriteLine($"Error Message - {response.ErrorMessage}");
            Debug.WriteLine($"Eception - {response.ErrorException}");
        }

        [TestMethod]
        public void GetRequestPrintResponse()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);
            RestResponse response = client.ExecuteGet(getRequest);

            if (response.IsSuccessful)
            {
                Debug.WriteLine(response.StatusCode);
                Debug.WriteLine(response.Content);
            }
            else
            {
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");
                Debug.WriteLine($"Eception - {response.ErrorException}");
            }
        }
        [TestMethod]
        public void GetRequestInXml()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);
            RestResponse response = client.ExecuteGet(getRequest);
            //getRequest.AddHeader("Aceept", "application/xml");
            client.UseXml();

            if (response.IsSuccessful)
            {
                Debug.WriteLine(response.StatusCode);
                Debug.WriteLine(response.Content);
            }
            else
            {
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");
                Debug.WriteLine($"Eception - {response.ErrorException}");
            }
        }

        public void GetRequestInJson()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);
            RestResponse response = client.ExecuteGet(getRequest);
            //getRequest.AddHeader("Aceept", "application/xml");
            client.UseJson(); //  Default Config

            if (response.IsSuccessful)
            {
                Debug.WriteLine(response.StatusCode);
                Debug.WriteLine(response.Content);
            }
            else
            {
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");
                Debug.WriteLine($"Eception - {response.ErrorException}");
            }

        }


        [TestMethod]
        public void GetRequestJSonDeserialization()
        {
            RestClient client = new RestClient();
            RestRequest getRequest = new RestRequest(getUrl);
            RestResponse<List<JsonRootObject>> response = client.ExecuteGet<List<JsonRootObject>>(getRequest); // ExecuteGet() Which Takes the T type Parameter

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            response.StatusCode.Should().Be(HttpStatusCode.OK); // Fluent Assertion

            if (response.IsSuccessful)
            {
                Debug.WriteLine(response.StatusCode);
                Debug.WriteLine(response.Data);
                response.Data.ForEach((item) =>
                {                     // Foreach loop in list Data 
                    Debug.WriteLine($"Response ID is - {item.Id}");   // Linq statement for debug
                });

                JsonRootObject jsonObj = response.Data.Find((item) =>
                {
                    return item.Id == 1;
                });


                jsonObj.BrandName.Should().NotBeEmpty();// Fluent Assertion

                jsonObj.BrandName.Should().Be("Alienware");// Fluent Assertion 

                /** Error Message of Fluent Assertion 
                 *   Expected jsonObj.BrandName to be "Alienware21" with a length of 11,
                 *   but "Alienware" has a length of 9, differs near "e" (index 8).
                 * */

                Assert.AreEqual("Alienware", jsonObj.BrandName); // Normal Assertion

                /** Error Message of Normal Assertion 
                 *   Assert.AreEqual failed. Expected:<Alienwar21>. Actual:<Alienware>.
                 * */

                jsonObj.Features.Feature.Should().NotBeEmpty();


                Assert.IsTrue(jsonObj.Features.Feature.Contains("8GB, 2x4GB, DDR4, 2666MHz"), "Feature Not found");
            }
            else
            {
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");
                Debug.WriteLine($"Eception - {response.ErrorException}");
            }
        }

        [TestMethod]
        public void GetRequestXmlDeserialization()
        {
            RestClient client = new RestClient();
            client.UseXml();

            RestRequest getRequest = new RestRequest(getUrl);
            RestResponse<LaptopDetailss> response = client.ExecuteGet<LaptopDetailss>(getRequest); // ExecuteGet() Which Takes the T type Parameter

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            response.StatusCode.Should().Be(HttpStatusCode.OK); // Fluent Assertion

            if (response.IsSuccessful)
            {
                Debug.WriteLine(response.StatusCode);
                Debug.WriteLine(response.Data);
                response.Data.Laptop.ForEach((item) =>
                {                     // Foreach loop in list Data 
                    Debug.WriteLine($"Response ID is - {item.Id}");   // Linq statement for debug
                });

                Laptop xmlObj = response.Data.Laptop.Find((item) =>
                {
                    return "1".Equals(item.Id, StringComparison.CurrentCultureIgnoreCase);
                });


                xmlObj.BrandName.Should().NotBeEmpty();// Fluent Assertion

                xmlObj.BrandName.Should().Be("Alienware");// Fluent Assertion 

                /** Error Message of Fluent Assertion 
                 *   Expected jsonObj.BrandName to be "Alienware21" with a length of 11,
                 *   but "Alienware" has a length of 9, differs near "e" (index 8).
                 * */

                Assert.AreEqual("Alienware", xmlObj.BrandName); // Normal Assertion

                /** Error Message of Normal Assertion 
                 *   Assert.AreEqual failed. Expected:<Alienwar21>. Actual:<Alienware>.
                 * */

                xmlObj.Features.Feature.Should().NotBeEmpty();


                Assert.IsTrue(xmlObj.Features.Feature.Contains("8GB, 2x4GB, DDR4, 2666MHz"), "Feature Not found");
            }
            else
            {
                Debug.WriteLine($"Error Message - {response.ErrorMessage}");
                Debug.WriteLine($"Eception - {response.ErrorException}");
            }
        }



    }
}





