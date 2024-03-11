using System.Collections.Generic;
using SimpleDynamicApi.Models;
using Soneta.Types;
using Soneta.Types.DynamicApi;

namespace SimpleDynamicApi
{
    public interface ISimpleDataApi
    {
        [DynamicApiMethod(HttpMethods.GET, nameof(GetObjectData),
            Summary = "Przykładowa metoda zwracająca obiet typu SimpleData",
            ImplementationNotes =
                "Podczas implementacji metod zwracających obiekty własnych klas należy zwrócić uwagę aby nie dziedziczyły po klasie Row, "+
                "która jest ścieśle związana z logiką biznesową i z uwagi na swoją złożoność nie może podlegać serializacji do JSON."+
                "Należy zwrócić uwagę w jakim formacie zwracane są poszczególne property w odpowiedzi po stronie klienta."
        )]
        SimpleData GetObjectData();

        [DynamicApiMethod(HttpMethods.POST, nameof(PostObjectData),
            Summary = "Przykładowa metoda przesyłająca w parametrach obiekt typu SimpleData",
            ImplementationNotes =
                "Podczas implementacji metod przesyłających obiekty własnych klas należy zwrócić uwagę aby nie dziedziczyły po klasie Row, " +
                "która jest ścieśle związana z logiką biznesową i z uwagi na swoją złożoność nie może podlegać serializacji do JSON." +
                "Należy zwrócić uwagę w jakim formacie można przesyłać property obiektu po stronie klienta."
        )]
        SimpleData PostObjectData(SimpleData data);


        [DynamicApiMethod(HttpMethods.GET, nameof(GetListData),
            Summary = "Przykładowa metoda zwracająca listę obiektów typu SimpleData"
        )]
        List<SimpleData> GetListData();

        [DynamicApiMethod(HttpMethods.POST, nameof(PostListData),
            Summary = "Przykładowa metoda przesyłająca listę obiektów typu PostListData"
        )]
        List<SimpleData> PostListData(List<SimpleData> list);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostStringData), 
            MediaType = "text/json",
            Summary = "Przykładowa metoda przesyłająca parametr typu string",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        string PostStringData(string strValue);


        [DynamicApiMethod(HttpMethods.GET, nameof(GetTwoStringData),
            MediaType = "text/json",
            Summary = "Przykładowa metoda przesyłająca parametry typu string",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        string GetTwoStringData(string strValue, string strValue2);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostIntData),
            Summary = "Przykładowa metoda przesyłająca parametry typu int",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        int PostIntData(int intValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostBoolData),
            Summary = "Przykładowa metoda przesyłająca parametry typu int",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        bool PostBoolData(bool boolValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostDecimalData),
            Summary = "Przykładowa metoda przesyłająca parametry typu int",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        decimal PostDecimalData(decimal decimalValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostEnumData),
            Summary = "Przykładowa metoda przesyłająca parametry typu enum",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        SimpleDataEnum PostEnumData(SimpleDataEnum enumValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostFloatData),
            Summary = "Przykładowa metoda przesyłająca parametry typu float",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        float PostFloatData(float floatValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostAmountData),
            Summary = "Przykładowa metoda przesyłająca parametry typu Amount",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        Amount PostAmountData(Amount amountValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostCurrencyData),
            Summary = "Przykładowa metoda przesyłająca parametry typu Currency",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        Currency PostCurrencyData(Currency currencyValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostDateData),
            Summary = "Przykładowa metoda przesyłająca parametry typu Date",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        Date PostDateData(Date dateValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostDoubleCyData),
            Summary = "Przykładowa metoda przesyłająca parametry typu DoubleCy",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        DoubleCy PostDoubleCyData(DoubleCy doubleCyValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostFractionData),
            Summary = "Przykładowa metoda przesyłająca parametry typu Fraction",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        Fraction PostFractionData(Fraction fractionValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostFromToData),
            Summary = "Przykładowa metoda przesyłająca parametry typu FromTo",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        FromTo PostFromToData(FromTo fromToValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostPercentData),
            Summary = "Przykładowa metoda przesyłająca parametry typu Percent",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        Percent PostPercentData(Percent percentValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostTimeData),
            Summary = "Przykładowa metoda przesyłająca parametry typu Time",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        Time PostTimeData(Time timeValue);


        [DynamicApiMethod(HttpMethods.POST, nameof(PostYearMonthData),
            Summary = "Przykładowa metoda przesyłająca parametry typu YearMonth",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        YearMonth PostYearMonthData(YearMonth yearMonthValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostArrayOfInt),
            Summary = "Przykładowa metoda przesyłająca parametry typu tablicy integer",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        int[] PostArrayOfInt(int[] arrayOfInt);
        
        [DynamicApiMethod(HttpMethods.POST, nameof(PostArrayOfPercent),
            Summary = "Przykładowa metoda przesyłająca parametry jako tablica obiektów typu Percent",
            ImplementationNotes = "Należy zwrócić uwagę jak budowane są parametry kontrolera."
        )]
        Percent[] PostArrayOfPercent(Percent[] array);


        [DynamicApiMethod(HttpMethods.PUT, nameof(MethodWithException),
            Summary = "Przykładowa metoda rzucająca wyjątek, dla testu zwracanego wyjątku"
        )]
        string MethodWithException();

        [DynamicApiMethod(HttpMethods.POST, nameof(GetObjectWithSameClassRef),
            Summary = "Przykładowa metoda zwracająca dane typu RefDocument, który posiada property tego samego typu co klasa zwracanego obiektu. "
        )]
        RefDocument GetObjectWithSameClassRef(RefDocument refDoc);
        
        [DynamicApiMethod(HttpMethods.POST, nameof(GetListOfObjectWithSameClassRef),
            Summary = "Przykładowa metoda zwracająca dane typu List<RefDocument>, który posiada property tego samego typu co klasa zwracanego obiektu. "
        )]
        List<RefDocument> GetListOfObjectWithSameClassRef();
    }
}
