// Zdefiniowanie klasy pośredniej (przelotki) jest wymagane dla wszystkich tabel opisanych w business.xml 
// i generowanych do business.cs przez Soneta.Generator.

// W odróżnieniu od business.cs - ten plik możemy bezpiecznie edytować i rozbudowywać

namespace Soneta.Szkolenie

// W tym pliku definiujemy klasę pośrednią dla wiersza tabeli
{
    public class Lot : SzkolenieModule.LotRow
    {
        public override string ToString()   // przeciążenie metody ToString spowoduje przyjazne wyświetlanie zawartości kontrolki,
                                            // w której wybieramy obiekt, a nie typ posty
        {
            return Nazwa + " z " + LokalizacjaMiejscowosc;
        }
    }
}
