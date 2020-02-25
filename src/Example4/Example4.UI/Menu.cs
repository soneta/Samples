
using Soneta.Business.Licence;
using Soneta.Business.UI;
using Samples.Example4;

[assembly: FolderView("Samples",
    Priority = 10,
    Description = "Przykłady implementacji enova365",
    BrickColor = FolderViewAttribute.BlueBrick,
    Contexts = new object[] { LicencjeModułu.All }
)]

[assembly: FolderView("Samples/Kursy walut NBP",
    Priority = 11,
    Description = "Przykład kursów walut pobieranych z NBP online",
    ObjectType = typeof(DzienneKursyWalutNbp),
    ObjectPage = "DzienneKursyWalutNbp.Ogolne.pageform.xml",
    ReadOnlySession = false,
    ConfigSession = false
)]