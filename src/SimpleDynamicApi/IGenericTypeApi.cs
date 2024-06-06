using System.Collections.Generic;
using SimpleDynamicApi;
using SimpleDynamicApi.Models;
using Soneta.Types.DynamicApi;

[assembly: DynamicApiController(
    typeof(IGenericTypeApi),
    typeof(GenericTypeApi),
    Summary = "Przykładowy kontroler pokazujący możliwości związane z wymianą danych poprzez użycie typów generycznych."
)]

namespace SimpleDynamicApi
{
    public interface IGenericTypeApi
    {
        [DynamicApiMethod(HttpMethods.GET, nameof(GetInheritedFromDataResponseGenericAsObjectDto),
            MediaType = "text/json",
            Summary = "Przykładowa metoda pobierająca obiekt klasy dziedziczącej po klasie generycznej"
        )]
        DataResponseObjectDto GetInheritedFromDataResponseGenericAsObjectDto(string kod);
        
        
        [DynamicApiMethod(HttpMethods.GET, nameof(GetGenericDataResponseAsObjectDto),
            MediaType = "text/json",
            Summary = "Przykładowa metoda pobierająca uniwersalny obiekt opakowujący paczkę wymiany danych typu generycznego"
        )]
        DataResponseGeneric<ObjectDto> GetGenericDataResponseAsObjectDto(string kod);
        
        
        [DynamicApiMethod(HttpMethods.GET, nameof(GetGenericDataResponseAsExtendedObjectDto),
            MediaType = "text/json",
            Summary = "Przykładowa metoda pobierająca uniwersalny obiekt opakowujący paczkę wymiany danych typu generycznego"
        )]
        DataResponseGeneric<ExtendedObjectDto> GetGenericDataResponseAsExtendedObjectDto(string kod);

        
        [DynamicApiMethod(HttpMethods.GET, nameof(GetGenericDataResponseAsListExtendedObjectDto),
            MediaType = "text/json",
            Summary = "Przykładowa metoda pobierająca uniwersalny obiekt opakowujący paczkę wymiany danych typu generycznego"
        )]
        DataResponseGeneric<List<ExtendedObjectDto>> GetGenericDataResponseAsListExtendedObjectDto(string kod);

        
        [DynamicApiMethod(HttpMethods.POST, nameof(PostGenericDataResponseAsObjectDto),
            Summary = "Przykładowa metoda przesyłająca uniwersalny obiekt opakowujący paczkę wymiany danych typu generycznego"
        )]
        DataResponseGeneric<ObjectDto> PostGenericDataResponseAsObjectDto(DataResponseGeneric<ObjectDto> args);
    }
}