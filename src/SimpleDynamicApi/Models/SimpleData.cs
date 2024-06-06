using Soneta.Core;
using Soneta.Types;
using System;

namespace SimpleDynamicApi.Models
{
    [BinSerializable]
    public class SimpleData {
        public SimpleData() {
            BoolValue = true;
            IntValue = 2;
            FloatValue = 100.5678f;
            AmountValue = Amount.Zero;
            DecimalValue = Decimal.Zero;
            CurrencyValue = new Currency(200m);
            DateValue = Date.Today;
            DateTimeValue = DateTime.Now;
            DoubleCyValue = new DoubleCy(0.5m);
            FractionValue = Fraction.Quarter;
            FromToValue = FromTo.Month(Date.Today);
            PercentValue = Percent.Zero;
            StringValue = Guid.NewGuid().ToString();
            TimeValue = Time.Now;
            YearMonthValue = YearMonth.Today;
            EnumValue = SimpleDataEnum.None;
            Wojewodztwo = Wojewodztwa.nieokreślone;
            IntervalValue = new Interval(DateValue, DateValue.AddMonths(1));
            PeriodsValue = Periods.New(FromToValue).Add(DateValue).Add(DateValue.AddMonths(-1));
        }

        public bool BoolValue { get; set; }
        public int IntValue { get; set; }
        public float FloatValue { get; set; }
        public string StringValue { get; set; }
        public decimal DecimalValue { get; set; }
        public Amount AmountValue { get; set; }
        public Currency CurrencyValue { get; set; }
        public DateTime DateTimeValue { get; set; }
        public Date DateValue { get; set; }
        public DoubleCy DoubleCyValue { get; set; }
        public Fraction FractionValue { get; set; }
        public FromTo FromToValue { get; set; }
        public Percent PercentValue { get; set; }
        public Time TimeValue { get; set; }
        public YearMonth YearMonthValue { get; set; }
        public SimpleDataEnum EnumValue { get; set; }
        public Wojewodztwa Wojewodztwo { get; set; }
        public Interval IntervalValue { get; set; }
        public Periods PeriodsValue { get; set; }
        public PłećOsoby Płeć { get; set; }
        public PłećOsoby? PłećNulowa { get; set; }
        public PłećOsoby[] Płcie { get; set; }

    }
}
