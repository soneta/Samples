using Soneta.Types.JsonConverters;

namespace WebApiClient
{
    public partial class ApiClient
    {
        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings)
            => settings.Converters = BaseJsonConverter.JsonTypeConverters;
    }
}
