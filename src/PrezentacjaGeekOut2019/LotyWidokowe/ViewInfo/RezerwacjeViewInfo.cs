using System;
using System.Collections.Generic;
using System.Linq;

using Soneta.Business;
using Soneta.Business.UI;
using PrezentacjaGeekOut2019.LotyWidokowe;
using Soneta.Core;
using Soneta.CRM;
using Soneta.Types;

/*[assembly: FolderView("PrezentacjaGeekOut2018/RezerwacjeViewInfo",
    Priority = 15,
    Description = "Rezerwacje lotów widokowych",
    TableName = "Rezerwacje",
    ViewType = typeof(RezerwacjeViewInfo)
)] */

namespace PrezentacjaGeekOut2019.LotyWidokowe
{
    public class RezerwacjeViewInfo : ViewInfo
    {

        [Context]
        public Context Context { get; set; }

        public RezerwacjeViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "Rezerwacje";

            // Inicjowanie contextu
            InitContext += RezerwacjeViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += RezerwacjeViewInfo_CreateView;
            NewRows = new[] {new NewRowAttribute("Rezerwacje lotów widokowych", typeof(Rezerwacja))};
        }

        void RezerwacjeViewInfo_InitContext(object sender, ContextEventArgs args)
        {
          
            if (args?.Context == null)
                return;

            ClearData(args.Context, new[] { typeof(WParams)});
            args.Context.TryAdd(() => new WParams(args.Context));

        }

        void RezerwacjeViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            View view = LotyWidokoweModule.GetInstance(args.Session).Rezerwacje.CreateView();
            view.AllowNew = true;
            args.Context.Get(out WParams pars);
            if (pars.Typ != CzyOplacone.Razem)
                view.Condition = new FieldCondition.Equal("CzyOplacona", pars.Typ);

            args.View = view;
        }

        

        private static WParams CreateParams(Context context)
        {
            return new WParams(context);
        }


        private static void ClearData(Context cx, IEnumerable<Type> typesToRemove)
        {
            if (cx == null)
                return;

            foreach (var type in typesToRemove)
                cx.Remove(type);
        }



    }
    public class WParams : ContextBase
    {
        private CzyOplacone _typ = CzyOplacone.Razem;
        private const string Key = "Prezentacja.RezerwacjeView";

        public WParams(Context context) : base(context)
        {
        }

        [Caption("Status zapłaty")]
        public CzyOplacone Typ
        {
            get { return _typ; }
            set
            {
                _typ = value;
                OnChanged(EventArgs.Empty);
            }
        }

        public CzyOplacone[] GetListOplacone()
        {
            return new[] { CzyOplacone.Razem, CzyOplacone.Nieoplacone, CzyOplacone.Oplacone };
        }
    }
}
