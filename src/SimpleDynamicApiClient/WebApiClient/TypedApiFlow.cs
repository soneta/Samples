namespace WebApiClient
{
  public class TypedApiFlow : ApiFlow, IDisposable
    {
        readonly ApiClient _apiClient;

        public TypedApiFlow(string apiUri, string apiToken) : base(apiUri, apiToken) 
            => _apiClient = new ApiClient(apiUri, _httpClient);

        public override void Login(Action<object> onSucceed, Action<object> onError = null)
        {
            SetRequestAuthorizationToken(_apiToken);
            ProceedApiMethod<LoginApiResult>(_apiClient.LoginApiAsync(), onSucceed, onError);
        }

        public void InvokeApiMethod<TResult>(string methodName, Action<TResult> onSucceed, Action<object> onError = null)
        {
            var mi = _apiClient.GetType().GetMethod(methodName, Type.EmptyTypes);
            var swaggerResultObject = mi?.Invoke(_apiClient, new object[] { });
            ProceedApiMethod<TResult>(swaggerResultObject, onSucceed, onError);
        }


        public void InvokeApiMethod<TParams, TResult>(string methodName, TParams tparams, 
            Action<TResult> onSucceed, Action<object> onError = null)
        {
            var mi = _apiClient.GetType().GetMethod(methodName, new Type[] { typeof(TParams), typeof(TParams) });
            var swaggerResultObject = mi?.Invoke(_apiClient, new object[] { tparams, tparams });
            ProceedApiMethod<TResult>(swaggerResultObject, onSucceed, onError);
        }

        void ProceedApiMethod<T>(object methodTask, Action<T> onSucceed, Action<object> onError = null)
        {
            var typedTask = (Task<SwaggerResponse<T>>)methodTask;
            var swaggerResponse = typedTask.GetAwaiter().GetResult();
            var methodResut = swaggerResponse.Result;
            var token = swaggerResponse.Headers["Authorization"].First();
            SetRequestAuthorizationToken(token);
            onSucceed?.Invoke(methodResut);
        }

        protected override void Dispose()
        {
            _apiClient?.LogoutApiAsync();
            base.Dispose();
        }
    }
}