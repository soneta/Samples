using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Soneta.Langs;

namespace Soneta.Types.JsonConverters {
    [TranslateIgnore]
    public class IntervalJsonConverter : BaseJsonConverter {
        public IntervalJsonConverter(Type type) : base(type) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.String)
                return TypeDescriptor.GetConverter(typeof(Interval)).ConvertFrom(reader.Value);

            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject item = JObject.Load(reader);
                if(item["Start"] != null && item["End"] != null) {
                    var start = item["Start"].Value<DateTime>();
                    var end = item["End"].Value<DateTime>();
                    return new Interval(start, end);
                }
            }
            return Interval.Empty;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            Interval iv = (Interval)value;
            if (iv.IsNull) writer.WriteNull();
            else iv.ToJObject()?.WriteTo(writer);
        }
    }
}
