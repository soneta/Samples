using System;
using Newtonsoft.Json.Linq;

namespace Soneta.Types.JsonConverters {
    internal static class JObjectConverter {
        internal static JObject ToJObject(this FromTo ft) {
            return new JObject {
                new JProperty(nameof(ft.From), new DateTime(ft.From.Year, ft.From.Month, ft.From.Day)),
                new JProperty(nameof(ft.To), new DateTime(ft.To.Year, ft.To.Month, ft.To.Day))
            };
        }

        internal static JObject ToJObject(this Interval interval) {
            return new JObject {
                new JProperty(nameof(interval.Start), interval.Start),
                new JProperty(nameof(interval.End), interval.End)
            };
        }

        internal static JObject ToJObject(this Currency currency) {
            return new JObject {
                new JProperty(nameof(currency.Symbol), currency.Symbol),
                new JProperty(nameof(currency.Value), currency.Value)
            };
        }

        internal static JObject ToJObject(this DoubleCy doubleCy) {
            return new JObject {
                new JProperty(nameof(doubleCy.Symbol), doubleCy.Symbol),
                new JProperty(nameof(doubleCy.Value), doubleCy.Value)
            };
        }

        internal static JObject ToJObject(this YearMonth yearMonth) {
            return new JObject {
                new JProperty(nameof(yearMonth.Year), yearMonth.Year),
                new JProperty(nameof(yearMonth.Month), yearMonth.Month)
            };
        }

        internal static JObject ToJObject(this Fraction fraction) {
            return new JObject {
                new JProperty(nameof(fraction.Num), fraction.Num),
                new JProperty(nameof(fraction.Den), fraction.Den)
            };
        }

        internal static JObject ToJObject(this Amount amount) {
            return new JObject {
                new JProperty(nameof(amount.Symbol), amount.Symbol),
                new JProperty(nameof(amount.Value), amount.Value)
            };
        }
    }
}
