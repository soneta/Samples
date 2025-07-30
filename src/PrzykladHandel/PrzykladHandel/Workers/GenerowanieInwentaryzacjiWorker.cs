using PrzykladHandel;
using Soneta.Business;
using Soneta.Handel;
using Soneta.Magazyny;
using Soneta.Towary;
using Soneta.Types;
using System;

[assembly: Worker(typeof(GenerowanieInwentaryzacjiWorker), typeof(DokHandlowe))]

namespace PrzykladHandel
{
    class GenerowanieInwentaryzacjiWorker
    {
        [Context]
        public Session Session { get; set; }

        [Action("Przykład Handel/Generuj inwentaryzację", Mode = ActionMode.SingleSession | ActionMode.Progress)]
        public void GenerujInwentaryzacje()
        {
            HandelModule handelModule = HandelModule.GetInstance(Session);
            TowaryModule towaryModule = TowaryModule.GetInstance(Session);
            MagazynyModule magazynyModule = MagazynyModule.GetInstance(Session);

            using (ITransaction tran = Session.Logout(true))
            {
                DokumentHandlowy dokument = new DokumentHandlowy();

                // Nowy dokument należy związać z definicją dokumentu handlowego.
                // W tym przypadku wyszukujemy definicję z kolekcji standardowych definicji.
                dokument.Definicja = handelModule.DefDokHandlowych.Inwentaryzacja;

                dokument.Magazyn = magazynyModule.Magazyny.Firma;
                handelModule.DokHandlowe.AddRow(dokument);

                Towar towar = towaryModule.Towary.WgKodu["BIKINI"];
                if (towar != null)
                {
                    using (var tranPozycji = Session.Logout(true))
                    {
                        PozycjaDokHandlowego pozycja = new PozycjaDokHandlowego(dokument);
                        handelModule.PozycjeDokHan.AddRow(pozycja);
                        pozycja.Towar = towar;
                        pozycja.Ilosc = new Quantity(10, null);
                        pozycja.Cena = new DoubleCy(12.34);
                        tranPozycji.CommitUI();
                    }
                }
                tran.Commit();
            }

            // Dokument ten znajduje się w buforze, więc żeby stany magazynowe
            // mogły być zmodyfikowane, należy go zatwierdzić.
        }
    }
}
