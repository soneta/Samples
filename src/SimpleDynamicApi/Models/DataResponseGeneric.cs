using System;

namespace SimpleDynamicApi.Models
{
    public class DataResponseGeneric<T> 
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public T ResponseDto { get; set; }
    }

    public class DataResponseObjectDto : DataResponseGeneric<ObjectDto>
    {
        public bool Inherited { get; set; }
    }
    
    public class ObjectDto
    {
        public int Id { get; set; }
        public string Guid { get; set; }
    }

    public class ExtendedObjectDto : ObjectDto
    {
        public string Name { get; set; }
    }
}