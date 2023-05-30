using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.APIHelper.ApiResponse
{
    public class Response : AbstractResponse
    {
        private readonly RestResponse _restResponse;

        public Response(RestResponse response) : base(response)
        {
            _restResponse = response;
        }

        public override string GetResponseData()
        {
            return _restResponse.Content;
        }
    }

    public class Response<T> : AbstractResponse<T>
    {

        private readonly RestResponse<T> _restResponse;

        public Response(RestResponse<T> response) : base(response)
        {
            _restResponse = response;
        }

        public override T GetResponseData()
        {
            return _restResponse.Data;
        }
    }
}
