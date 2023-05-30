using HttpTracer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.APIHelper.Client
{
    public class TracerClient : IClient
    {
        private readonly RestClientOptions _restClientOptions;
        private RestClient _restClient;

        public TracerClient()
        {
            _restClientOptions = new RestClientOptions();

        }

        private HttpMessageHandler TraceConfig(HttpMessageHandler handler)
        {
            var tracer = new HttpTracerHandler(handler, HttpMessageParts.All);
            return tracer;

        }


        public void Dispose()
        {
            _restClient?.Dispose();
        }

        public RestClient GetClient()
        {
           // _restClientOptions.ConfigureMessageHandler = TraceConfig;
            _restClientOptions.ConfigureMessageHandler = (handler) =>
            {
                return new HttpTracerHandler(handler, HttpMessageParts.All);
            };
            _restClientOptions.ThrowOnDeserializationError = true;

            _restClient = new RestClient(_restClientOptions);

            return _restClient;
        }
    }
}
