﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.APIHelper
{
    public interface IResponse : IRestApiResponse
    {
        string GetResponseData();

    }

    public interface IResponse<T> : IRestApiResponse
    {
        T GetResponseData();
    }
}
