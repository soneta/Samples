using Soneta.Types;

namespace WebApiService.Models
{
    [BinSerializable]
    public class TowarModel
    {
        public string Kod { get; set; }
        public string EAN { get; set; }
        public string Nazwa { get; set; }
        public double Stan { get; set; }
        public string Jednostka { get; set; }
        public double Cena { get; set; }
    }
}
