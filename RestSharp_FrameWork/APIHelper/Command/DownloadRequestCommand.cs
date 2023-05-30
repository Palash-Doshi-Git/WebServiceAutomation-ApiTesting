using RestSharp;
using RestSharp_FrameWork.APIHelper.APIRequest;
using System.Threading.Tasks;

namespace RestSharp_FrameWork.APIHelper.Command
{
    public class DownloadRequestCommand : ICommand
    {
        private readonly AbstractRequest _abstractRequest;
        private readonly IClient _client;

        public DownloadRequestCommand(AbstractRequest abstractRequest, IClient client)
        {
            _abstractRequest = abstractRequest;
            _client = client;
        }


        public byte[] DownloadData()
        {
            var client = _client.GetClient();
            var request = _abstractRequest.Build();
            var data = client.DownloadData(request);
            return data;

        }

        public Task<byte[]> DownloadDataAsync()
        {
            var client = _client.GetClient();
            var request = _abstractRequest.Build();
            return client.DownloadDataAsync(request);
             
        }

        public IResponse ExecuteRequest()
        {
            throw new System.NotImplementedException("Use the RequestCommand.cs for execute request");
        }

        public IResponse<T> ExecuteRequest<T>()
        {
            throw new System.NotImplementedException("Use the RequestCommand.cs for execute request");
        }
    }
}
