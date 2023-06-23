using Soneta.Langs;
using Newtonsoft.Json;

namespace Soneta.Types.JsonConverters
{
    [TranslateIgnore]
    public abstract class BaseJsonConverter : JsonConverter {
        protected readonly Type _type;

        internal static readonly JsonConverter[] JsonTypeConverters = new JsonConverter[] {
                new AmountJsonConverter(typeof(Amount)),
                new CurrencyJsonConverter(typeof(Currency)),
                new DateJsonConverter(typeof(Date)),
                new DateTimeJsonConverter(typeof(DateTime)),
                new DoubleCyJsonConverter(typeof(DoubleCy)),
                new FromToJsonConverter(typeof(FromTo)),
                new FractionJsonConverter(typeof(Fraction)),
                new IntervalJsonConverter(typeof(Interval)),
                new PeriodJsonConverter(typeof(Periods)),
                new PercentJsonConverter(typeof(Percent)),
                new TimeJsonConverter(typeof(Time)),
                new YearMonthJsonConverter(typeof(YearMonth)),
                new EnumJsonConverter()
            };

        public BaseJsonConverter(Type type) => _type = type;

        public override bool CanRead => true;
        public override bool CanWrite => true;

        public override bool CanConvert(Type objectType) => objectType == _type;
        //  _type.Any(t => t == objectType);
    }
}
