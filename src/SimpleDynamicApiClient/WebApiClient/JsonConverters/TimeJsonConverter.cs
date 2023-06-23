using System;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;

namespace Soneta.Types.JsonConverters {
    public class TimeJsonConverter : BaseJsonConverter {
        public TimeJsonConverter(Type type) : base(type) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer)
                return TypeDescriptor.GetConverter(typeof(Time)).ConvertFrom(reader.Value);
            return Time.Empty;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var tm = (Time)value;
            if (tm.IsNull) writer.WriteNull();
            writer.WriteValue(tm.ToString(null, CultureInfo.InvariantCulture));
        }
    }
}
