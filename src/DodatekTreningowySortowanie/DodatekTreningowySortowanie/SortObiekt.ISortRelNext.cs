using Soneta.Business;

namespace DodatekTreningowySortowanie
{
    public partial class SortObiekt : ISortRelNext
    {
        /// <summary>
        /// SqlResolving zamienia pole na bazodanowe tworząc Joina do tabeli SortRelObiekty.
        /// W ten sposób sortowania i filtrowania stają się realizowane przez SQL a nie kliencko
        /// </summary>
        [SqlResolving(ParentTableSubRow = "SortRelObiekty", RelationField = "SortObiekt")]
        public SortRelObiekt SortRelObiektNext => Relacje.GetNext();
    }
}
