using System.Linq;
using Soneta.Handel;
using Soneta.Tools;

namespace EnovaDB.Punktacja
{
    static class PrzeliczaniePunktowHelper
    {
        public static void Przelicz(HandelModule.PozycjaDokHandlowegoRow row)
        {
            var dokument = row.Dokument;
            // Jeżeli dokument nie jest w sesji nie kontynuujemy przeliczania
            if(!dokument.IsLive) return;

            // Odszukujemy kolekcję (SubTable) punktów związanych z dokumentem zmienianej pozycji
            var punktyWgDokumentu = PunktacjaModule.GetInstance(dokument).Punkty.WgDokument[dokument];

            // Jeżeli punktów jest więcej niż 1, nie dokunujemy automatycznego przeliczenia,
            // gdyż użytkownik wprowadził je ręcznie
            if (punktyWgDokumentu.Count > 1) return;

            // Sumujemy ilości na wszystkich pozycja dokumentu handlowego zaokrąglając w dół
            var sumaIlosciPozycji = dokument.Pozycje.Cast<PozycjaDokHandlowego>()
                .Sum(pozycja => (int)Math.Floor(pozycja.Ilosc.Value));

            // Pobieramy pierwszy punkt z kolekcji
            var punkt = punktyWgDokumentu.GetNext() as Punkt;

            // Jeżeli suma punktów z pozycji jest zerowa usuwamy punkt lub przerywamy przeliczenie gdy pusty
            if (sumaIlosciPozycji == 0)
            {
                if (punkt != null && punkt.IsLive)
                {
                    punkt.Delete();
                }
                return;
            }

            // Dodajemy punkt jeżeli jest pusty
            punkt = punkt ?? dokument.Session.AddRow(new Punkt(dokument));
            // Ustawiamy liczbę
            punkt.Liczba = sumaIlosciPozycji;
        }
    }
}