
using Soneta.Business.Licence;
using Soneta.Business.UI;
using Samples.Example1.UI.Extender;

[assembly: FolderView("Samples",
    Priority = 10,
    Description = "Przykłady implementacji enova365",
    BrickColor = FolderViewAttribute.BlueBrick,
    Contexts = new object[] { LicencjeModułu.All }
)]

[assembly: FolderView("Samples/Towary własne",
    Priority = 11,
    Description = "Towary ulubione osoby kontaktowej",
    TableName = "TowaryUlubione",
    ViewType = typeof(TowaryUlubioneKontaktuViewInfo)
)]