using PrzykladHandel;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Towary;
using System;
using System.Text;

// Jeden worker możemy zarejestrować dla różnych tabel.
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
            // Przygotowujemy zmienne.
            StringBuilder sb = new StringBuilder();
            TowaryModule towaryModule = TowaryModule.GetInstance(Session);
            CRMModule crmModule = CRMModule.GetInstance(Session);

            // Następnie odczytujemy obiekt reprezentujący tabele 
            // wszystkich towarów znajdujących się w bazie danych.
            Towary towary = towaryModule.Towary;

            // W celu przeglądnięcia wszystkich towarów możemy wykorzystać enumerator.
            // Przeglądanie będzie odbywać się wg nazwy towaru.
            int suma = 0;
            foreach (Towar towar in towary.WgNazwy)
            {
                // Tutaj można umieścić kod przetwarzający towar
                suma += towar.Nazwa.Length;
            }
            sb.AppendLine(string.Format("Suma długości nazw wszystkich towarów: {0} znaków", suma));

            // Częściej zdarza się jednak, że chcemy wyszukać towary
            // spełniające pewne warunki, które najlepiej gdyby liczyły się
            // na serwerze SQL. W tym celu należy uzyskać obiekt widoku View.
            View view = towary.CreateView();

            // Nakładamy filtr, np. tylko towary o cesze 'Kolor' 
            // równej wartości 'Czerwony'. Narunek zakładany jest wówczas,
            // gdy cecha 'Kolor' w ogóle istnieje.
            if (towary.FeatureDefinitions.Contains("Kolor"))
                view.Condition &= new FieldCondition.Equal("Features.Kolor", "Czerwony");

            // Można również nakładać warunki na pola będące referencjami
            // do innych obiektów.
            // Dołóżmy jeszcze warunek, dla towarów pochodzących od dostawcy
            // 'ABC', o ile taki kontrahent istnieje w bazie.
            Kontrahent kontrahentABC = crmModule.Kontrahenci.WgKodu["ABC"];
            if (kontrahentABC != null)
                view.Condition &= new FieldCondition.Equal("Dostawca", kontrahentABC);

            // Teraz możemy przeglądnąć otrzymaną listę.
            foreach (Towar towar in view)
            {
                sb.AppendLine(string.Format("Kod={0}, Nazwa={1}", towar.Kod, towar.Nazwa));
            }

            // Zwracamy rezultat.
            return sb.ToString();
        }
    }
}
