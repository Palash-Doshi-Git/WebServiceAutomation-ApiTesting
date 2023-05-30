using RestSharp;
using RestSharp.Authenticators;
using RestSharp_FrameWork.APIHelper;

namespace RestSharpLatest.APIHelper.Client
{
    public class BasicAuthDecorator : IClient
    {
        private readonly IClient _client;

        public BasicAuthDecorator(IClient client)
        {
            _client = client;
        }
        public void Dispose()
        {
            _client.Dispose();
        }

        public RestClient GetClient()
        {
            //1. Invoke _client.GetClient() API

            var newClient = _client.GetClient();

            //2. Add the auth configuration
            newClient.Authenticator = new HttpBasicAuthenticator("admin", "welcome");

            //3. return the new client
            return newClient;
        }
    }
}