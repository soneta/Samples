using Soneta.Business;

namespace DodatekTreningowySortowanie
{
    public class SortRelObiekt : DodatekTreningowySortowanieModule.SortRelObiektRow
    {
        public SortRelObiekt(RowCreator creator) : base(creator)
        {
        }

        public SortRelObiekt(SortObiekt sortobiekt) : base(sortobiekt)
        {
        }
    }
}
