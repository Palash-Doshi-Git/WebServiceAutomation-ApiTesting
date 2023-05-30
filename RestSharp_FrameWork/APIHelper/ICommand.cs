using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.APIHelper.APIRequest
{
    public interface ICommand
    {
        IResponse ExecuteRequest(); // for response in string format 
        IResponse<T> ExecuteRequest<T>(); // for reponse in object format

    }
}
