using PrzykladHandel;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Ksiega;
using Soneta.Magazyny;
using Soneta.Towary;
using Soneta.Types;
using System;

[assembly: Worker(typeof(GenerowaniePrzyjeciaWorker), typeof(DokHandlowe))]

namespace PrzykladHandel
{
    class GenerowaniePrzyjeciaWorker
    {
        [Action("Przykład Handel/Generuj PZ 2", Mode = ActionMode.NoSession | ActionMode.Progress)]
        public void GenerujPrzyjecieMagazynowe(Context context)
        {
            // Metoda tworzy nowy dokument PZ2 wypełniając go przykładowymi pozycjami.
            // Rozpoczęcie tworzenia dokumentu polega na utworzeniu obiektu sesji (Session),
            // w którym będą odbywać się poszczególne operacje.
            // Pierwszy parametr określa, czy sesja jest tylko do odczytu danych.
            // Drugi parametr, określa czy sesja będzie modyfikować ustawienia
            // konfiguracyjne (tj. definicje dokumentów, jednostki, 
            // definicje cen, itp.). Standardowo obydwa parametry dajemy false.
            using (Session session = context.Login.CreateSession(false, false))
            {
                // Po utworzeniu sesji dobrze jest sobie przygotować odpowiednie 
                // zmienne reprezentujące poszczególne moduły programu w tej sesji.
                // Wystarczy przygotwać tylko te moduły, które będą nam potrzebne.
                HandelModule handelModule = HandelModule.GetInstance(session);
                TowaryModule towaryModule = TowaryModule.GetInstance(session);
                MagazynyModule magazynyModule = MagazynyModule.GetInstance(session);
                CRMModule crmModule = CRMModule.GetInstance(session);

                // Wszystkie operacje wykonujemy w transakcji sesji, którą należy
                // na początku otworzyć. W transakcji możemy wskazać czy będą 
                // robione zmiany w danych.
                using (ITransaction tran = session.Logout(true))
                {
                    // Następnie należy utworzyć nowy obiekt reprezentujący dokument
                    // handlowy (nagłówek dokumentu).
                    DokumentHandlowy dokument = new DokumentHandlowy();

                    // Nowy dokument należy również związać z definicją dokumentu handlowego.
                    // W tym przypadku wyszukujemy definicję według jej symbolu.
                    DefDokHandlowego definicja = handelModule.DefDokHandlowych.WgSymbolu["PZ 2"];
                    if (definicja == null)
                        throw new InvalidOperationException("Nieznaleziona definicja dokumentu PZ 2.");
                    dokument.Definicja = definicja;

                    // Dokument należy też przypisać do magazynu, do którego będzie
                    // przyjmowany towar. Poniżej przypisywany jest standardowy
                    // magazyn programu "Firma".
                    dokument.Magazyn = magazynyModule.Magazyny.Firma;

                    // Ale można wyszukać magazyn np. według symbolu
                    // dokument.Magazyn = magazynyModule.Magazyny.WgSymbol["MAG1"];

                    // Oraz dodajemy nowo utworzony dokument do aktualnej sesji.
                    handelModule.DokHandlowe.AddRow(dokument);

                    // Przyjęcie magazynowe PZ 2 wymaga również przypisania kontrahenta,
                    // od którego towar jest przyjmowany.
                    // Przykład prezentuje przypisanie dokumentowi kontrahenta o kodzie "ABC".
                    Kontrahent kontrahent = crmModule.Kontrahenci.WgKodu["ABC"];
                    if (kontrahent == null)
                        throw new InvalidOperationException("Nieznaleziony kontrahent o kodzie ABC.");
                    dokument.Kontrahent = kontrahent;

                    // Przykład poniżej prezentuje wyszukanie towaru wg kodu EAN "2000000000954". 
                    // Ponieważ w kartotece może znajdować się wiele towarów o tym 
                    // samym kodzie, wybrany zostanie pierwszy z nich.
                    Towar towar = towaryModule.Towary.WgEAN["2000000000954"].GetNext();
                    if (towar != null)
                    {
                        // Utworzyć nową transakcję dla każdej pozycji osobno.
                        using (var tranPozycji = session.Logout(true))
                        {
                            // Utworzyć nowy obiekt pozycji dokumentu handlowego, który
                            // zostanie dodany do sesji
                            PozycjaDokHandlowego pozycja = new PozycjaDokHandlowego(dokument);
                            handelModule.PozycjeDokHan.AddRow(pozycja);

                            // Przypisać towar do nowo utworzonej pozycji dokumentu, czyli
                            // wskazać, który towar ma być przyjęty do magazynu.
                            pozycja.Towar = towar;

                            // W pozycji dokumentu należy jeszcze wprowadzić ilość
                            // towaru przyjmowanego na magazyn. Ilość reprezentowana jest
                            // przez liczbę 10 będącą wartością ilości (pierwszy parametr) 
                            // oraz jednostkę opisującą tę ilość (drugi parametr). Jeżeli
                            // jednostka jest nullem, to przyjmowana jest jednostka z
                            // kartoteki towarowej.
                            // Poniżej znajduje się również wykomentowany przykład, w
                            // którym w sposób jawny wskazana jest jednostka w metrach.
                            pozycja.Ilosc = new Quantity(10, null);
                            // pozycja.Ilosc = new Quantity(10, "m");

                            // Pozycji dokumentu należy również przypisać cenę w jakiej
                            // będzie ona wprowadzana do magazynu (cena zakupu).
                            // Poniżej przypisywana jest cena w PLN. Dlatego nie jest
                            // podany drugi parametr określający walutę ceny.
                            pozycja.Cena = new DoubleCy(12.34);

                            // Poszczególnym pozycjom można przypisać również dodatkowe
                            // cechy, które zależne są od konfiguracji programu. Przykład
                            // pokazuje jak ustawić cechę z numerem beli. Wcześniej trzeba
                            // dodać definicję takiej cechy w konfiguracji.
                            //
                            // pozycja.Features["Numer beli"] = "123456";

                            // Na każdej pozycji dokumentu należy zatwierdzić osobną
                            // transakcję metodą CommitUI.
                            tranPozycji.CommitUI();
                        }
                    }

                    // Dokumentowi, podobnie jak pozycji dokumentu, również można
                    // przypisać dodatkowe cechy zależne od konfiguracji programu. 
                    // Przykład pokazuje jak ustawić cechę z lokalizacją.
                    //
                    // dokument.Features["Lokalizacja"] = "AB/12";

                    // Po dokonaniu wszystkich operacji na dokumencie można ten
                    // dokument wprowadzić (zatwierdzić), co powoduje zabezpieczenie 
                    // przed przypadkową edycją tego dokumentu oraz przeniesienie go
                    // do ewidencji dokumentów księgowych.
                    dokument.Stan = StanDokumentuHandlowego.Zatwierdzony;

                    // Na zakończenie należy zamknąć transakcję.
                    tran.Commit();
                }

                // Powyższe operacje były wykonywane na sesji, czyli w pamięci.
                // Teraz należy rezultat prac zapisać do bazy danych.
                session.Save();
            }

            // I to wszystko. Dokument PZ 2 znajduje się w bazie.
        }
    }
}
