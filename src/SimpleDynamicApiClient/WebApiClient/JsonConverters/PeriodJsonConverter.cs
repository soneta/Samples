using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Soneta.Types.JsonConverters {
    public class PeriodJsonConverter : BaseJsonConverter {
        public PeriodJsonConverter(Type type) : base(type) { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.StartArray)
            {
                var periods = new Periods();
                JArray.Load(reader)?.Select(item => FromToJsonConverter.ReadFromJObject(item))?.ToList()?.ForEach(ft => {
                    periods = periods.Add(ft);
                });
                return periods;
            }
            return Periods.Empty;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            Periods periods = (Periods)value;
            if (periods == null) {
                writer.WriteNull();
                return;
            }

            writer.WriteStartArray();
            var enumerator = periods.GetEnumerator();
            while (enumerator.MoveNext()) {
                var ft = (FromTo)enumerator.Current;
                ft.ToJObject().WriteTo(writer);
            }
            writer.WriteEndArray();
        }
    }
}
