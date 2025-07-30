using PrzykladHandel;
using Soneta.Business;
using Soneta.CRM;
using System;

[assembly: Worker(typeof(GenerowanieKontrahentaWorker), typeof(Kontrahenci))]

namespace PrzykladHandel
{
    class GenerowanieKontrahentaWorker
    {
        [Context]
        public GenerowanieKontrahentaParams Params { get; set; }

        [Context]
        public Session Session { get; set; }

        [Action("Przykład Handel/Generuj kontrahenta", Mode = ActionMode.SingleSession | ActionMode.Progress)]
        public void GenerowanieKontrahenta(Context context)
        {
            // Do kontrahentów wystarczy uzyskać moduł CRM, w którym
            // znajduje się odpowiednia kolekcja
            CRMModule crmModule = CRMModule.GetInstance(Session);

            // Następnie otwieramy transakcję biznesową do edycji
            using (ITransaction tran = Session.Logout(true))
            {
                // Tworzymy nowy, pusty obiekt kontrahenta
                Kontrahent kontrahent = new Kontrahent();

                // Następnie dodajemy pusty obiekt kontrahenta do tabeli
                crmModule.Kontrahenci.AddRow(kontrahent);

                // Inicjujemy wymagane pole kod kontrahenta na podaną wartość.
                // Pole jest unikalne w bazie danych, wieć jeżeli kontranhent
                // o zadanym kodzie już istnieje w bazie danych, to podczas podstawiania
                // wartości do property zostanie wygenerowany wyjątek.
                kontrahent.Kod = Params.Kod;

                // Inicjujemy nazwę kontrahenta. To pole nie jest już unikalne.
                kontrahent.Nazwa = "Nazwa " + Params.Kod;

                // Inicjujemy pozostałe pola, które chcemy zainicjować.
                kontrahent.NIP = "123-45-67-890";
                kontrahent.Adres.Ulica = "Szara";
                kontrahent.Adres.NrDomu = "12";
                kontrahent.Adres.NrLokalu = "34";
                kontrahent.Adres.Miejscowosc = "Kraków";

                // Zatwierdzamy transakcję biznesową.
                tran.Commit();
            }
        }
    }
}
