using Soneta.Business;

namespace DodatekTreningowySortowanie
{
    public class SortAdresExt : DodatekTreningowySortowanieModule.SortAdresExtRow
    {
        public SortAdresExt(RowCreator creator) : base(creator)
        {
        }

        public SortAdresExt(ISortAdresHost host) : base(host)
        {
        }
    }
}
