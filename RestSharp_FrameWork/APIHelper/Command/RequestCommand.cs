using RestSharp;
using RestSharp_FrameWork.APIHelper.APIRequest;
using RestSharp_FrameWork.APIHelper.ApiResponse;


namespace RestSharp_FrameWork.APIHelper.Command
{
    public class RequestCommand : ICommand // only purpose of the class tO get request and send response
    {
        private readonly IClient _client;
        private readonly AbstractRequest _abstractRequest;

        public RequestCommand(AbstractRequest abstractRequest, IClient client)
        {
            _abstractRequest = abstractRequest;
            _client = client;

        }
        public IResponse ExecuteRequest()
        {
            var client = _client.GetClient();
            var request = _abstractRequest.Build();
            var reponse = client.Execute(request);
            return new Response(reponse);
        }
        public IResponse<T> ExecuteRequest<T>()
        {
            var client = _client.GetClient();
            var request = _abstractRequest.Build();
            var response = client.Execute<T>(request);
            return new Response<T>(response);
        }

    }
}
