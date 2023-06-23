using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Soneta.Types.JsonConverters {
    public class DateJsonConverter : BaseJsonConverter {
        public DateJsonConverter(Type type) : base(type) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Date)
                return new Date((DateTime)reader.Value);

            if (reader.TokenType == JsonToken.String)
                return TypeDescriptor.GetConverter(typeof(Date)).ConvertFrom(reader.Value);

            return Date.Empty;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var dt = (Date)value;
            if (dt == null) writer.WriteNull();
            else writer.WriteValue(new DateTime(dt.Year, dt.Month, dt.Day));
        }
    }
}
