using System;
using System.Linq;

using Soneta.Business;
using Soneta.Business.UI;
using PrezentacjaGeekOut2019;


namespace PrezentacjaGeekOut2019.LotyWidokowe
{
    public class KatalogSamolotowViewInfo : ViewInfo
    {
        public KatalogSamolotowViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "KatalogSamolotow";

            // Inicjowanie contextu
            InitContext += KatalogSamolotowViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += KatalogSamolotowViewInfo_CreateView;
            NewRows = new[] {new NewRowAttribute("Dostępne maszyny", typeof(Maszyna))};
        }

        void KatalogSamolotowViewInfo_InitContext(object sender, ContextEventArgs args)
        {
        }

        void KatalogSamolotowViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
           
            View view = LotyWidokoweModule.GetInstance(args.Session).Maszyny.CreateView();
            args.View.AllowNew = true;
            args.View.AllowEdit = true;
            args.View = view;
        }

        public class WParams : ContextBase
        {
            public WParams(Context context) : base(context)
            {
            }
        }

        protected View ViewCreate(WParams pars)
        {
            View view = null;
            return view;
        }

    }
}
