using System;
using SimpleDynamicApi;
using SimpleDynamicApi.Models;
using Soneta.Types.DynamicApi;

[assembly: DynamicApiController(
    typeof(INullableTypeApi),
    typeof(NullableTypeApi),
    Summary = "Kontroler zawierający metodę, której parametr może być nullable"
)]

namespace SimpleDynamicApi
{
    public interface INullableTypeApi
    {
        [DynamicApiMethod(HttpMethods.GET, nameof(GetDataByNullableBool),
            Summary = "Przykładowa metoda zwracająca dane typu NullableDto, dla której parametr typu bool może być nullable. "
        )]
        NullableDto GetDataByNullableBool(bool? boolValue);

        
        
        [DynamicApiMethod(HttpMethods.GET, nameof(GetDataByNullableByte),
            Summary = "Przykładowa metoda zwracająca dane typu NullableDto, dla której parametr typu byte może być nullable. "
        )]
        NullableDto GetDataByNullableByte(byte? byteValue);

        [DynamicApiMethod(HttpMethods.GET, nameof(GetDataByNullableByte),
            Summary = "Przykładowa metoda zwracająca dane typu int?[], dla której parametr typu int?[] może być nullable. "
        )]
        int?[] GetDataByNullableIntArray(int?[] intArrayValue);


        
        [DynamicApiMethod(HttpMethods.GET, nameof(GetDataByNullableDateTime),
            Summary =
                "Przykładowa metoda zwracająca dane typu NullableDto, dla której parametr typu decimal może być nullable. "
        )]
        public NullableDto GetDataByNullableDateTime(DateTime? dateTimeValue);
        
        
        
        [DynamicApiMethod(HttpMethods.GET, nameof(GetDataByNullableDecimal),
            Summary =
                "Przykładowa metoda zwracająca dane typu NullableDto, dla której parametr typu decimal może być nullable. "
        )]
        public NullableDto GetDataByNullableDecimal(decimal? decimalValue);



        [DynamicApiMethod(HttpMethods.GET, nameof(GetDataByNullableFloat),
            Summary = "Przykładowa metoda zwracająca dane typu NullableDto, dla której parametr typu float może być nullable. "
        )]
        NullableDto GetDataByNullableFloat(float? floatValue);


        
        [DynamicApiMethod(HttpMethods.GET, nameof(GetDataByNullableInt),
            Summary = "Przykładowa metoda zwracająca dane typu NullableDto, dla której parametr typu int może być nullable. "
        )]
        NullableDto GetDataByNullableInt(int? intValue);


        
        [DynamicApiMethod(HttpMethods.GET, nameof(GetDataByNullableObject),
            Summary = "Przykładowa metoda zwracająca dane typu NullableDto, dla której parametr może być nullable. "
        )]
        NullableDto GetDataByNullableObject(NullableDto nullableDto = null);
    }
}
