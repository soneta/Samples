using PrzykladHandel;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Kasa;
using Soneta.Types;
using System;

[assembly: Worker(typeof(GenerowanieRozliczeniaWorker), typeof(Kontrahenci))]

namespace PrzykladHandel
{
    class GenerowanieRozliczeniaWorker
    {
        [Context]
        public Kontrahent[] Kontrahenci { get; set; }

        [Context]
        public Session Session { get; set; }

        [Action("Przykład Handel/Generuj rozliczenie SP",
            Mode = ActionMode.SingleSession | ActionMode.Progress | ActionMode.ConfirmFinished)]
        public void GenerujRozliczenie()
        {
            KasaModule kasaModule = KasaModule.GetInstance(Session);

            foreach (var kontrahent in Kontrahenci)
            {
                Wplata wpłata = null;
                Naleznosc należność = null;

                SubTable st = kasaModule.RozrachunkiIdx.WgPodmiot[kontrahent, Date.MaxValue];
                foreach (RozrachunekIdx idx in st)
                {
                    if (idx.Typ == TypRozrachunku.Wpłata && wpłata == null)
                        wpłata = (Wplata)idx.Dokument;
                    if (idx.Typ == TypRozrachunku.Należność && należność == null && !idx.Dokument.Bufor)
                        należność = (Naleznosc)idx.Dokument;
                    if (wpłata != null && należność != null)
                        break;
                }

                if (wpłata == null || należność == null)
                    throw new InvalidOperationException(string.Format("Nieznalezione wpłata lub należność dla kontrahenta {0}", kontrahent.Nazwa));

                using (ITransaction t = Session.Logout(true))
                {
                    RozliczenieSP rozliczenie = new RozliczenieSP(należność, wpłata);
                    kasaModule.RozliczeniaSP.AddRow(rozliczenie);
                    t.Commit();
                }
            }
        }
    }
}
