using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.APIHelper.APIRequest
{
    public class GetRequestBuilder : AbstractRequest
    {

        private readonly RestRequest _restRequest;

        public GetRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Get
            };
        }
        public override RestRequest Build()
        {
            return _restRequest;
        }

        //url
        public GetRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        //Req Header
        public GetRequestBuilder WithHeader(Dictionary<string, string> headers)
        {
            WithHeader(headers, _restRequest);
            return this;
        }

    }
}
