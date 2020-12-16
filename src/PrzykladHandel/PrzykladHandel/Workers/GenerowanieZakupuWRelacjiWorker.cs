using PrzykladHandel;
using Soneta.Business;
using Soneta.Handel;
using Soneta.Handel.RelacjeDokumentow.Api;
using System;

[assembly: Worker(typeof(GenerowanieZakupuWRelacjiWorker), typeof(DokumentHandlowy))]

namespace PrzykladHandel
{
    class GenerowanieZakupuWRelacjiWorker
    {
        [Context]
        public DokumentHandlowy Dokument { get; set; }

        [Context]
        public Session Session { get; set; }

        [Action("Przykład Handel/Generuj ZK 2", Mode = ActionMode.SingleSession | ActionMode.Progress | ActionMode.OnlyTable)]
        public void GenerujZakupWRelacji()
        {
            // Do poprawnego działania tej funkcji konieczne jest zdefiniowanie
            // obiegu dokumentów polegającego na wprowadzaniu dokumentu PZ 2
            // a potem ręcznym generowaniu faktury zakupu ZK 2.
            //
            // Metoda demonstruje sposób generowania dokumentu podrzędnego 
            // relacji do innego dokumentu.

            // Operacje wykonujemy w transakcji sesji którą należy
            // na początku otworzyć. W transakcji możemy wskazać czy będą 
            // robione zmiany w danych.
            using (ITransaction tran = Session.Logout(true))
            {
                // Tworzymy dokument ZK 2 za pomocą relacji PZ 2 -> ZK 2 za pomocą API Relacji
                Session.GetService(out IRelacjeService service);
                service.NowyPodrzednyIndywidualny(new[] { Dokument }, "ZK 2");

                // Wszystkie operacje zostały poprawnie zakończone i zapewne 
                // chcemy zatwierdzić transakcję sesji
                tran.Commit();
            }

            // I to wszystko. Dokument ZK znajduje się w bazie.
        }

        public static bool IsVisibleGenerujZakupWRelacji(DokumentHandlowy dokument)
        {
            // Sprawdzamy czy definicja dokumentu to PZ 2
            return dokument.Definicja.Symbol == "PZ 2";
        }
    }
}
