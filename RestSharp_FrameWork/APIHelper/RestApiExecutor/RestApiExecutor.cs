using RestSharp_FrameWork.APIHelper.APIRequest;
using System;

namespace RestSharp_FrameWork.APIHelper.RestAPIExecutor
{
    public class RestApiExecutor
    {
        private ICommand Command;

        public void SetCommand(ICommand _command)
        {
            Command = _command;
        }

        public IResponse ExecuteRequest()
        {
            return Command.ExecuteRequest();
        }

        public IResponse<T> ExecuteRequest<T>()
        {
            return Command.ExecuteRequest<T>();
        }

      
    }
}
