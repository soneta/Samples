using System;
using Soneta.Types;

namespace SimpleDynamicApi.Models
{
    [BinSerializable]
    public class NullableDto {
        public bool? BoolValue { get; set; }
        public byte? ByteValue { get; set; }
        public DateTime? DateTimeValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public float? FloatValue{ get; set; }
        public int? IntValue{ get; set; }
        public int?[] IntArrayValue{ get; set; }
    }
}