

using Soneta.Business.UI;
using Samples.Example8.Extender;
using Soneta.Business.Licence;

[assembly: FolderView("Samples",
    Priority = 10,
    Description = "Przykłady implementacji enova365",
    BrickColor = FolderViewAttribute.BlueBrick,
    Contexts = new object[] { LicencjeModułu.All }
)]

[assembly: FolderView("Samples/Notowania GPW",
    Priority = 13,
    Description = "Przykład pokazujący programistyczne możliwości kolorowania wierszy",
    ObjectType = typeof(Notowania),
    ObjectPage = "Notowania.Ogolne.pageform.xml",
    ReadOnlySession = false,
    ConfigSession = false
)]
