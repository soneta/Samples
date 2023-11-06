using SimpleDynamicApi;
using Soneta.Business;
using Soneta.Types.DynamicApi;

[assembly: DynamicApiController(
    typeof(IIncorrectTypeApi),
    typeof(IncorrectTypeApi),
    Summary = "Kontroler zawierający błędnie zdefinowane metody, które zostaną zgłoszone w log jako ostrzeżenie"
)]

namespace SimpleDynamicApi
{
    public interface IIncorrectTypeApi
    {
        [DynamicApiMethod(HttpMethods.GET, nameof(GetRowById),
             Summary = "Przykładowa metoda pobierająca dane o typie IRow. " +
                       "W log zostanie wyświetlone ostrzeżenia o niepoprawnej definicji metody."
        )]
        IRow GetRowById(int id);

        
        [DynamicApiMethod(HttpMethods.POST, nameof(SetRow),
            Summary = "Przykładowa metoda przesyłająca dane o typie IRow. " +
                      "W log zostanie wyświetlone ostrzeżenia o niepoprawnej definicji metody."
        )]
        int SetRow(IRow row);

        
        [DynamicApiMethod(HttpMethods.GET, nameof(GetGuidedRowById),
            Summary = "Przykładowa metoda pobierająca dane o typie IGuidedRow. " +
                      "W log zostanie wyświetlone ostrzeżenia o niepoprawnej definicji metody."
        )]
        IGuidedRow GetGuidedRowById(int id);

        
        [DynamicApiMethod(HttpMethods.POST, nameof(SetGuidedRow),
            Summary = "Przykładowa metoda przesyłająca dane o typie IGuidedRow. " +
                      "W log zostanie wyświetlone ostrzeżenia o niepoprawnej definicji metody."
        )]
        int SetGuidedRow(IGuidedRow row);
    }
}
