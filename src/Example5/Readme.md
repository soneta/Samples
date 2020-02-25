### Example 5
-----------------------------------------------------------------------------------------------------

Przykład pokazuje możliwość definiowania pageform dla parametrów zarejestrowanego workera.

W wyniku zastosowania dodatku, w menu czynności na liście towarów powinna pojawić się dodatkowa 
sekcja o nazwie *Soneta.Examples*, która zawiera akcje o nazwie *"Zmiana postfix/prefix"* zaimplementowaną 
w przykładzie.

* `Example5`\Extender\ZmianaNazwTowarowWorker.cs

    Przykładowa klasa worker'a implementująca czynności w opraciu o klasę parametrów z którą powiązany jest zarejestrowany page.
* `Example5.UI`\UI\ZmianaNazwTowarowParams.Ogolne.pageform.xml

    Pageform zarejestrowany dla parametrów metody worker'a
