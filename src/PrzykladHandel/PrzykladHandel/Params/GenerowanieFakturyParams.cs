using Soneta.Business;
using Soneta.Types;

namespace PrzykladHandel
{
    public class GenerowanieFakturyParams : ContextBase
    {
        public GenerowanieFakturyParams(Context context) : base(context) { }

        [Caption("Zapłacono gotówką")]
        public bool Gotowka { get; set; }

        [Caption("Na przelew")]
        public bool Przelew { get; set; }
    }
}
