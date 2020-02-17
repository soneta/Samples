### Example 1
-----------------------------------------------------------------------------------------------------

Przykład pokazuje możliwość zastosowania własnej listy w oparciu o istniejące obiekty enova. Zawiera zdefiniowane własne    View, z którym została powiązana odpowiednia definicja w postaci struktury viewform.xml. 
    
W wyniku zastosowania dodatku, powinna pojawić się dodatkowa grupa w menu głównym programu o nazwie *Samples* z opcją *"Towary własne"*, po wybraniu której pojawi się zaimplementowana lista.

* Extender\TowaryUlubioneKontaktuViewInfo.cs

    Przykładowan klasa z implementacją View zbudowanego na bazie tabel enova.
* Extender\Menu.cs

    Rejestracja listy dla View

* UI\TowaryUlubioneKontaktu.viewform.xml

    Definicja page'a dla View

* UI\Config.TowaryUlubione.pageform.xml
  
  Definicja formularza ustawień w Narzędzia/Opcje...
    
* Extender\TowaryUlubioneConfigExtender.cs

  Obsługa zapisu ustawień w Narzędzia/Opcje...

* Extender\TowaryUlubionePokazKontaktWorker.cs
  
  Rejestracja akcji dla listy towarów ulubionych otwarcia powiazanych rekordów 