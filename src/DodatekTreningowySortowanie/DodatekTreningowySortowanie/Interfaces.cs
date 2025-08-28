using Soneta.Business;

namespace DodatekTreningowySortowanie
{
    public interface ISortAdresHost : IRow
    {
        SortAdres SortAdres { get; }
    }

    public interface ISortRelTop : IRow
    {
        SortRelObiekt SortRelObiektTop { get; }
    }

    public interface ISortRelNext : IRow
    {
        SortRelObiekt SortRelObiektNext { get; }
    }
}
