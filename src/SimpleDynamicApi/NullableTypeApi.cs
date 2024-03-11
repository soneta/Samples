using System;
using System.Collections.Generic;
using SimpleDynamicApi.Models;
using Soneta.Types;

namespace SimpleDynamicApi
{
    public class NullableTypeApi: INullableTypeApi
    {
        public NullableDto GetDataByNullableBool(bool? boolValue)
            => boolValue.HasValue ? new NullableDto { BoolValue = boolValue } : new NullableDto();

        public NullableDto GetDataByNullableByte(byte? byteValue)
            => byteValue.HasValue ? new NullableDto { ByteValue = byteValue } : new NullableDto();

        public NullableDto GetDataByNullableDateTime(DateTime? dateTimeValue)
            => dateTimeValue.HasValue ? new NullableDto { DateTimeValue = dateTimeValue } : new NullableDto();

        public NullableDto GetDataByNullableDecimal(decimal? decimalValue)
            => decimalValue.HasValue ? new NullableDto { DecimalValue = decimalValue } : new NullableDto();

        public NullableDto GetDataByNullableFloat(float? floatValue)
            => floatValue.HasValue ? new NullableDto { FloatValue = floatValue } : new NullableDto();

        public NullableDto GetDataByNullableInt(int? intValue)
             => intValue.HasValue ? new NullableDto { IntValue = intValue } : new NullableDto();

        public NullableDto GetDataByNullableObject(NullableDto nullableDto = null)
            => !nullableDto?.IntValue.HasValue ?? false ? nullableDto : new NullableDto();
    }
}
