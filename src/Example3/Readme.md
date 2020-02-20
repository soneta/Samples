### Example 3
-----------------------------------------------------------------------------------------------------

Przyk³ad pokazuje mo¿liwoœæ podpiêcia w³asnej zak³adki konfiguracyjnej dla potrzeb w³asnego dodatku.
W przyk³adzie zastosowano mechanizm tworzenia ustawieñ konfiguracji bezpoœrednio w kodzie programu,
bez stosowania pliku *config.xml*.

W wyniku zastosowania dodatku, w konfiguracji powinna pojawiæ siê dodatkowa zak³adka w sekcji o nazwie 
**Samples**.


W sk³ad przyk³adu wchodz¹ trzy projekty:

* `Example3` - zawieraj¹cy elementy logiki biznesowej
* `Example3.UI` - elementy interfejsu u¿ytkownika
* `Example3.Tests` - testy


#### Zawartoœæ przyk³adu:
> Przyk³ad zawiera jedynie elementy zwi¹zane z interfejsem u¿ytkownika wiec ca³oœæ znajduje siê w `Example3.UI`.



* `Example3.UI`\Extender\ZakladkaTowaryConfigExtender.cs

    Wsparcie dla ustawieñ

* `Example3.UI`\UI\Config.Zak³adkaTowary.pageform.xml

    Definicja zak³adki konfiguracyjnej

* `Example3.UI`\ViewInfo\Menu.cs

   Rejestracja listy dla View

* `Example3.UI`\ViewInfo\TowaryZakladkaViewInfo.cs

  Definicja ViewInfo z logik¹ steruj¹c¹ widocznoœci¹