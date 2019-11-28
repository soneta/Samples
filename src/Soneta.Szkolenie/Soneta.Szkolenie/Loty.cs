// Zdefiniowanie klasy pośredniej (przelotki) jest wymagane dla wszystkich tabel opisanych w business.xml 
// i generowanych do business.cs przez Soneta.Generator.

// W odróżnieniu od business.cs - ten plik możemy bezpiecznie edytować i rozbudowywać

using Soneta.Business;

[assembly: NewRow(typeof(Soneta.Szkolenie.Lot))]  // rejestracja obiektu dla przycisku "Nowy" na tym ViewInfo

// W tym pliku definiujemy klasę pośrednią dla samej tabeli

namespace Soneta.Szkolenie
{
    public class Loty : SzkolenieModule.LotTable
    {
    }
}
