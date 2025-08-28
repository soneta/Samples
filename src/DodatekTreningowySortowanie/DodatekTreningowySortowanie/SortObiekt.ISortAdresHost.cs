using Soneta.Business;

namespace DodatekTreningowySortowanie
{
    public partial class SortObiekt : ISortAdresHost
	{
        [SqlResolving(RelationField = "Host", ParentTableSubRow = "SortAdresy", PrefixSubRow = "SortAdres",
            WhereEqual = new object[] { "HostType", "SortObiekty" })]
        public SortAdres SortAdres =>
            DodatekTreningowySortowanieModule.GetInstance(this).SortAdresy.WgHost[this]?.SortAdres;
	}
}
