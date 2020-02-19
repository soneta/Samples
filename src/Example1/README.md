### Example 1
-----------------------------------------------------------------------------------------------------

Przyk³ad pokazuje mo¿liwoœæ zastosowania w³asnej listy w oparciu o istniej¹ce obiekty enova. Zawiera zdefiniowane w³asne    View, z którym zosta³a powi¹zana odpowiednia definicja w postaci struktury viewform.xml. 
    
W wyniku zastosowania dodatku, powinna pojawiæ siê dodatkowa grupa w menu g³ównym programu o nazwie *Samples* z opcj¹ *"Towary w³asne"*, po wybraniu której pojawi siê zaimplementowana lista.

* Extender\TowaryUlubioneKontaktuViewInfo.cs

    Przyk³adowan klasa z implementacj¹ View zbudowanego na bazie tabel enova.
* Extender\Menu.cs

    Rejestracja listy dla View

* UI\TowaryUlubioneKontaktu.viewform.xml

    Definicja page'a dla View

* UI\Config.TowaryUlubione.pageform.xml
  
  Definicja formularza ustawieñ w Narzêdzia/Opcje...
    
* Extender\TowaryUlubioneConfigExtender.cs

  Obs³uga zapisu ustawieñ w Narzêdzia/Opcje...

* Extender\TowaryUlubionePokazKontaktWorker.cs
  
  Rejestracja akcji dla listy towarów ulubionych otwarcia powiazanych rekordów 