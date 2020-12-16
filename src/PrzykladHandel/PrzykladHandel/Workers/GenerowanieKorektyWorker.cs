using PrzykladHandel;
using Soneta.Business;
using Soneta.Handel;
using Soneta.Handel.RelacjeDokumentow.Api;
using System;
using System.Linq;

[assembly: Worker(typeof(GenerowanieKorektyWorker), typeof(DokumentHandlowy))]

namespace PrzykladHandel
{
    class GenerowanieKorektyWorker
    {
        [Context]
        public DokumentHandlowy Dokument { get; set; }

        [Context]
        public Session Session { get; set; }

        [Action("Przykład Handel/Generuj korektę", Mode = ActionMode.SingleSession | ActionMode.Progress | ActionMode.OnlyTable)]
        public void GenerujKorekte()
        {
            // Metoda tworzy dokument korygujący do dokumentu wyciągniętego z kontekstu
            using (ITransaction tran = Session.Logout(true))
            {
                // Pobranie serwisu relacji
                Session.GetService(out IRelacjeService relacjeService);

                // Utworzenie dokumentu korekty
                DokumentHandlowy korekta = relacjeService.NowaKorekta(new[] { Dokument }).First();

                // Zatwierdzanie dokumentu
                korekta.Stan = StanDokumentuHandlowego.Zatwierdzony;

                tran.Commit();
            }
        }

        public static bool IsVisibleGenerujKorekte(DokumentHandlowy dokument)
        {
            return !dokument.Korekta;
        }
    }
}
