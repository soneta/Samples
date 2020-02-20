### Example 2
-----------------------------------------------------------------------------------------------------

Przykład pokazuje możliwość podpięcia dodatkowej zakładki użytkownika dla klasy *Kontrahent*.
Implementacja dodatkowego extendera pokazuje w jaki sposób można dodawać własne informacje
w postaci różnych danych (np. dodatkowe pola nie związane z logiką enova365). W przypadku 
zakładki wprowadzone zostało dodatkowe pole wyświetlające logo kontrahenta. 
Aby logo się pojawiło konieczne jest wstawienie kontrahentowi załącznika o typie obraz i ustawieniu go jako domyślny.
 
W wyniku zastosowania dodatku, powinna pojawić się dodatkowa zakładka na formularzu
kontrahenta, która oprócz danych standardowych zakładki ogólnej będzie posiadała również
logo kontrahenta.


W skład przykładu wchodzą trzy projekty:

* `Example2` - zawierający elementy logiki biznesowej
* `Example2.UI` - elementy interfejsu użytkownika
* `Example2.Tests` - testy


#### Zawartość przykładu:
> Przykład zawiera jedynie elementy związane z interfejsem użytkownika wiec całość znajduje się w `Example2.UI`.


* `Example2.UI`\Extender\KontrahentNewOgolneExtender.cs
   
   Przykładowa klasa implementująca property Logo, które zostało umieszczone na dodatkowej zakładce
* `Example2.UI`\UI\Kontrahent.KontrahentNewOgolne.pageform.xml

    Definicja dodatkowej zakładki wyświetlającej logo Kontrahenta