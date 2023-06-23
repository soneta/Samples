using Microsoft.Extensions.Configuration;
using WebApiClient;
{
    Configure((apiUri, apiToken) => { 
        UseAnonymousApiFlow(apiUri, apiToken);
        UseTypedApiFlow(apiUri, apiToken);
    });

    #region UseAnonymousApiFlow

    void UseAnonymousApiFlow(string apiUri, string apiToken)
    {

        using var apiClient = new AnonymousApiFlow(apiUri, apiToken);
        //Logowanie do api
        apiClient.Login(result =>
        {
            // Wywołanie metody api GetObjectData
            apiClient.InvokeApiMethod("GET", "SimpleDataApi/GetObjectData", null, data =>
            {

                Console.WriteLine("  Pobrana wartość stringValue : {0}", data.StringValue);
                ProceedResult(data);

                // Wywołanie metody api PostObjectData
                apiClient.InvokeApiMethod("POST", "SimpleDataApi/PostObjectData", (object)data, result => {

                    Console.WriteLine("Zmieniona wartość stringValue : {0}", result.StringValue);

                }, err => LogError(err));
            }, err => LogError(err));
        }, err => LogError(err));
    }

    #endregion

    #region UseTypedApiFlow

    void UseTypedApiFlow(string apiUri, string apiToken)
    {

        using var apiClient = new TypedApiFlow(apiUri, apiToken);

        //Logowanie do api
        apiClient.Login(result =>
        {
            // Wywołanie metody api GetObjectData
            apiClient.InvokeApiMethod<SimpleData>(nameof(ApiClient.GetObjectDataAsync), data =>
            {

                Console.WriteLine("  Pobrana wartość stringValue : {0}", data.StringValue);
                ProceedResult(data);

                // Wywołanie metody api PostObjectData
                apiClient.InvokeApiMethod<SimpleData, SimpleData>(nameof(ApiClient.PostObjectDataAsync), data, result => {

                    Console.WriteLine("Zmieniona wartość stringValue : {0}", result.StringValue);

                }, err => LogError(err));
            }, err => LogError(err));
        }, err => LogError(err));
    }

    #endregion

    #region Utils

    void Configure(Action<string, string> action)
    {
        var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", false, false)
                .Build();
        var apiToken = config.GetSection("Token").Value ?? "";
        var apiUri = config.GetSection("ApiUri").Value ?? "";
        if (string.IsNullOrEmpty(apiUri) || string.IsNullOrEmpty(apiToken))
            throw new Exception("Brak parametrów wywołania metody");

        action.Invoke(apiUri, apiToken);
    }

    void ProceedResult(dynamic data)
    {
        var originalStringValue = data.StringValue;
        data.StringValue = $"{DateTime.Now.ToLongTimeString()} -> {originalStringValue}";
    }

    void LogError(object err)
        => Console.WriteLine("Error: {0}", err);

    #endregion

}
