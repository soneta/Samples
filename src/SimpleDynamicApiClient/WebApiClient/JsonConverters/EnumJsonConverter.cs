using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Soneta.Langs;
using Soneta.Tools;
using WebApiClient;

namespace Soneta.Types.JsonConverters {
    [TranslateIgnore]
    public class EnumJsonConverter : StringEnumConverter {
        public EnumJsonConverter() : base() {}

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.Integer) {
                var values = new List<int>();
                foreach (var o in Enum.GetValues(objectType)) values.Add((int)o);
                if(!values.Any( o => o == (int)Convert.ChangeType(reader.Value, typeof(int))))
                    throw new Exception("Błąd konwersji wartości {0} do typu {1}".TranslateFormat(reader.Value, objectType.FullName));

                return Enum.ToObject(objectType, reader.Value);
            }

            if (reader.TokenType == JsonToken.String)
            {
                if (objectType == typeof(Wojewodztwo))
                    return Wojewodztwo._10;

                switch (reader.Value as string)
                {
                    case "None":
                        return EnumValue._0;
                    case "Single":
                        return EnumValue._1;
                    case "Multi":
                        return EnumValue._2;
                }   
            }

            if (reader.TokenType == JsonToken.Null)
            {
                return Enum.GetValues(objectType).GetValue(0);
            }

            return base.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (value == null) writer.WriteNull();
            else
            {
                var str = value.ToString();
                writer.WriteValue(str.StartsWith("_") ? str.TrimStart('_') : str);
            }
        }
    }
}
