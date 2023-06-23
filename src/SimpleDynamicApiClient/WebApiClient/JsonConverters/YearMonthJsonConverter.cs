using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Soneta.Langs;

namespace Soneta.Types.JsonConverters {
    [TranslateIgnore]
    public class YearMonthJsonConverter : BaseJsonConverter {
        public YearMonthJsonConverter(Type type) : base(type) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.String)
                return TypeDescriptor.GetConverter(typeof(YearMonth)).ConvertFrom(reader.Value);

            if (reader.TokenType == JsonToken.StartObject) {
                JObject item = JObject.Load(reader);
                if(item["Year"] != null && item["Month"] != null) {
                    var year = item["Year"].Value<int>();
                    var month = item["Month"].Value<int>();
                    return new YearMonth(year, month);
                }
            }
            return YearMonth.Empty;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var ym = (YearMonth)value;
            if (ym.IsNull) writer.WriteNull();
            else ym.ToJObject().WriteTo(writer);
        }
    }
}
