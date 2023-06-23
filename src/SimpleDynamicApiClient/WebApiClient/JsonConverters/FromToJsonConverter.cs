using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Soneta.Langs;

namespace Soneta.Types.JsonConverters {
    [TranslateIgnore]
    public class FromToJsonConverter : BaseJsonConverter {
        public FromToJsonConverter(Type type) : base(type) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Null)
                return FromTo.Empty;

            if (reader.TokenType == JsonToken.String)
                return TypeDescriptor.GetConverter(typeof(FromTo)).ConvertFrom(reader.Value);

            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject item = JObject.Load(reader);
                return ReadFromJObject(item);
            }
            return FromTo.Empty;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var ft = (FromTo)value;
            if (ft.IsNull) writer.WriteNull();
            else ft.ToJObject()?.WriteTo(writer);
        }

        internal static FromTo ReadFromJObject(JToken item) {
            if(item["From"] != null && item["To"] != null) {
                var from = item["From"].Value<DateTime>();
                var to = item["To"].Value<DateTime>();
                return new FromTo(from, to);
            }
            return FromTo.Empty;
        }
    }
}
