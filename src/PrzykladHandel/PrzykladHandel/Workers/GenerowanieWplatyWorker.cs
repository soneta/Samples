using PrzykladHandel;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Kasa;
using Soneta.Types;
using System;

[assembly: Worker(typeof(GenerowanieWplatyWorker), typeof(RaportyESP))]

namespace PrzykladHandel
{
    class GenerowanieWplatyWorker
    {
        [Context]
        public RaportESP[] Raporty { get; set; }

        [Context]
        public Session Session { get; set; }

        [Action("Przykład Handel/Generuj wpłatę do kasy gotówkowej", 
            Mode = ActionMode.SingleSession | ActionMode.Progress | ActionMode.ConfirmFinished)]
        public void GenerujWplate()
        {
            KasaModule kasaModule = KasaModule.GetInstance(Session);
            CRMModule CRMModule = CRMModule.GetInstance(Session);

            Kontrahent kontrahent = CRMModule.Kontrahenci.WgKodu.GetNext();
            if (kontrahent == null)
                throw new InvalidOperationException("Nieznaleziono żadnego kontrahenta.");

            using (ITransaction t = Session.Logout(true))
            {
                foreach (var raport in Raporty)
                {
                    DokumentWplata wpłata = new DokumentWplata(raport);
                    kasaModule.DokumentyKasowe.AddRow(wpłata);
                    if (!wpłata.IsReadOnlyData())
                        wpłata.Data = Date.Today;
                    wpłata.Zaplata.Podmiot = kontrahent;
                    wpłata.Zaplata.Kwota = new Currency(100.0m, "PLN");
                    wpłata.Zaplata.Opis = "Wpłata do kasy";
                    wpłata.Zatwierdzony = true;
                }
                t.Commit();
            }
        }
    }
}
