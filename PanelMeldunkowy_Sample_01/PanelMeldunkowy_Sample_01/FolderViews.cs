using PanelMeldunkowy_Sample_01;
using Soneta.Business.Licence;
using Soneta.Business.UI;

[assembly: FolderView("Panel meldunkowy/Sample",
    Priority = 1000,
    Description = "Obsługa kafelków dodatkowych dla panelu meldunkowego",
    IconName = "jodit-folder",
    Contexts = [LicencjeModułu2.PXN]
)]

[assembly: FolderView("Panel meldunkowy/Sample/Materiały planowane",
    Priority = 100,
    Description = "Ekran materiałów planowanych do pobrania",
    IconName = "wozek",
    ObjectType = typeof(MaterialyPlanowane),
    ObjectPage = "MaterialyPlanowane.Sample.pageform.xml",
    ReadOnlySession = false,
    ConfigSession = false,
    Contexts = [LicencjeModułu2.PXN]
)]