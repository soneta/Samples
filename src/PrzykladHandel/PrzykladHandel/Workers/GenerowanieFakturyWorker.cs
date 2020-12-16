using PrzykladHandel;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Kasa;
using Soneta.Magazyny;
using Soneta.Towary;
using Soneta.Types;
using System;

[assembly: Worker(typeof(GenerowanieFakturyWorker), typeof(DokHandlowe))]

namespace PrzykladHandel
{
    class GenerowanieFakturyWorker
    {
        [Context]
        public GenerowanieFakturyParams Params { get; set; }

        [Context]
        public Session Session { get; set; }

        [Action("Przykład Handel/Generuj fakturę", Mode = ActionMode.SingleSession | ActionMode.Progress)]
        public void GenerujFakture()
        {
            HandelModule handelModule = HandelModule.GetInstance(Session);
            TowaryModule towaryModule = TowaryModule.GetInstance(Session);
            MagazynyModule magazynyModue = MagazynyModule.GetInstance(Session);
            CRMModule CRMModule = CRMModule.GetInstance(Session);
            KasaModule kasaModule = KasaModule.GetInstance(Session);

            using (ITransaction tran = Session.Logout(true))
            {
                DokumentHandlowy dokument = new DokumentHandlowy();
                dokument.Definicja = handelModule.DefDokHandlowych.FakturaSprzedaży;
                dokument.Magazyn = magazynyModue.Magazyny.Firma;

                handelModule.DokHandlowe.AddRow(dokument);

                Kontrahent kontrahent = CRMModule.Kontrahenci.WgKodu["ABC"];
                if (kontrahent == null)
                    throw new InvalidOperationException("Nieznaleziony kontrahent o kodzie ABC.");
                dokument.Kontrahent = kontrahent;

                Towar towar = towaryModule.Towary.WgKodu["MONTAZ"];
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

                // Modyfikacja płatności
                // Standradowo dokument ma wygenerowaną przez system jedną płatność. Zmienimy w niej
                // sposób zapłaty, domyślną ewidencję oraz termin płatności.
                // Jeżeli chcemy mieć więcej niż jedną płatność, to zmniejszamy kwotę w instniejącej
                // i dodajemy kolejne płatności aż do zrównoważenia kwoty płatności i dokumentu.
                // Dodatkowo, jeżeli generujemy płatność gotówkową, to dodamy do niej informację
                // o zapłacieniu i odpowiedni wpis na raport kasowy (musi być założony i niezatwierdzony).

                // Wymuszamy przeliczenie zdarzeń. W przeciwnym razie sumy mogą być nieaktualne.
                // Normalnie robi to za nas UI.
                Session.Events.Invoke();

                Naleznosc gotowka = null;
                Naleznosc przelew = null;
                // Pobieramy isntniejacą płatność. System zrobił na pewno jedną (o ile wartość
                // dokumentu jest różna od zera).
                // Możemy też płatność usunąć i odtworzyć dokładnie tak samo jak to jest 
                // robione z dodatkową płatnością.
                Naleznosc platnosc = (Naleznosc)dokument.Platnosci.GetNext();
                if (Params.Gotowka && Params.Przelew)
                {
                    Currency kwota = platnosc.Kwota;
                    platnosc.Kwota = platnosc.Kwota / 2;
                    gotowka = platnosc;
                    // Tworzymy nowy obiekt należności
                    przelew = new Naleznosc(dokument);
                    // Dodajemy go do tabeli Platnosci
                    kasaModule.Platnosci.AddRow(przelew);
                    // Ustawiamy kwotę
                    przelew.Kwota = kwota - gotowka.Kwota;
                }
                else if (Params.Gotowka)
                    gotowka = platnosc;
                else if (Params.Przelew)
                    przelew = platnosc;

                // Mamy już potrzebne płatności. Teraz musimy im zmodyfikować sposób zapłaty,
                // ewidencję SP oraz termin płatności.

                // Najpier przelew
                if (przelew != null)
                {
                    // Wyszukujemy sposób zapłaty. Na szczęście dla gotówki mamy standardowy.
                    // Możemy też szukać wg nazwy lub Guid.
                    przelew.SposobZaplaty = kasaModule.SposobyZaplaty.Przelew;

                    // Szukamy ewidencję SP
                    przelew.EwidencjaSP = kasaModule.EwidencjeSP.WgNazwy["Firmowy rachunek bankowy"];

                    // I termin płatności
                    przelew.TerminDni = 21;
                }

                if (gotowka != null)
                {
                    // Wyszukujemy sposób zapłaty. Na szczęście dla gotówki mamy standardowy.
                    // Możemy też szukać wg nazwy lub Guid.
                    gotowka.SposobZaplaty = kasaModule.SposobyZaplaty.Gotówka;

                    // Szukamy ewidencję SP
                    gotowka.EwidencjaSP = kasaModule.EwidencjeSP.WgNazwy["Kasa gotówkowa"];

                    // I termin płatności
                    gotowka.TerminDni = 0;

                    // Pozostaje to teraz zapłacić.
                    // Szukamy raportu kasowego. Musi być istnieć i być otwarty lub 
                    // nie istnieć i mieć flagę automatyczny.
                    // Oczywiście ewidencja SP musi być typu kasa.
                    RaportESP raport = ((Kasa)gotowka.EwidencjaSP).NowyRaport(dokument, dokument.Data);
                    // Tworzymy nowy dokument wpłaty
                    Wplata wpłata = new Wplata(dokument, raport);
                    // Dodajemy go do tabeli
                    kasaModule.Zaplaty.AddRow(wpłata);
                    // Ustawiamy podmiot (taki jak w należności)
                    wpłata.Podmiot = dokument.Kontrahent;
                    // ... sposób zapłaty
                    wpłata.SposobZaplaty = gotowka.SposobZaplaty;
                    // ... opis wystawrczy zainicjować, zostanie przeliczony podczas zapisu
                    wpłata.Opis = "?";
                    // Oraz oczywiście kwotę
                    wpłata.Kwota = gotowka.Kwota;

                    // Wpłata z należnością zostaną rozliczone podczas zatwierdzania dokumentu
                }

                dokument.Stan = StanDokumentuHandlowego.Zatwierdzony;

                tran.Commit();
            }
        }
    }
}
