### Example 9
-----------------------------------------------------------------------------------------------------

Przykład pokazuje implementacje własnej klasy, pozwalającej na dynamiczne uzupełnienie zawartości 
formularza. Dla zaimplementowanej klasy została utworzona dedykowana definicja formularza. 
W przykładzie pokazano wykorzystanie elementu *Include* zasilanego kontentem przez metodę zwracającą
dynamiczną wartość formularza zależną od specyficznych warunków. 

W wyniku zastosowania dodatku, powinna pojawić się dodatkowa grupa w menu głównym programu o nazwie 
*Soneta.Examples*, z opcją *"Formularz dynamiczny"*, po wybraniu której pojawi się formularz utworzony
za pośrednictwem kodu programu.

* `Example9.UI`\Extender\DynamicFormExtender.cs

    Przykładowan klasa implementująca dane oraz metody
* `Example9.UI`\Extender\DynamicFormExtender.Ogolne.pageform.xml

    Definicja zakładki powiązanej z klasą implementująca dane
* `Example9.UI`\Menu

    Rejestracja View