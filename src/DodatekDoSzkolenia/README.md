#Dodatek z własnymi tabelami, posiadającymi referencje do tabel innego dodatku

Przykład dodatku wprowadzającego własne tabele bazodanowe, które posiadają referencje do tabel bazodanowych wprowadzonych przez inny dodatek (w tym przypadku dodatek **Soneta.Szkolenie**).

Warunkiem użycia tabel innego dodatku jest dostęp do pliku **.business.xml* tego dodatku, którego tabel chcemy użyć. Jest to niezbędne dla Generatora, który musi mieć informację na temat klas wprowadzonych przez referowany dodatek aby poprawnie wygenerować plik **.business.cs*.

W pliku **.business.xml* bieżącego dodatku należy użyć atrybutu

    <import></import>

w którym należy umieścić ścieżkę dostępu do pliku/plików **.business.xml* referowanego dodatku.

Ten atrybut deklaruje folder, w którym będą poszukiwane pozostałe pliki **.business.xml*.
Wczytywane są wszystkie pliki ze wskazanego folderu oraz folderów podrzędnych.

Zalecane jest podawanie ścieżek relatywnych, gdyż ułatwia to późniejsze utrzymanie kodu. Oczywiście autor dodatku musi wtedy zadbać o stałe utrzymywanie poprawnej struktury repozytoriów lub dostosowywanie tej ścieżki do ewentualnych zmian.

Poza wskazaniem Generatorowi dodatkowych plików **.business.xml* należy także dodać do bieżącgo pliku **.business.xml* atrybut

    <using></using>

z odpowiednim namespace z referowanego dodatku.

Dodatkowo trzeba jeszcze w projekcie (w pliku .csproj bieżącego dodatku) dodać referencję do biblioteki DLL (assembly) referowanego dodatku w celu poprawnej kompilacji kodu.


##UWAGA!

Niniejszy przykład został umieszczony jako część solution zawierającego także projekt dodatku **Soneta.Szkolenie**, ale nie jest konieczne umieszczanie obu projektów (dodatku bieżącego i referowanego) w tym samym solution. Może to jednak znacznie ułatwić utrzymanie kodu obu dodatków.
