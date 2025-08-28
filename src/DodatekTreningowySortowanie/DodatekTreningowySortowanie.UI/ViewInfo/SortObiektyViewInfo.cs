using DodatekTreningowySortowanie.UI.ViewInfo;
using Soneta.Business.UI;

[assembly: FolderView("Dodatek treningowy", Description = "Dodatek treningowy")]
[assembly: FolderView("Dodatek treningowy/Tabela SortObiekty",
    TableName = "SortObiekty",
    Priority = 1,
    Description = "Tabela SortObiekty",
    ViewType = typeof(SortObiektyViewInfo))]
namespace DodatekTreningowySortowanie.UI.ViewInfo
{
    public class SortObiektyViewInfo : Soneta.Business.ViewInfo
    {
        public SortObiektyViewInfo()
        {
            NewRowTable = "SortObiekt";
            CreateView += SortObiektyView_CreateView;
        }

        private void SortObiektyView_CreateView(object sender, Soneta.Business.CreateViewEventArgs args)
        {
            args.View = args.Session.GetDodatekTreningowySortowanie().SortObiekty.CreateView();
        }
    }
}
