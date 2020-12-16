using PrzykladHandel;
using Soneta.Business;
using Soneta.Kasa;
using Soneta.Types;
using System;

[assembly: Worker(typeof(GenerowanieRaportuKasowegoWorker), typeof(RaportyESP))]

namespace PrzykladHandel
{
    class GenerowanieRaportuKasowegoWorker
    {
        [Context]
        public Session Session { get; set; }

        [Action("Przykład Handel/Generuj raport kasowy", Mode = ActionMode.SingleSession | ActionMode.Progress)]
        public void GenerujRaportKasowy()
        {
            KasaModule kasa = KasaModule.GetInstance(Session);
            RaportESP raport = kasa.RaportyESP.WgKasa[kasa.EwidencjeSP.Kasa, Date.Today, 1];
            if (raport != null)
                throw new InvalidOperationException("Raport na dzień dzisiejszy został już założony");

            using (ITransaction t = Session.Logout(true))
            {
                raport = new RaportESP(kasa.EwidencjeSP.Kasa, new FromTo(Date.Today, Date.Today));
                kasa.RaportyESP.AddRow(raport);
                t.Commit();
            }
        }
    }
}
