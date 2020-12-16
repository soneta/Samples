using PrzykladHandel;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using System.Text;

// Jeden worker możemy zarejestrować dla różnych tabeli
[assembly: Worker(typeof(WyswietlanieKontrahentowWorker), typeof(DokHandlowe))]
[assembly: Worker(typeof(WyswietlanieKontrahentowWorker), typeof(Kontrahenci))]

namespace PrzykladHandel
{
    class WyswietlanieKontrahentowWorker
    {
        [Action("Przykład Handel/Wyświetl kontrahentów", Mode = ActionMode.NoSession | ActionMode.Progress)]
        public string WyswietlKontrahentow(Context context)
        {
            // Przygotować zmienną do gromadzenia wyników
            StringBuilder sb = new StringBuilder();

            // Do przeglądania obiektów w bazie danych wystarczy otworzyć sesje
            // w trybie read-only - pierwszy parametr true
            using (Session session = context.Login.CreateSession(true, false))
            {
                // Kontrahenci znajdują się w module CRM
                CRMModule module = CRMModule.GetInstance(session);

                // Następnie odczytujemy obiekt reprezentujący tabele 
                // wszystkich kontrahentów znajdujących się w bazie danych
                Kontrahenci kontrahenci = module.Kontrahenci;

                // Jeżeli chcemy przeglądnąć wszystkich kontrahentów to
                // można wykorzystać enumerator w celu ich przeglądnięcia.
                // Przeglądanie odbywa się wg primary key.
                // Zostanie wyciągnięta mało ciekawa statystyka.
                int suma = 0;
                foreach (Kontrahent kontrahent in kontrahenci)
                {
                    // Tutaj można umieścić kod przetwarzający kontrahenta
                    suma += kontrahent.Kod.Length;
                }
                sb.AppendLine(string.Format(
                    "Suma długości kodów wszystkich kontrahentów: {0} znaków", suma));

                // Częściej zdarza się jednak, że chcemy wyszukać kontrahentów 
                // spełniających pewne warunki, które najlepiej gdyby liczyły się
                // na serwerze SQL. W tym celu należy uzyskać obiekt widoku view.
                View view = kontrahenci.CreateView();

                // I założyć filtr, np tylko kontrahentów, zawierających literkę 
                // 's' w nazwie i o kodzie nie !INCYDENTALNY.
                // Operatory
                // & to jest AND
                // | to jest OR
                // ! to jest NOT
                view.Condition &= new FieldCondition.Like("Nazwa", "*s*")
                    & !new FieldCondition.Equal("Kod", "!INCYDENTALNY");

                // Teraz możemy przeglądnąć otrzymaną listę
                foreach (Kontrahent kontrahent in view)
                {
                    sb.AppendLine(string.Format("Kod={0}, Nazwa={1}",
                        kontrahent.Kod, kontrahent.Nazwa));
                }

                // Wzracamy rezultat
                return sb.ToString();

                // Oczywiście Session.Save na końcu nie jest potrzebny, bo nie 
                // zmieniliśmy nic w bazie
            }
        }
    }
}
