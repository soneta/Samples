using System;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;

namespace Soneta.Types.JsonConverters {
    public class PercentJsonConverter : BaseJsonConverter {
        public PercentJsonConverter(Type type) : base(type) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Integer)
                return new Percent(Convert.ToDecimal(reader.Value));

            if (reader.TokenType == JsonToken.String ||reader.TokenType == JsonToken.Float)
                return TypeDescriptor.GetConverter(typeof(Percent)).ConvertFrom(null, CultureInfo.InvariantCulture, reader.Value);

            return Percent.Blank;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            var pc = (Percent)value;
            if (pc.IsNull) writer.WriteNull();
            writer.WriteValue(pc.ToString(CultureInfo.InvariantCulture));
        }
    }
}
