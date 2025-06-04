using PanelMeldunkowy_Sample_02;
using Soneta.Business.Licence;
using Soneta.Business.UI;

[assembly: FolderView("Panel meldunkowy/Sample",
    Priority = 1000,
    Description = "Obsługa kafelków dodatkowych dla panelu meldunkowego",
    IconName = "jodit-folder",
    Contexts = [LicencjeModułu2.PXN]
)]

[assembly: FolderView("Panel meldunkowy/Sample/Lista towarów",
    Priority = 100,
    Description = "Lista towarów do wskazania",
    IconName = "jodit-menu",
    ObjectType = typeof(ListaTowarow),
    ObjectPage = "ListaTowarow.Sample.pageform.xml",
    ReadOnlySession = false,
    ConfigSession = false,
    Contexts = [LicencjeModułu2.PXN]
)]