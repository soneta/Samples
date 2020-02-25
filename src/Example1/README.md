### Example 1
-----------------------------------------------------------------------------------------------------

Przyk�ad pokazuje mo�liwo�� zastosowania w�asnej listy w oparciu o istniej�ce obiekty enova. Zawiera zdefiniowane w�asne    View, z kt�rym zosta�a powi�zana odpowiednia definicja w postaci struktury viewform.xml. 
    
W wyniku zastosowania dodatku, powinna pojawi� si� dodatkowa grupa w menu g��wnym programu o nazwie *`Samples`* z opcj� *`Towary w�asne`*, po wybraniu kt�rej pojawi si� zaimplementowana lista.


W sk�ad przyk�adu wchodz� trzy projekty:

* `Example1` - zawieraj�cy elementy logiki biznesowej
* `Example1.UI` - interfejsu u�ytkownika
* `Example1.Tests` - testy


#### Zawarto�� przyk�adu:
> Przyk�ad zawiera jedynie elementy interfejsu u�ytkownika wiec ca�o�� znajduje si� w `Example1.UI`.

* `Example1.UI`\Extender\TowaryUlubioneKontaktuViewInfo.cs

    Przyk�adowan klasa z implementacj� View zbudowanego na bazie tabel enova.
* `Example1.UI`\Extender\Menu.cs

    Rejestracja listy dla View

* `Example1.UI`\UI\TowaryUlubioneKontaktu.viewform.xml

    Definicja page'a dla View


* `Example1.UI`\Extender\TowaryUlubionePokazKontaktWorker.cs
  
  Rejestracja akcji dla listy towar�w ulubionych