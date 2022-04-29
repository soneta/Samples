# Operacje na dokumentach handlowych

**Przykład umieszczenia workera na liście dokumentów handlowych dla operacji na zaznaczonych na liście wierszach.**

## Rejestracja workera

    [assembly: Worker(typeof(OperacjeNaDokumentach.UI.OperacjeNaDokHandlowychWorker),
     typeof(DokHandlowe))]

Worker `OperacjeNaDokHandlowychWorker` jest zarejestrowany na na typie tabeli (typ `DokHandlowe`), co powoduje, że:
1. pojawi się wyłącznie na liście dokumentów handlowych (np. Faktury sprzedaży)
2. uruchomiony wykona się tylko raz - niezależnie od ilości zaznaczonych na liście wierszy

## Worker operujący na liście zaznaczonych wierszy

Główną, wywoływaną przyciskiem na toolbarze lub z menu _Czynności_, metodą workera jest metoda oznaczona atrybutem `Action`

    [Action("Wypisz numery dokumentów", Mode = ActionMode.SingleSession)]
    public object AkcjaNaDokumentach()
        {...}

W powyższym przykładzie kodu akcja workera pojawi się w menu _Czynności_ pod nazwą **_"Wypisz numery dokumentów"_** na liście dokumentów handlowych (np. dokumentów sprzedaży). Dodatkowo metoda będzie miała zapewnione utworzenie sesji do zapisu zmian oraz otwartej transakcji, która po zakończeniu działania metody workera w jakikolwiek inny sposób niż wyjątkiem, zostanie zamknięta przez `Commit`. Oznacza to, że kwestiami sesji i transakcji nie musimy sobie zaprzątać głowy.

W przypadku tak zarejestrowego workera, w celu wykonania operacji na zaznaczonych wierszach, powinniśmy oprzeć się na tablicy zaznaczonych wierszy, króra jest dostępna dla niego, jako element kontekstu o typie tablicy `TypWiersza[]` (w naszym przypadku `DokumentHandlowy[]`).  
Najprościej dostać się do takiego elementu definiując kontekstowe property:

    [Context]
    public DokumentHandlowy[] dokumenty { get; set; }

Następnie w prosty sposób możemy wykonywać wymagane operacje na każdym wierszu w pętli:

    foreach (var dokHandlowy in dokumenty)

W przykładowym workerze operacja na pojedynczym wierszu zostałą zamknięta w prywatnej metodzie workera `WykonajNaDokumencie`.

Na końcu metody **Action** - czyli naszej metody `AkcjaNaDokumentach` - zwracany jest string, co spowoduje wyświetlenie po zakończeniu pracy workera okna typu `MsgBox`, zawierającgo odpowiedni komunikat.  
W przykładzie pokazano także jak wpisywać informacje o przetwarzanych kolejno dokumentach do logu systemowego odpowiedniej kategorii, ponieważ składanie ich w string do wyświetlenia w `MsgBox` może spowodować przekroczenie jego pojemności i utratę informacji.

Linia `Trace.Write("SHOWOUTPUT", LogMessagesCategory);` spowoduje automatyczne otwarcie okna loga o podanej kategorii (tylko w wersji okienkowej).
