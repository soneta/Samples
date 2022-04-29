## Przykłady workerów opartych na Soneta.MSBuild.SDK.

W zależności od potrzeby i sposobu rejestracji workery mogą operować na pojedynczych wierszach lub na zakresie zaznaczonych na liście wierszy.  
Worker operujący  na zakresie zaznaczonych 

### Worker na liście: 

Folder [./WorkerNaLiscie](WorkerNaLiscie/)  
Przykład workera zarejestrowanego dla tabeli, więc umieszczonego na liście i operującego łącznie na zaznaczonych wierszach (Dokumentów handlowych).  

### Worker na wierszach:
Folder [./WorkerNaWierszach](WorkerNaWierszach/)  
Przykład workera zarejestrowanego dla typu wiersza, więc umieszczonego na formularzu oraz na liście obiektów i operującym na każdym wierszu (Towaru) z osobna.  

### Worker na liście i wierszu:
Folder [./WorkerNaWierszuILiscie](WorkerNaWierszuILiscie/)  
Przykład dwóch workerów zarejestrowanych: jednego dla tabeli, więc umieszonego na liście, a drugiego dla typu wiersza, więc pojawiającego się na formularzu kontrahenta, oraz poprawnej ich konstrukcji dla operacji odpowiednio na pojedynczym (otwartym) lub łącznie na zaznaczonych na liście wierszach (Kontrahentach).  