namespace DodatekTreningowySortowanie
{
    public partial class SortObiekt : ISortRelTop
    {
        /// <summary>
        /// Pole tak zdefiniowane zachowuje się czysto kliencko.
        /// Posortowanie listy po tym polu wymaga de facto pobrania danych dla wszystkich elementów i sortowania klienckiego
        /// </summary>
        public SortRelObiekt SortRelObiektTop =>   Relacje.GetFirst();
    }
}
