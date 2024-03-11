using System.Collections.Generic;
using Soneta.Types;

namespace SimpleDynamicApi.Models
{
    [BinSerializable]
    public class RefDocument
    {
        public string Symbol { get; set; } = "Document1";
        public RefDocument Parent { get; set; } = null;
        public List<RefDocument> Parents { get; set; } = new();
    }
}