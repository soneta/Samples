
using System;
using Samples.Example9.UI.Extender;
using Soneta.Business.Licence;
using Soneta.Business.UI;

[assembly: FolderView("Samples",
    Priority = 10,
    Description = "Przykłady implementacji enova365",
    BrickColor = FolderViewAttribute.BlueBrick,
    Contexts = new object[] { LicencjeModułu.All }
)]

[assembly: FolderView("Samples/Formularz dynamiczny",
    Priority = 11,
    Description = "Przykład pokazujący programistyczne możliwości dynamicznego tworzenia formularza",
    ObjectType = typeof(DynamicFormExtender),
    ObjectPage = "DynamicFormExtender.ogolne.pageform.xml",
    ReadOnlySession = false,
    ConfigSession = false
)]