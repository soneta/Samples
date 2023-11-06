using SimpleDynamicApi;
using Soneta.Types.DynamicApi;

[assembly: DynamicApiController(
    typeof(IWithoutMethodsApi),
    typeof(WithoutMethodsApi),
    Summary = "Kontroler nie zawierający metod zostanie zgłoszony w log jako ostrzeżenie"
)]

namespace SimpleDynamicApi
{
    public interface IWithoutMethodsApi
    {
    }
}
