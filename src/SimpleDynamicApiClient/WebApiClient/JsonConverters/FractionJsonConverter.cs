using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Soneta.Langs;

namespace Soneta.Types.JsonConverters {
    [TranslateIgnore]
    public class FractionJsonConverter : BaseJsonConverter {
        public FractionJsonConverter(Type type) : base(type) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.String)
                return TypeDescriptor.GetConverter(typeof(Fraction)).ConvertFrom(reader.Value);

            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject item = JObject.Load(reader);
                if(item["Num"] != null && item["Den"] != null) {
                    var num = item["Num"].Value<int>();
                    var den = item["Den"].Value<int>();
                    return new Fraction(num, den);
                }
            }
            return Fraction.Zero;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var fraction = (Fraction)value;
            if (fraction == null) writer.WriteNull();
            else fraction.ToJObject()?.WriteTo(writer);
        }
    }
}
