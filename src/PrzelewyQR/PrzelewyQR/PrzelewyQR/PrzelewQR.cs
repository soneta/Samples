using PrzelewyQR;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Core;
using Soneta.Kasa;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

[assembly: FolderView("Ewidencja Środków Pieniężnych/Skanowanie przelewów",
    Priority = 100000,
    Description = "Skaner",
    ObjectType = typeof(PrzelewQR),
    ObjectPage = "PrzelewQR.ogolne.pageform.xml",
    ReadOnlySession = false,
    ConfigSession = false,
    IconName = "kod_kreskowy"
)]

namespace PrzelewyQR
{
    public class PrzelewQR : INewBarCode 
    {   
        private Dictionary<string, string> _qr;
        private string[] _nazwyPozycji;
        private Context _context;

        public PrzelewQR()
        {
            _qr = new Dictionary<string, string>();

            //Pozycje spisane zgodnie z rekomendacją związku banków polskich
            //https://zbp.pl/getmedia/1d7fef90-d193-4a2d-a1c3-ffdf1b0e0649/2013-12-03_-_Rekomendacja_-_Standard_2D
            _nazwyPozycji = new string[]
            {
                "Identyfikator Odbiorcy",
                "Kod Kraju",
                "Numer Rachunku Odbiorcy",
                "Kwota w Groszach",
                "Nazwa Odbiorcy",
                "Tytuł Płatności",
                "Rezerwa 1",
                "Rezerwa 2",
                "Rezerwa 3"
            };
        }

        [Context]
        public Context Context
        {
            get { return _context; }
            set
            {
                _context = value;
                if (!Context.Contains(typeof(PrzelewQRParams)))
                    Context.Set(new PrzelewQRParams(Context));
            }
        }

        //W tym miejscu wykonujemy operacje odczytania danych z kodu, a następnie dodania nowej pozycji przelewowej
        public object Enter(Context cx, string code, double quantity)
        {
            PrzelewQRParams prms = (PrzelewQRParams)Context[typeof(PrzelewQRParams), false];

            string[] splitCode = code.Split('|');

            for (int i = 0; i < splitCode.Length; i++)
            {
                _qr[_nazwyPozycji[i]] = splitCode[i];
            }

            var numerRachunku = _qr["Kod Kraju"] + _qr["Numer Rachunku Odbiorcy"];

            var tytuł = _qr["Tytuł Płatności"];

            var kwotaDec = Convert.ToDecimal(_qr["Kwota w Groszach"].Insert(_qr["Kwota w Groszach"].Length - 2, ","));

            var kwota = new Currency(kwotaDec);

            var kasa = Context.Session.GetKasa();

            var rachunekBankowyPodmiotu = kasa.RachBankPodmiot.WgRachunekBank.Where(x => Regex.Replace(x.Rachunek.Numer.Pełny, @"\s+", "") == numerRachunku).FirstOrDefault();

            var podmiot = rachunekBankowyPodmiotu.Podmiot;

            using (ITransaction t = cx.Session.Logout(true))
            {
                var przelew = cx.Session.AddRow(new Przelew(prms.Rachunek));

                przelew.Podmiot = podmiot;
                przelew.Rachunek = rachunekBankowyPodmiotu;
                przelew.Kwota = kwota;
                przelew.Data = Date.Now;
                przelew.Tytulem1 = tytuł;

                t.Commit();
            }

            cx.Session.InvokeChanged();

            return FormAction.None;
        }

        //Miejsce konfigurowania viewinfo na potrzeby formularza
        public ViewInfo PrzelewyViewInfo
        {
            get
            {
                var vi = new ViewInfo();

                vi.CreateView += (sender, args) =>
                {
                   PrzelewQRParams prms = (PrzelewQRParams)Context[typeof(PrzelewQRParams), false];

                   var kasa = args.Context.Session.GetKasa();

                   var view = kasa.Przelewy.WgPodmiot.CreateView();

                   if (prms.Rachunek != null)
                      view.Condition &= new FieldCondition.Equal("EwidencjaSP", prms.Rachunek);
                            
                   if (prms.Podmiot != null)
                      view.Condition &= new FieldCondition.Equal("Podmiot", prms.Podmiot);

                   args.View = view;
                };

                return vi;
            }
        }

        //Klasa parametrów
        private class PrzelewQRParams : ContextBase
        {
            private readonly PodmiotKasowyLookupHelper _podmiotLookup;
            private RachunekBankowyFirmy _rachunek;
            private IPodmiotKasowy _podmiot;

            public PrzelewQRParams(Context context) : base(context)
            {
                Rachunek = (RachunekBankowyFirmy)KasaModule.GetInstance(Context).EwidencjeSP.RachunekBankowy;
                _podmiotLookup = new PodmiotKasowyLookupHelper();
            }

            public RachunekBankowyFirmy Rachunek
            {
                get => _rachunek;
                set
                {
                    _rachunek = value;
                    OnChanged(new EventArgs());
                }
            }

            public object GetListRachunek()
                        => KasaModule.GetInstance(Context).EwidencjeSP.GetModernLookup(EwidencjeSP.ModernViewArgs.Rachunki());


            public IPodmiotKasowy Podmiot
            {
                get => _podmiot;
                set
                {
                    _podmiot = value;
                    OnChanged(new EventArgs());
                }
            }

            public object GetListPodmiot()
                => _podmiotLookup.GetList(Context.Session, PodmiotKasowyLookupTyp.Kontrahenci);
        }
    }
}
