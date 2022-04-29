## Operacje na kontrahentach

**Przykład umieszczenia workerów na liście i na formularzu kontrahenta oraz poprawnej ich konstrukcji dla operacji odpowiednio na pojedynczym (otwartym) lub zaznaczonych na liście wierszach.**

### Rejestracja workerów

**Nie jest możliwe takie zarejestrowanie pojedynczego workera, by był widoczny na liście i na formularzu, a jednocześnie wywołany z listy wykonał się tylko raz dla wszystkich zaznaczonych wierszy!**

Dlatego przykład definiuje dwa workery oparte na jednej klasie bazowej, ale rejestrowane oddzielnie: jeden na typie wiersza (`Kontrahent`), a drugi na typie tabeli (`Kontrahenci`).

* Sama klasa bazowa definiuje elementy wspólne dla workerów potomnych jak na przykład metodę, która realizuje samą podstawową operację do przeprowadzenia na pojedynczym wierszu (typu `Kontrahent`) - czyli metodę `WykonajNaKontrahencie`.

* `OperacjaNaKontrahencieWorker` jest workerem zarejestrowanym dla typu `Kontrahent` (jest to typ wiersza), co spowodowałoby pojawienie się workera zarówno na formatce kontrahenta, jak i na liście kontrahentów. Jednak atrybut **Action** metody `AkcjaNaKontrahencie` otrzymał w property **Mode** wartość `ActionMode.OnlyForm`, co powoduje, że na liście kontrahentów ta czynność nie będzie widoczna.

* `OperacjeNaKontrahentachWorker` jest workerem zarejestrowanym dla typu `Kontrahenci` (jest to typ tabeli), co powoduje pojawienie się tego workera wyłącznie na liście kontrahentów. Wartość `ActionMode.OnlyTable` w property **Mode** atrybutu **Action** metody `AkcjaNaKontrahentach` jest w tym przypadku nadmiarowa, ponieważ worker zarejestrowany dla typu tabeli i tak będzie dostępny jedynie na listach, a nie na formularzach.

### Worker operujący na otwartym wierszu

Worker jest zarejestrowany dla typu wiersza (`Kontrahent`), więc dostęp do wiersza właśnie podniesionego w formularzu obiektu odnajdziemy w kontekście, w elemencie typu `TypWiersza` (w tym przypadku `Kontrahent`), a najprościej uzyskać do niego dostęp definiując property kontekstowe:

    [Context]
    public Kontrahent kontrahent { get; set; }

następnie dla tego obiektu należy po prostu wywołać metodę z klasy bazowej, która przeprowadzi wymagane operacje na tym wierszu.

    WykonajNaKontrahencie(kontrahent);

Ponieważ worker ten operuje wywołany wyłącznie z formularza, **zawsze** zostanie wywołany tylko raz, więc może zwracać bezwarunkowo string komunikatu, który wyświetli się w oknie typu `MsgBox`.

### Worker operujący na zaznaczonych wierszach

Worker jest zarejestrowany dla typu tabeli (`Kontrahenci`), więc zostanie wywołany tylko raz, a dostęp do zaznaczonych na liście wierszy także odnajdziemy w kontekście, w elemencie typu `TypWiersza[]` (czyli `Kontrahenci[]`). Podobnie jak wcześniej dostęp do niej najprościej uzyskać definiując odpowiednie property kontekstowe.

    [Context]
    public Kontrahent[] kontrahenci { get; set; }

Przetwarzanie poszczególnych wierszy można następnie wykonać dzięki skorzystaniu z pętli `foreach`:

    foreach (var kontrahent in kontrahenci)
       WykonajNaKontrahencie(kontrahent);

Metoda workera kończy się zwrócenie stringa, co spowoduje wyświetlenie okna typu `MsgBox` z odpowiednią informacją.

W przykładzie pokazano ponadto sposób wpisywania danych na temat przetwarzanych wierszy do loga systemowego o odpowiedniej kategorii

    Trace.WriteLine(kontrahent.NazwaPierwszaLinia, LogMessagesCategory);

ponieważ składając te informacje w string i wyświetlając w końcowym oknie `MsgBox` łatwo moglibyśmy przekroczyć jego pojemność.

Linia `Trace.Write("SHOWOUTPUT", LogMessagesCategory);` powoduje (w wersji okienkowej) automatycznie otwarcie okna logu odpowiedniej kategorii.

### Klasa bazowa

Ta abstrakcyjna klasa zawierać może elementy wspólne dla obu potomnych klas workerów - w tym przypadku wewnętrzną metodę operującą na pojedynczym wierszu `WykonajNaKontrahencie`