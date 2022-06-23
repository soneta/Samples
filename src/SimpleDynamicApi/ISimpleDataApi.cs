using System.Collections.Generic;
using SimpleDynamicApi.Models;
using Soneta.Types;
using Soneta.Types.DynamicApi;

namespace SimpleDynamicApi
{
    public interface ISimpleDataApi
    {
        [DynamicApiMethod(HttpMethods.GET, nameof(GetObjectData))]
        SimpleData GetObjectData();

        [DynamicApiMethod(HttpMethods.POST, nameof(PostObjectData))]
        SimpleData PostObjectData(SimpleData data);

        [DynamicApiMethod(HttpMethods.GET, nameof(GetListData))]
        List<SimpleData> GetListData();

        [DynamicApiMethod(HttpMethods.POST, nameof(PostStringData), MediaType = "text/json")]
        string PostStringData(string strValue);

        [DynamicApiMethod(HttpMethods.GET, nameof(GetTwoStringData), MediaType = "text/json")]
        string GetTwoStringData(string strValue, string strValue2);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostIntData))]
        int PostIntData(int intValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostBoolData))]
        bool PostBoolData(bool boolValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostDecimalData))]
        decimal PostDecimalData(decimal decimalValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostEnumData))]
        SimpleDataEnum PostEnumData(SimpleDataEnum enumValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostFloatData))]
        float PostFloatData(float floatValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostAmountData))]
        Amount PostAmountData(Amount amountValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostCurrencyData))]
        Currency PostCurrencyData(Currency currencyValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostDateData))]
        Date PostDateData(Date dateValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostDoubleCyData))]
        DoubleCy PostDoubleCyData(DoubleCy doubleCyValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostFractionData))]
        Fraction PostFractionData(Fraction fractionValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostFromToData))]
        FromTo PostFromToData(FromTo fromToValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostPercentData))]
        Percent PostPercentData(Percent percentValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostTimeData))]
        Time PostTimeData(Time timeValue);

        [DynamicApiMethod(HttpMethods.POST, nameof(PostYearMonthData))]
        YearMonth PostYearMonthData(YearMonth yearMonthValue);
    }
}
