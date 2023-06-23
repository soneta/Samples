using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Soneta.Langs;

namespace Soneta.Types.JsonConverters {
    [TranslateIgnore]
    public class CurrencyJsonConverter : BaseJsonConverter {
        public CurrencyJsonConverter(Type type) : base(type) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Float)
                return TypeDescriptor.GetConverter(typeof(Currency)).ConvertFrom(reader.Value);

            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject item = JObject.Load(reader);
                if(item["Symbol"] != null && item["Value"] != null) {
                    var symbol = item["Symbol"].Value<string>();
                    var value = item["Value"].Value<double>();
                    return new Currency(value, symbol);
                }
            }

            return Currency.Zero;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            Currency cy = (Currency)value;
            if(cy == null) writer.WriteNull();
            else cy.ToJObject()?.WriteTo(writer);
        }
    }
}
