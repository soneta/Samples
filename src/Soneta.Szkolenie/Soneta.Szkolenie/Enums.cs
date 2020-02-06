// W celach porządkowych używane enumy zostają zebrane w oddzielnym folderze projektu biznesowego

using Soneta.Types;

namespace Soneta.Szkolenie
{
    // ten enum będzie używany zarówno na formatce edycji rezerwacji jak i w filtrach
    public enum CzyOplacone
    {
        // Caption można dodawać też w enumach: decyduje ono o tekście wyświetlanym np. w dropdownach
        [Caption("Opłacone")]
        Oplacone = 0,
        [Caption("Nieopłacone")]
        Nieoplacone = 1,
        [Caption("Razem")]
        Razem = 2
    }
}