using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.APIHelper
{
    public abstract class AbstractRequest
    {
        public abstract RestRequest Build();

        //ForUrl

        protected virtual void WithUrl(string url, RestRequest restRequest)
        {
            restRequest.Resource = url;
        }
        protected virtual void WithHeader(Dictionary<string, string> header, RestRequest restRequest)
        {
            foreach (string key in header.Keys)
                restRequest.AddOrUpdateHeader(key, header[key]);

        }

        //QueryParameter
        //Url Segements



    }
}
