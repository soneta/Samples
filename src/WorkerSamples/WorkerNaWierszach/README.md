# Operacje na towarach

**Przykład umieszczenia workera na liście i na formularzu towaru dla operacji odpowiednio na pojedynczym (otwartym) lub odzielnie na każdym z zaznaczonych na liście wierszu.**

## Rejestracja workera

    [assembly: Worker(typeof(OperacjeNaTowarach.UI.OperacjeNaTowarzeWorker), typeof(Towar))]

Worker `OperacjeNaTowarzeWorker` jest zarejestrowany na na typie wiersza (typ `Towar`), co powoduje że jego metoda oznaczona atrybutem `Action`:
1. pojawi się na liście towarów (np. Towary i usługi), ale także na formularzu towaru
2. na liście będzie widoczny tylko jeśli lista nie będzie pusta
3. uruchomiona z formularza wykona się oczywiście tylko raz - dla otwartego formularza
4. uruchomiona z listy wywoła się oddzielnie dla każdego zaznaczonego wiersza oddzielnie

Tego typu rejestracja jest przydatna wtedy, gdy operacja wykonywana na obiekcie wiersza nie jest zależna od innych zaznaczonych wierszy lub ich ilości.

Jeśli tego typu worker chcemy ukryć
* na liście - musimy zastosować w atrybucie **Action** wartość property **Mode** o wartości `ActionMode.OnlyForm`
* na formularzu - musimy zastosować w atrybucie **Action** wartość property **Mode** o wartości `ActionMode.OnlyTable`

## Worker operujący na pojedynczych wierszach zaznaczonych na liście

Metoda workera zainicjowana z listy jest wywoływana tyle razy, ile zostało zaznaczonych na liście wierszy, a każde jej wywołanie ma dostęp do aktualnie przetwarzanego wiersza w elemencie kontekstu o typie `TypWiersza` (w tym przypadku `Towar`). Dostep do niego najłatwiej uzyskać definiując property kontekstowe:

    [Context]
    public Towar towar { get; set; }

W kontekście jest także dostępny element typu `TypWiersza[]` (tu `Towar[]`), ale nie powinniśmy opierać na nim pętli przetwarzania wierszy, ponieważ w każdym wywołaniu metody (dla każdego zaznaczonego wiersza) tablica ta będzie taka sama: będzie zawierała wszystkie zaznaczone wiersze.
   
## Worker operujący na otwartym wierszu

Metoda workera zainicjowana z otwartego formularza wiersza wykona się tylko raz - oczywiście dla otwartego obiektu wiersza.  
Identycznie jak powyżej w kontekście ten wiersz będzie dostępny element typu `TypWiersza` (tu `Towar`), reprezentujący obiekt z podniesionego formularza:

    [Context]
    public Towar towar { get; set; }

> **UWAGA!**
> W kontekście będzie się także znajdował element typu `TypWiersza[]` (tu `Towar[]`), ale tym razem będzie zawierał wyłącznie jeden element: ten sam, który znajduje się w elemencie o typie `TypWiersza` (w tym przypadku `Towar`).  
> Nie ma znaczenia czy na liście pod otwartym formularzem zaznaczony jest jeden, czy też wiele wierszy.
>    `[Context]`  
>    `public Towar[] towary { get; set; }`


Do samej operacji na pojedynczym wierszu przeznaczona została prywatna metoda `WykonajNaTowarze`.

W przykładowym workerze przedstawiony został też sposób rozróżnienia czy metoda została wywołana dla pojedynczego wiersza (podniesionego formularza lub jedynego zaznaczonego na liście), co umożliwia sterowaniem zwracanego rezultatu:
dla jednego zaznaczonego lub dla formularza zostanie wyświetlone okno z nazwą towaru, a dla wielu zaznaczonych (na liście) poszczególne nazwy towarów będą wpiywane do loga, a po przetworzeniu ostatniego zaznaczonego zostanie wyświetlone okno z informacją.

    var jedenZaznaczony = towary.Length == 1;

    if (!jedenZaznaczony)
        return "Obsłużony towar: " + towar.Nazwa;
    else
    {
        Trace.WriteLine(towar.Nazwa, LogMessagesCategory);

        if (towar == towary[towary.Length - 1]) 
            return "Lista zaznaczonych towarów znajduje się w logu '" + LogMessagesCategory + "'.";
        else
            return null;
    }
