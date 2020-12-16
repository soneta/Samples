using PrzykladHandel;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Towary;
using System;
using System.Text;

// Jeden worker możemy zarejestrować dla różnych tabeli
[assembly: Worker(typeof(WyswietlanieTowarowWorker), typeof(DokHandlowe))]
[assembly: Worker(typeof(WyswietlanieTowarowWorker), typeof(Towary))]

namespace PrzykladHandel
{
    class WyswietlanieTowarowWorker
    {
        [Context]
        public Session Session { get; set; }

        [Action("Przykład Handel/Wyświetl towary", Mode = ActionMode.SingleSession | ActionMode.Progress)]
        public string WyswietlTowary()
        {
            // Przygotować zmienną do gromadzenia wyników
            StringBuilder sb = new StringBuilder();

            // Towary znajdują się w module Towary, ale kontrahenci
            // też się przydadzą
            TowaryModule towaryModule = TowaryModule.GetInstance(Session);
            CRMModule CRMModule = CRMModule.GetInstance(Session);

            // Następnie odczytujemy obiekt reprezentujący tabele 
            // wszystkich towarów znajdujących się w bazie danych
            Towary towary = towaryModule.Towary;

            // Jeżeli chcemy przeglądnąć wszystkie towary to
            // można wykorzystać enumerator w celu ich przeglądnięcia.
            // Przeglądanie będzie odbywać się wg nazwy towaru. 
            // Zostanie wyciągnięta mało ciekawa statystyka.
            int suma = 0;
            foreach (Towar towar in towary.WgNazwy)
            {
                // Tutaj można umieścić kod przetwarzający towar
                suma += towar.Nazwa.Length;
            }
            sb.AppendLine(string.Format(
                "Suma długości nazw wszystkich towarów: {0} znaków", suma));

            // Częściej zdarza się jednak, że chcemy wyszukać towary
            // spełniające pewne warunki, które najlepiej gdyby liczyły się
            // na serwerze SQL. W tym celu należy uzyskać obiekt widoku view.
            View view = towary.CreateView();

            // I założyć filtr, np tylko towary o cesze 'Kolor' 
            // równiej wartości 'Czerwony'. Warunek zakładany jest wówczas
            // gdy cecha 'Kolor' w ogóle istnieje.
            if (towary.FeatureDefinitions.Contains("Kolor"))
                view.Condition &= new FieldCondition.Equal("Features.Kolor", "Czerwony");

            // Można również zakładać warunki na pola będące referencjami
            // do innych obiektów.
            // Dołóżmy jeszcze warunek, dla towarów pochodzących od dostawcy
            // 'ABC', o ile taki kontrahent istnieje w bazie.
            Kontrahent kontrahentABC = CRMModule.Kontrahenci.WgKodu["ABC"];
            if (kontrahentABC != null)
                view.Condition &= new FieldCondition.Equal("Dostawca", kontrahentABC);

            // Teraz możemy przeglądnąć otrzymaną listę
            foreach (Towar towar in view)
            {
                sb.AppendLine(string.Format("Kod={0}, Nazwa={1}",
                    towar.Kod, towar.Nazwa));
            }

            // Zwracamy rezultat
            return sb.ToString();
        }
    }
}
