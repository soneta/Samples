using PrzykladHandel;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using System.Text;

// Jeden worker możemy zarejestrować dla różnych tabel.
[assembly: Worker(typeof(WyswietlanieKontrahentowWorker), typeof(DokHandlowe))]
[assembly: Worker(typeof(WyswietlanieKontrahentowWorker), typeof(Kontrahenci))]

namespace PrzykladHandel
{
    class WyswietlanieKontrahentowWorker
    {
        [Action("Przykład Handel/Wyświetl kontrahentów", Mode = ActionMode.NoSession | ActionMode.Progress)]
        public string WyswietlKontrahentow(Context context)
        {
            // Przygotować zmienną do gromadzenia wyników.
            StringBuilder sb = new StringBuilder();

            // Do przeglądania obiektów w bazie danych wystarczy otworzyć sesję
            // w trybie read-only - pierwszy parametr true.
            using (Session session = context.Login.CreateSession(true, false))
            {
                // Kontrahenci znajdują się w module CRM.
                CRMModule crmModule = CRMModule.GetInstance(session);

                // Odczytujemy obiekt reprezentujący tabelę
                // wszystkich kontrahentów znajdujących się w bazie danych.
                Kontrahenci kontrahenci = crmModule.Kontrahenci;

                // W celu przeglądnięcia wszystkich kontrahentów możemy wykorzystać enumerator.
                // Przeglądanie odbywa się wg primary key.
                int suma = 0;
                foreach (Kontrahent kontrahent in kontrahenci)
                {
                    // Tutaj można umieścić kod przetwarzający kontrahenta.
                    suma += kontrahent.Kod.Length;
                }
                sb.AppendLine(string.Format("Suma długości kodów wszystkich kontrahentów: {0} znaków", suma));

                // Częściej zdarza się jednak, że chcemy wyszukać kontrahentów 
                // spełniających pewne warunki, które najlepiej gdyby liczyły się
                // na serwerze SQL. W tym celu należy uzyskać obiekt widoku View.
                View view = kontrahenci.CreateView();

                // Zakładamy filtr, np. tylko kontrahentów, zawierających literkę 
                // 's' w nazwie i o kodzie innym niż !INCYDENTALNY.
                // Operatory
                // & to jest AND
                // | to jest OR
                // ! to jest NOT
                view.Condition &= new FieldCondition.Like("Nazwa", "*s*")
                    & !new FieldCondition.Equal("Kod", "!INCYDENTALNY");

                // Teraz możemy przeglądnąć otrzymaną listę.
                foreach (Kontrahent kontrahent in view)
                {
                    sb.AppendLine(string.Format("Kod={0}, Nazwa={1}", kontrahent.Kod, kontrahent.Nazwa));
                }

                // Wzracamy rezultat.
                return sb.ToString();

                // Oczywiście Session.Save na końcu nie jest potrzebny, bo nie 
                // zmieniliśmy nic w bazie.
            }
        }
    }
}
