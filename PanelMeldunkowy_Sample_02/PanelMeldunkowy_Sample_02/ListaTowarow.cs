using System;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.ProdukcjaPro;
using Soneta.ProdukcjaPro.Panel;
using Soneta.Tools;
using Soneta.Towary;

namespace PanelMeldunkowy_Sample_02
{
    public class ListaTowarow : PanelBase
    {
        [Context]
        public ProMeldunek Meldunek { get; set; }
        
        public Towar Towar { get; set; }

        public View<Towar> Towary => CreateTowaryView();

        private View<Towar> CreateTowaryView()
        {
            var module = Context.Session.GetTowary();
            var cond = module.Towary.FiltrOperatoraCondition() &
                new FieldCondition.Equal("Blokada", false) & 
                new RowCondition.Exists("ProTowary", "Towar") & 
                new FieldCondition.NotEqual("Typ", TypTowaru.Usługa); 
            return module.Towary.WgKodu[cond].CreateView();
        }

        public object Wybierz(UIElement _)
        {
            if (Towar is null)
                throw new ApplicationException("Nie znaleziono towaru produkcyjnego o wskazanym kodzie.".Translate());
            DodajMaterial();
            return NavigateWithSession("Panel meldunkowy/Raportowanie/Materiały");
        }

        private void DodajMaterial()
        {
            using (var tr = Meldunek.Session.Logout(true))
            {
                var material = new ProMaterialMeldunku { Meldunek = Meldunek };
                Meldunek.Session.AddRow(material);
                material.Towar = Towar;
                tr.Commit();
            }
        }

        public object NavigateMenu(UIElement element) => new FormActionResult
        {
            Action = FormAction.Close,
            CommittedHandler = cx =>
            {
                cx.Remove(typeof(ProMeldunek));
                return new NavigationResult(element.Tag) { Context = cx, Target = NavigationTarget.NewTab };
            }
        };
    }
}
