using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Soneta.Langs;

namespace Soneta.Types.JsonConverters {
    [TranslateIgnore]
    public class AmountJsonConverter : BaseJsonConverter {
        public AmountJsonConverter(Type type) : base(type) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Float)
                return TypeDescriptor.GetConverter(typeof(Amount)).ConvertFrom(reader.Value);

            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject item = JObject.Load(reader);
                if(item["Symbol"] != null && item["Value"] != null) {
                    var symbol = item["Symbol"].Value<string>();
                    var value = item["Value"].Value<double>();
                    return new Amount(value, symbol);
                }
            }
            return Amount.Empty;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var amount = (Amount)value;
            if(amount.IsNull) writer.WriteNull();
            else amount.ToJObject().WriteTo(writer);
        }
    }
}
