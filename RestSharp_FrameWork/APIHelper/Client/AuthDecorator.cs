using RestSharp;
using RestSharp.Authenticators;

namespace RestSharp_FrameWork.APIHelper.Client
{
    public class AuthDecorator : IClient
    {
        private readonly IClient _client;
        private readonly AuthenticatorBase _authBase;

        public AuthDecorator(IClient client, AuthenticatorBase authBase)
        {
            _client = client;
            _authBase = authBase;
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
            newClient.Authenticator = _authBase;

            //3. return the new client
            return newClient;
        }
    }
}
