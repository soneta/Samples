using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Soneta.Langs;

namespace Soneta.Types.JsonConverters {
    [TranslateIgnore]
    public class DoubleCyJsonConverter : BaseJsonConverter {
        public DoubleCyJsonConverter(Type type) : base(type) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {

            if (reader.TokenType == JsonToken.String ||
                reader.TokenType == JsonToken.Float ||
                reader.TokenType == JsonToken.Integer)
                return TypeDescriptor.GetConverter(typeof(DoubleCy)).ConvertFrom(reader.Value);

            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject item = JObject.Load(reader);
                if(item["Symbol"] != null && item["Value"] != null) {
                    var symbol = item["Symbol"].Value<string>();
                    var value = item["Value"].Value<double>();
                    return new DoubleCy(value, symbol);
                }
            }
            return DoubleCy.Zero;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            DoubleCy cy = (DoubleCy)value;
            if (cy == null) writer.WriteNull();
            else cy.ToJObject()?.WriteTo(writer);
        }
    }
}
