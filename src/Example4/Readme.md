### Example 4
-----------------------------------------------------------------------------------------------------

Przykład pokazuje implementacje własnej klasy, nie powiązanej z logiką enova365. Dla zaimplementowanej 
klasy została utworzona dedykowana definicja formularza. W przykładzie pokazano wykorzystanie *Command* 
oraz podpięcia do nich własnych metod. 

W wyniku zastosowania dodatku, powinna pojawić się dodatkowa grupa w menu głównym programu o nazwie 
*Soneta.Examples*, z opcją *"Kursy walut NBP"*, po wybraniu której pojawi się zaimplementowana lista.

Na liście pokazane zostało użycie zaimplementowanych metod extender'a:
* `Aktualizuj` - uzupełnia dane pobrane z serwera 
* `AktualizujZPliku` - która uzupełnia dane na liście na podstawie pliku XML.
* `PobierzXml` - pokazuje możliwość ściągnięcia pliku xml, zawierającego
dane przygotowane po stronie serwera. 

Adres url serwera można wprowadzić w konfiguracji *Narzedzia/Opcje/Samples/Ustawienia Kursy NBP*

W skład przykładu wchodzą trzy projekty:

* `Example4` - zawierający elementy logiki biznesowej
* `Example4.UI` - interfejsu użytkownika
* `Example4.Tests` - testy


#### Zawartość przykładu:

* `Example4`\KursWalutyNbp.cs

    Klasa implemenująca pojedynczy wiersz wyświetlany na liście zdefiniowanej na zakładce.

* `Example4`\DzienneKursyWalutNbp.*.cs 
   
   Klasa implementująca dane oraz metody

* `Example4`\Extender\CfgWalutyNbpExtender.cs

    Przykładowa klasa implementująca ustawienia konfiguracyjne

*  `Example4.UI`\UI\Config.CfgWalutyNbpExtender.pageform.xml

    Definicja formularza własnej zakładki konfiguracyjnej

*  `Example4.UI`\UI\DzienneKursyWalutNbp.Ogolne.pageform.xml

   Definicja page'a dla kursów

*  `Example4.UI`\Menu.cs

   Rejestracja zakładki