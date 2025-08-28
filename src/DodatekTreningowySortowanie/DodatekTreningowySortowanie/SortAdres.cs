using Soneta.Business;

namespace DodatekTreningowySortowanie
{
    [SqlResolving(ToStringFields = new[] { "Miejscowosc", "KodPocztowy", "Ulica", "NrDomu", "NrLokalu" })]
    public  class SortAdres : DodatekTreningowySortowanieModule.SortAdresRow
    {
        public override string ToString() =>$"{Miejscowosc} {KodPocztowy} {Ulica} {NrDomu} {NrLokalu}";
    }
}
