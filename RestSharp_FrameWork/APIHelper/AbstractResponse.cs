using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.APIHelper
{
    public abstract class AbstractResponse : IResponse
    {
        private readonly RestResponse _restResponse;

        public AbstractResponse(RestResponse response)
        {
            _restResponse = response;
        }
        public Exception GetException()
        {
            return _restResponse.ErrorException;
        }

        public HttpStatusCode GetHttpStatusCode()
        {
            return _restResponse.StatusCode;
        }

        public abstract string GetResponseData();
       
       
    }

    public abstract class AbstractResponse<T> : IResponse<T>
    {


        private readonly RestResponse<T> _restResponse;

        public AbstractResponse(RestResponse<T> response)
        {
            _restResponse = response;
        }

        public Exception GetException()
        {
            return _restResponse.ErrorException;
        }

        public HttpStatusCode GetHttpStatusCode()
        {
            return _restResponse.StatusCode;
        }
        public abstract T GetResponseData();
        
    }
}
