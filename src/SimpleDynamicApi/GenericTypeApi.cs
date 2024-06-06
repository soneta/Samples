using System;
using System.Collections.Generic;
using SimpleDynamicApi.Models;

namespace SimpleDynamicApi
{
    public class GenericTypeApi : IGenericTypeApi
    {
        public DataResponseObjectDto GetInheritedFromDataResponseGenericAsObjectDto(string kod)
            => new()
            {
                Id = 2,
                TimeStamp = DateTime.Now,
                Inherited = true,
                ResponseDto = new ObjectDto
                {
                    Id  = 101,
                    Guid = Guid.NewGuid().ToString()
                }
            };

        public DataResponseGeneric<ObjectDto> GetGenericDataResponseAsObjectDto(string kod)
            => new() {
                ResponseDto = new ObjectDto {
                    Id = 123,
                    Guid = Guid.NewGuid().ToString()
                }
            };

        public DataResponseGeneric<ExtendedObjectDto> GetGenericDataResponseAsExtendedObjectDto(string kod)
            => new() {
                ResponseDto = new ExtendedObjectDto {
                    Id = 123,
                    Guid = Guid.NewGuid().ToString(),
                    Name = nameof(ExtendedObjectDto)
                }
            };

        public DataResponseGeneric<List<ExtendedObjectDto>> GetGenericDataResponseAsListExtendedObjectDto(string kod)
            => new() {
                ResponseDto = new List<ExtendedObjectDto> {
                    new() {
                        Id = 1,
                        Guid = Guid.NewGuid().ToString(),
                        Name = $"{nameof(ExtendedObjectDto)}1"
                    }
                }
            };

        public DataResponseGeneric<ObjectDto> PostGenericDataResponseAsObjectDto(DataResponseGeneric<ObjectDto> args)
            => args;

    }
}