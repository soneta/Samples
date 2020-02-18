### Example 3
-----------------------------------------------------------------------------------------------------

Przykład pokazuje możliwość podpięcia własnej zakładki konfiguracyjnej dla potrzeb własnego dodatku.
W przykładzie zastosowano mechanizm tworzenia ustawień konfiguracji bezpośrednio w kodzie programu, bez stosowania pliku *config.xml*.

* Extender\Menu.cs 

  Rejestracja listy dla View

* UI\Config.ZakładkaTowary.pageform.xml
  
  Definicja formularza ustawień w Narzędzia/Opcje...

    
* Extender\ZakladkaTowaryConfigExtender.cs

  Obsługa zapisu i odczytu ustawień


* Extender\TowaryZakladkaViewInfo.cs

    Implementacja View ograniczona do sterowania widocznością zakładki.
