﻿using System;
using System.Collections.Generic;
using SimpleDynamicApi;
using SimpleDynamicApi.Models;
using Soneta.Business;
using Soneta.Core;
using Soneta.Types;
using Soneta.Types.DynamicApi;

[assembly: Service(
    typeof(ISimpleDataApi),
    typeof(SimpleDataApi), 
    ServiceScope.Session)]

[assembly: DynamicApiController(
    typeof(ISimpleDataApi),
    typeof(SimpleDataApi))]

namespace SimpleDynamicApi
{
    public class SimpleDataApi: ISimpleDataApi
    {
        private readonly SimpleData DefaultData = new SimpleData();

        public SimpleData GetObjectData() => DefaultData;

        public SimpleData PostObjectData(SimpleData data)
        {
            var modifiedData = new SimpleData();

            if(data.AmountValue != DefaultData.AmountValue)
              modifiedData.AmountValue = data.AmountValue;

            if(data.BoolValue != DefaultData.BoolValue)
              modifiedData.BoolValue = data.BoolValue;

            if(data.CurrencyValue != DefaultData.CurrencyValue)
              modifiedData.CurrencyValue = data.CurrencyValue;

            if(data.DecimalValue != DefaultData.DecimalValue)
              modifiedData.DecimalValue = data.DecimalValue;

            if(data.DateValue != DefaultData.DateValue)
              modifiedData.DateValue = data.DateValue;

            if(data.DateTimeValue != DefaultData.DateTimeValue)
              modifiedData.DateTimeValue = data.DateTimeValue;

            if(data.DoubleCyValue != DefaultData.DoubleCyValue)
              modifiedData.DoubleCyValue = data.DoubleCyValue;

            if(data.EnumValue != DefaultData.EnumValue)
              modifiedData.EnumValue = data.EnumValue;

            if(data.FractionValue != DefaultData.FractionValue)
              modifiedData.FractionValue = data.FractionValue;

            if(data.FloatValue != DefaultData.FloatValue)
              modifiedData.FloatValue = data.FloatValue;

            if(data.FromToValue != DefaultData.FromToValue)
              modifiedData.FromToValue = data.FromToValue;

            if(data.IntValue != DefaultData.IntValue)
              modifiedData.IntValue = data.IntValue;

            if(data.PercentValue != DefaultData.PercentValue)
              modifiedData.PercentValue = data.PercentValue;

            if(data.StringValue != DefaultData.StringValue)
              modifiedData.StringValue = data.StringValue;

            if(data.TimeValue != DefaultData.TimeValue)
              modifiedData.TimeValue = data.TimeValue;

            if(data.YearMonthValue != DefaultData.YearMonthValue)
              modifiedData.YearMonthValue = data.YearMonthValue;

            if(data.Wojewodztwo != DefaultData.Wojewodztwo)
              modifiedData.Wojewodztwo = data.Wojewodztwo;

            modifiedData.IntervalValue = new Interval(data.DateValue, data.DateValue.AddMonths(1));
            modifiedData.PeriodsValue = Periods.New(data.FromToValue).Add(data.DateValue).Add(data.DateValue.AddMonths(1));

            return modifiedData;
        }

        public List<SimpleData> GetListData() {
          var rnd = new Random();
          var maxVal = rnd.Next(1, 20);
          var list = new List<SimpleData>();
          for(var i=0; i<maxVal; i++) {
            list.Add(new SimpleData() {
              AmountValue = new Amount(i, "MB"),
              CurrencyValue = new Currency(i*100.00, "PLN"),
              DoubleCyValue = new DoubleCy(i*100.00, "PLN"),
              Wojewodztwo = (Wojewodztwa) new Random().Next(17),
              EnumValue = (SimpleDataEnum) new Random().Next(3),
              YearMonthValue = new YearMonth(Date.Today.Year, new Random().Next(1, 12)),
              DateValue = new Date(Date.Today).AddDays(i),
              IntervalValue = new Interval(new Date(Date.Today).AddDays(i)),
              DateTimeValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(i),
              FromToValue = FromTo.Day(new Date(Date.Today).AddDays(i)),
              DecimalValue = (decimal)i,
              IntValue = i+1,
              StringValue = $"dataItem{i+1}",
            });
          }
          return list;
      }

        public string PostStringData(string strValue) => strValue;

        public string GetTwoStringData(string strValue, string strValue2) => strValue + strValue2;

        public int PostIntData(int intValue) => intValue;

        public bool PostBoolData(bool boolValue) => boolValue;

        public decimal PostDecimalData(decimal decimalValue) => decimalValue;

        public SimpleDataEnum PostEnumData(SimpleDataEnum enumValue) => enumValue;

        public float PostFloatData(float floatValue) => floatValue;

        public Amount PostAmountData(Amount amountValue) => amountValue;

        public Currency PostCurrencyData(Currency currencyValue) => currencyValue;

        public Date PostDateData(Date dateValue) => dateValue;

        public DoubleCy PostDoubleCyData(DoubleCy doubleCyValue) => doubleCyValue;

        public Fraction PostFractionData(Fraction fractionValue) => fractionValue;

        public FromTo PostFromToData(FromTo fromToValue) => fromToValue;

        public Percent PostPercentData(Percent percentValue) => percentValue;

        public Time PostTimeData(Time timeValue) => timeValue;

        public YearMonth PostYearMonthData(YearMonth yearMonthValue) => yearMonthValue;
    }
}
