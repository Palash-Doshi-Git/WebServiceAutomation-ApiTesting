using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebServiceAutomation.Helper.Response_Data
{
    public class ResponseDataHelper
    {
        //Json
        //XML
        //Static

        public static T DeserializeJSonResponse<T>(string responseData) where T : class
        {
            return JsonConvert.DeserializeObject<T>(responseData);
        }

        public static T DeserializeXmlResponse<T>(string responseData) where T : class
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            TextReader textReader = new StringReader(responseData);
            return (T)xmlSerializer.Deserialize(textReader);
        }

    }
}
