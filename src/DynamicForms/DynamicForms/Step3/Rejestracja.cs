using DynamicForms.Step3.Extender;
using Soneta.Business.Licence;
using Soneta.Business.UI;

[assembly: FolderView("GeekOut/DynamicForms/Krok 3", Priority = 20,
    IconName = "komentarz",
    Description = "Generowana lista",
    ObjectType = typeof(Step3Extender),
    ObjectPage = "Step3.Ogolne.pageform.xml",
    ReadOnlySession = true,
    ConfigSession = false,
    Contexts = new object[] { LicencjeModułu.All }
)]