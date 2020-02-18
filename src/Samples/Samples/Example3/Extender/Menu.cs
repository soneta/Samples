
using Soneta.Business.Licence;
using Soneta.Business.UI;
using Samples.Example3.Extender;

/*
[assembly: FolderView("Samples",
    Priority = 10,
    Description = "Przykłady implementacji enova365",
    BrickColor = FolderViewAttribute.BlueBrick,
    Contexts = new object[] { LicencjeModułu.All }
)]
*/

[assembly: FolderView("Samples/Zakladka towary",
    Priority = 11,
    Description = "Towary - zakladka",
    TableName = "Towary",
    ViewType = typeof(TowaryZakladkaViewInfo)
)]