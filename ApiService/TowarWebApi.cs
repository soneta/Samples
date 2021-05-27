using WebApiService;
using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiService.Interfaces;
using WebApiService.Models;
using Soneta.Handel;
using Soneta.Towary;
using Soneta.Magazyny;

[assembly: Service(typeof(ITowarAPI), typeof(TowarWebApi), Published = true)]

namespace WebApiService
{
    public class TowarWebApi : ITowarAPI
    {
        GetTowarInfo getTowar;
        HandelModule hm;
        TowaryModule tw;

        public TowarWebApi(Session session)
        {
            getTowar = new GetTowarInfo(session);
            hm = HandelModule.GetInstance(session);
            tw = TowaryModule.GetInstance(session);
        }

        public string NazwaTowaru(string EAN) => 
            getTowar.GetTowar(EAN).Nazwa;

        public TowarModel TowarInfo(string EAN, string NazwaMagazynu = "Firma")
        {
            var towar = getTowar.GetTowar(EAN);
            var defCeny = tw.DefinicjeCen.WgNazwy["Podstawowa"];
            var magazyn = hm.Magazyny.Magazyny.WgNazwa[NazwaMagazynu];
            var stanMagazynuWorker = new StanMagazynuWorker() { Magazyn = magazyn, Towar = towar };
            var stanNaMagazynie = stanMagazynuWorker.Stan;

            return new TowarModel()
            {
                Nazwa = towar.Nazwa,
                Stan = stanNaMagazynie.Value,
                Jednostka = towar.Jednostka.ToString(),
                Cena = towar.PobierzCenę(defCeny, null, null).Netto.Value,
                Kod = towar.Kod,
                EAN = towar.EAN
            };
        }
    }
}
