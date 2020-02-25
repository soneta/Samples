### Example 8
-----------------------------------------------------------------------------------------------------

Przykład pokazuje programistyczne możliwości kolorowania wierszy na liście. 

Aby system odpowiedno wyświetlił dany rekord, należy w odpowiedniej klasie (dotyczy to zarówno obiektów biznesowych jak i niesesyjnych)
dodać metodę GetAppearance(). Metoda ta sprawi, że pokolorowany zostanie cały wiersz. Aby pokolorowana została tylko dana komórka, należy 
do metody GetAppearance dodać sufiks zawierający nazwę property np. GetAppearanceNazwa(). 

Obiekt DataAppearance zwracany przez metodę GetAppearance() pozwala ustawić takie właściwości jak kolor tła oraz kolor i styl czcionki.

W skład przykładu wchodzą trzy projekty:

* `Example8` - zawierający elementy logiki biznesowej
* `Example8.UI` - interfejsu użytkownika
* `Example8.Tests` - testy


#### Zawartość przykładu:

* `Example8`\Extender\Akcja.cs

    Przykładowa klasa implementująca metody odpowiedzialne za kolorowanie wiersza.
* `Example8`\Extender\Notowania.cs

    Przykładowe dane.

* `Example8.UI`\UI\Notowania.Ogolne.pageform.xml
   
   Definicja page'a dla Notowań

* `Example8.UI`\ViewInfo\Menu.cs
   
   Rejestracja View