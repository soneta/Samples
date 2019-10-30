using Soneta.Business.UI;

/* Rejestracje folderów dodatku przeniesione z poszczególnych plików ViewInfo tutaj w celach porządkowych */

// Główny folder dodatku, umieszczony w głównym widoku bazy danych
[assembly: FolderView("Loty widokowe", // wymagane: to jest tekst na kaflu
    Priority = 0, // opcjonalne: Priority = 0 umieszcza kafel blisko lewej górnej strony widoku kafli
    Description = "Szkolenie techniczne - przykład dodatku", // opcjonalne: opis poniżej tytułu kafla
    BrickColor = FolderViewAttribute.BlueBrick, // opcjonalne: Kolor kafla
    Icon = "TableFolder.ico" // opcjonalne: Ikona wyświetlana na kaflu
    // Więcej nie ma potrzeby definiować bo jest to kafel "organizacyjny" - przechodzący do widoku innych kafli
)]

// Poszczególne foldery widoków wewnątrz głównego "kafla"
[assembly: FolderView("Loty widokowe/Klienci", // Tu widać ścieżkę definiującą strukturę drzewa kafli
    Priority = 0,
    Description = "Klienci",
    TableName = "Kontrahenci", // wymagane dla list: Nazwa tabeli, z której tworzone jest to ViewInfo
    ViewType = typeof(Soneta.Szkolenie.UI.KlienciViewInfo), // wymagane dla list: dokładne wskazanie klasy ViewInfo, 
                                                            // która zostanie użyta do wyświetenia widoku pod tym kaflem
    BrickColor = FolderViewAttribute.YellowBrick // opcjonalne: ten jeden kafel będzie żółty
)]

[assembly: FolderView("Loty widokowe/Katalog lotów",
    Priority = 100,
    Description = "Katalog lotów",
    TableName = "Loty",
    ViewType = typeof(Soneta.Szkolenie.UI.KatalogLotowViewInfo)
)]

[assembly: FolderView("Loty widokowe/Katalog maszyn",
    Priority = 200,
    Description = "Katalog maszyn",
    TableName = "Maszyny",
    ViewType = typeof(Soneta.Szkolenie.UI.KatalogMaszynViewInfo)
)]

[assembly: FolderView("Loty widokowe/Rezerwacje",
    Priority = 300,
    Description = "Lista rezerwacji",
    TableName = "Rezerwacje",
    ViewType = typeof(Soneta.Szkolenie.UI.RezerwacjeViewInfo)
)]