using System;
using Newtonsoft.Json;

namespace Soneta.Types.JsonConverters {
    public class DateTimeJsonConverter : BaseJsonConverter {
        public DateTimeJsonConverter(Type type) : base(type) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Date)
                return (DateTime)reader.Value;
            if (reader.TokenType == JsonToken.String)
                return DateTime.Parse((string)reader.Value);
            return DateTime.MinValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var dt = (DateTime)value;
            if (dt == null) writer.WriteNull();
            else writer.WriteValue(dt);
        }
    }
}
