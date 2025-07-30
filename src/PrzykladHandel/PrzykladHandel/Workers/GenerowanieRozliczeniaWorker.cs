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

                SubTable rozrachunki = kasaModule.RozrachunkiIdx.WgPodmiot[kontrahent, Date.MaxValue];
                foreach (RozrachunekIdx rozrachunek in rozrachunki)
                {
                    if (rozrachunek.Typ == TypRozrachunku.Wpłata && wpłata == null)
                        wpłata = (Wplata)rozrachunek.Dokument;
                    if (rozrachunek.Typ == TypRozrachunku.Należność && należność == null && !rozrachunek.Dokument.Bufor)
                        należność = (Naleznosc)rozrachunek.Dokument;
                    if (wpłata != null && należność != null)
                        break;
                }

                if (wpłata == null || należność == null)
                    throw new InvalidOperationException(string.Format("Nieznaleziona wpłata lub należność dla kontrahenta {0}", kontrahent.Nazwa));

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
