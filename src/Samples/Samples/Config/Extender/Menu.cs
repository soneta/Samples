
using Soneta.Business.Licence;
using Soneta.Business.UI;
using Samples.Lists.Extender;


[assembly: FolderView("Samples",
    Priority = 10,
    Description = "Przykłady implementacji enova365",
    BrickColor = FolderViewAttribute.BlueBrick,
    Icon = "Samples.Utils.examples.ico;Samples",
    Contexts = new object[] { LicencjeModułu.All }
)]

[assembly: FolderView("Samples/Towary własne",
    Priority = 11,
    Description = "Towary ulubione osoby kontaktowej",
    TableName = "TowaryUlubione",
    ViewType = typeof(TowaryUlubioneKontaktuViewInfo)
)]