using Soneta.Business;
using Soneta.Types;
using System;

namespace PrzykladHandel
{
    public class GenerowanieKontrahentaParams : ContextBase
    {
        public GenerowanieKontrahentaParams(Context context) : base(context)
        {
            Kod = "Kod" + new Random().Next(10000);
        }

        [Caption("Kod kontrahenta")]
        public string Kod { get; set; }
    }
}
