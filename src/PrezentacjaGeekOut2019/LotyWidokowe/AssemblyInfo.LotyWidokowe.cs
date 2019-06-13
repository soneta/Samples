using PrezentacjaGeekOut2019.LotyWidokowe;
using Soneta.Business.Licence;
using Soneta.Business.UI;

[assembly: FolderView("Loty widokowe",
    Priority = 10,
    Description = "Przykłady implementacji enowa365",
    BrickColor = FolderViewAttribute.BlueBrick,
    Contexts = new object[] { LicencjeModułu.All }
)]
[assembly: FolderView("Loty widokowe/Dostępne loty",
    Priority = 11,
    Description = "Katalog dostępnych lotów widokowych",
    TableName = "Lot",
    ViewType = typeof(KatalogLotowViewInfo)
)]

[assembly: FolderView("Loty widokowe/Dostępne maszyny",
    Priority = 12,
    Description = "Katalog dostępnych samolotów",
    TableName = "Maszyna",
    ViewType = typeof(KatalogSamolotowViewInfo)
)]


[assembly: FolderView("Loty widokowe/Rezerwacje",
    Priority = 13,
    Description = "Rezewracje lotów widokowych",
    TableName = "Rezerwacja",
    ViewType = typeof(RezerwacjeViewInfo)
)]
