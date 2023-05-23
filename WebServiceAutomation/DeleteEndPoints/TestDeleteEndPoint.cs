using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebServiceAutomation.Authentication;
using WebServiceAutomation.Helper.Request;
using WebServiceAutomation.Helper.Response_Data;
using WebServiceAutomation.Model;
using WebServiceAutomation.Model.JsonModel;
using WebServiceAutomation.Model.XmlModel;

namespace WebServiceAutomation.DeleteEndPoints
{   [TestClass]
    public class TestDeleteEndPoints
    {

        private readonly string postUrl = "http://localhost:8080/laptop-bag/webapi/api/add";
        private readonly string deleteUrl = "http://localhost:8080/laptop-bag/webapi/api/delete/";
        private readonly string secureDeleteUrl = "http://localhost:8080/laptop-bag/webapi/secure/delete/";
        private RestResponse restResponse;
        private string jsonMediaType = "application/json";
        private string xmlMediaType = "application/xml";
        private Random random = new Random();
        private Dictionary<string, string> jsonHeader = new Dictionary<string, string>() { { "Accept", "application/json" } };
        private Dictionary<string, string> xmlHeader = new Dictionary<string, string>() { { "Accept", "application/xml" } };


         [TestMethod]
        public void TestDeleteEndPointforJsonUsingHelperMethod()
        {
            int id = random.Next(100);
            AddJsonRecord(id);
            restResponse = HttpClientHelper.DeleteRequest(deleteUrl + id);
            Assert.AreEqual(200, restResponse.StatusCode);
            restResponse = HttpClientHelper.DeleteRequest(deleteUrl + id);
            Assert.AreEqual(404, restResponse.StatusCode);
        }

        [TestMethod]
        public void TestSecureDeleteEndPointforJson()
        {
            int id = random.Next(100);
            AddJsonRecord(id);
            string auth = Base64StringConvertor.GetBase64String("admin", "welcome");
            
            jsonHeader.Remove("Accept");            
            jsonHeader.Add("Authorization", "Basic " + auth);
            restResponse = HttpClientHelper.DeleteRequest(secureDeleteUrl + id, jsonHeader);
            Assert.AreEqual(200, restResponse.StatusCode);
            restResponse = HttpClientHelper.DeleteRequest(secureDeleteUrl + id, jsonHeader);
            Assert.AreEqual(404, restResponse.StatusCode);
        }

        [TestMethod]
        public void TestSecureDeleteEndPointwithXml()
        {
            int id = random.Next(100);
            AddXmlRecord(id);
            string auth = Base64StringConvertor.GetBase64String("admin" , "welcome");
            xmlHeader.Remove("Accept");
            xmlHeader.Add("Authorization", "Basic " + auth);
            restResponse = HttpClientHelper.DeleteRequest(secureDeleteUrl+id, xmlHeader) ;
            Assert.AreEqual(200, restResponse.StatusCode);
            restResponse = HttpClientHelper.DeleteRequest(secureDeleteUrl+id, xmlHeader);
            Assert.AreEqual(404, restResponse.StatusCode);
        }

        public void AddJsonRecord(int id)
        {
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

            restResponse = HttpClientHelper.PostRequest(postUrl, jsonData, jsonMediaType,jsonHeader);
            Assert.AreEqual(200, restResponse.StatusCode);
      
        }

        public void AddXmlRecord(int id)
        {
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
            restResponse = HttpClientHelper.PostRequest(postUrl, xmlData, xmlMediaType, xmlHeader);
            Assert.AreEqual(200, restResponse.StatusCode);
        }

    }
}
