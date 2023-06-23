using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WebApiClient
{
  public class AnonymousApiFlow : ApiFlow
    {
        public AnonymousApiFlow(string apiUri, string apiToken) : base(apiUri, apiToken) { }


        public override void Login(Action<object> onSuccess, Action<object> onError = null)
        {
            SetRequestAuthorizationToken(_apiToken);
            InvokeApiMethod("POST", "LoginApi", null, onSuccess, onError);
        }


        public void InvokeApiMethod(string methodType, string methodName, object methodArgs, 
            Action<dynamic> onSucceed = null, Action<object> onError = null)
        {
            dynamic args = methodArgs;
            dynamic result = new { };

            using var request = PrepareRequest(methodName, methodType);
            if (args != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> methodTask = _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            var response = methodTask.GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                if (response.Headers.Contains("Authorization"))
                {
                    var token = response.Headers.GetValues("Authorization").First();
                    SetRequestAuthorizationToken(token);
                }
                onSucceed?.Invoke(result);
            }
            else
            {
                onError?.Invoke(response.ToString());
            }
        }


        HttpRequestMessage PrepareRequest(string methodName, string methodType)
        {
            var urlBuilder_ = new StringBuilder();
            urlBuilder_.Append(_apiUri).Append("/api/").Append(methodName);

            var request_ = new HttpRequestMessage();
            request_.Method = new HttpMethod(methodType);
            request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            var url_ = urlBuilder_.ToString();
            request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
            return request_;
        }

        protected override void Dispose()
        {
            InvokeApiMethod("POST", "LogoutApi", null);
            base.Dispose();
        }
    }
}