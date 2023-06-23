using System.Net.Http.Headers;

namespace WebApiClient
{
  public abstract class ApiFlow : IDisposable
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _apiToken;
        protected readonly string _apiUri;

        internal ApiFlow(string apiUri, string apiToken)
        {
            _httpClient = new HttpClient();
            _apiToken = apiToken;
            _apiUri = apiUri;
        }


        public abstract void Login(Action<object> onSuccess, Action<object> onError = null);

        protected virtual void Dispose() =>
            _httpClient?.Dispose();

        protected void SetRequestAuthorizationToken(string token)
        {
            if(!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        void IDisposable.Dispose() =>
            Dispose();

    }
}