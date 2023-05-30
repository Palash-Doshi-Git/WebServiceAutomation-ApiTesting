﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.APIHelper
{
    public interface IRestApiResponse
    {
        HttpStatusCode GetHttpStatusCode();
        Exception GetException();
    }
}
