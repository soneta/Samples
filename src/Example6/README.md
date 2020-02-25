### Example 6
-----------------------------------------------------------------------------------------------------

Przykład pokazuje możliwość podpięcia workera dla typu Kontrahent z własnymi czynnościami. 
Zaimplementowane czynności wykorzystują różne typy zwracanych rezultatów mające wpływ na zachowanie interfejsu użytkownika. 
W przykładzie zastosowano rezultaty typu `MessageBoxInformation` i `QueryContextInformation`. 
Natomiast rezultat `NamedStream` pozwalający na zwrócenie pliku został zaprezentowany w przykładzie 4 .

W wyniku zastosowania dodatku, w menu czynności na liście kontrahentów powinna pojawić się dodatkowa 
sekcja o nazwie Soneta.Examples, która zawiera 4 różne akcje zaimplementowane w przykładzie.

* `Example6.UI`\Extender\ExampleWorker.cs

    Przykładowa klasa extender'a implementująca czynności z zastosowaniem różnych rezultatów.
