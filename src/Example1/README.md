### Example 1
-----------------------------------------------------------------------------------------------------

Przyk³ad pokazuje mo¿liwoœæ zastosowania w³asnej listy w oparciu o istniej¹ce obiekty enova. Zawiera zdefiniowane w³asne    View, z którym zosta³a powi¹zana odpowiednia definicja w postaci struktury viewform.xml. 
    
W wyniku zastosowania dodatku, powinna pojawiæ siê dodatkowa grupa w menu g³ównym programu o nazwie *`Samples`* z opcj¹ *`Towary w³asne`*, po wybraniu której pojawi siê zaimplementowana lista.


W sk³ad przyk³adu wchodz¹ trzy projekty:

* `Example1` - zawieraj¹cy elementy logiki biznesowej
* `Example1.UI` - interfejsu u¿ytkownika
* `Example1.Tests` - testy


#### Zawartoœæ przyk³adu:
> Przyk³ad zawiera jedynie elementy interfejsu u¿ytkownika wiec ca³oœæ znajduje siê w `Example1.UI`.

* `Example1.UI`\Extender\TowaryUlubioneKontaktuViewInfo.cs

    Przyk³adowan klasa z implementacj¹ View zbudowanego na bazie tabel enova.
* `Example1.UI`\Extender\Menu.cs

    Rejestracja listy dla View

* `Example1.UI`\UI\TowaryUlubioneKontaktu.viewform.xml

    Definicja page'a dla View


* `Example1.UI`\Extender\TowaryUlubionePokazKontaktWorker.cs
  
  Rejestracja akcji dla listy towarów ulubionych