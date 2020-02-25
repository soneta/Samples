### Example 3
-----------------------------------------------------------------------------------------------------

Przyk�ad pokazuje mo�liwo�� podpi�cia w�asnej zak�adki konfiguracyjnej dla potrzeb w�asnego dodatku.
W przyk�adzie zastosowano mechanizm tworzenia ustawie� konfiguracji bezpo�rednio w kodzie programu,
bez stosowania pliku *config.xml*.

W wyniku zastosowania dodatku, w konfiguracji powinna pojawi� si� dodatkowa zak�adka w sekcji o nazwie 
**Samples**.


W sk�ad przyk�adu wchodz� trzy projekty:

* `Example3` - zawieraj�cy elementy logiki biznesowej
* `Example3.UI` - elementy interfejsu u�ytkownika
* `Example3.Tests` - testy


#### Zawarto�� przyk�adu:
> Przyk�ad zawiera jedynie elementy zwi�zane z interfejsem u�ytkownika wiec ca�o�� znajduje si� w `Example3.UI`.



* `Example3.UI`\Extender\ZakladkaTowaryConfigExtender.cs

    Wsparcie dla ustawie�

* `Example3.UI`\UI\Config.Zak�adkaTowary.pageform.xml

    Definicja zak�adki konfiguracyjnej

* `Example3.UI`\ViewInfo\Menu.cs

   Rejestracja listy dla View

* `Example3.UI`\ViewInfo\TowaryZakladkaViewInfo.cs

  Definicja ViewInfo z logik� steruj�c� widoczno�ci�