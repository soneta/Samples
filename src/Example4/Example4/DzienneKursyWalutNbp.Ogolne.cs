using System.Collections.Generic;
using System.Linq;
using Soneta.Business;
using Samples.Example4;
using Samples.Example4.Extender;

namespace Samples.Example4
{

    public partial class DzienneKursyWalutNbp {
        [Context(Required = true)]
        public Context Context { get; set; }

        [Context(Required = true)]
        public Session Session { get; set; }

        #region Property dla formularza

        private SortedDictionary<string,KursWalutyNbp> _kursyWalut;
        public IEnumerable<KursWalutyNbp> KursyWalut {
            get {
                if (_kursyWalut != null) 
                    return _kursyWalut.Values.ToArray();

                _kursyWalut = new SortedDictionary<string, KursWalutyNbp> {
                    {
                        "USD", new KursWalutyNbp {
                            Kod = "USD",
                            Nazwa = "Dolar amerykański",
                            Przelicznik = 1,
                            KursSredni = 3.0589f
                        }
                    },
                    {
                        "CHF", new KursWalutyNbp {
                            Kod = "CHF",
                            Nazwa = "Frank szwajcarski",
                            Przelicznik = 1,
                            KursSredni = 3.4018f
                        }
                    }
                };
                return _kursyWalut.Values.ToArray();
            }
        }

        #endregion Property dla formularza
    }

}
