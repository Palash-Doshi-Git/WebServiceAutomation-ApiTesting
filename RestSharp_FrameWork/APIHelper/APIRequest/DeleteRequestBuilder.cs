using RestSharp;
using System.Collections.Generic;

namespace RestSharp_FrameWork.APIHelper.APIRequest
{
    public class DeleteRequestBuilder : AbstractRequest
    {
        private readonly RestRequest _restRequest;

        public DeleteRequestBuilder()
        {
            _restRequest = new RestRequest()
            {
                Method = Method.Delete
            };
        }
        public override RestRequest Build()
        {
            return _restRequest;
        }

        //url
        public DeleteRequestBuilder WithUrl(string url)
        {
            WithUrl(url, _restRequest);
            return this;
        }

        //Req Header
        public DeleteRequestBuilder WithDefaultHeader()
        {
            WithHeader(null, _restRequest);
            return this;
        }

        protected override void WithHeader(Dictionary<string, string> header, RestRequest restRequest)
        {
            restRequest.AddOrUpdateHeader("Accept","text/plain");
        }

        //QueryParameter
        public DeleteRequestBuilder WithQueryParameters(Dictionary<string,string> param)
        {
            WithQueryParameters(param, _restRequest);
            return this;
        }
    }
}
