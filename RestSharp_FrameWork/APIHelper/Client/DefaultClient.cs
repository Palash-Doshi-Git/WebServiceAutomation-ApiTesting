﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.APIHelper
{
    public class DefaultClient : IClient
    {
        private RestClient _client;
        private readonly RestClientOptions _restClientOptions;

        public DefaultClient()
        {
            _restClientOptions = new RestClientOptions();
        }
         
        public void Dispose()
        {
            _client?.Dispose();
        }

        public RestClient GetClient()
        {
            _restClientOptions.ThrowOnDeserializationError  = true;
            _client = new RestClient(_restClientOptions);
            return _client;
        }
    }
}
