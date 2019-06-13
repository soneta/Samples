using System;
using System.Linq;

using Soneta.Business;
using Soneta.Business.UI;
using PrezentacjaGeekOut2019;
using PrezentacjaGeekOut2019.LotyWidokowe;
using Soneta.Core;

/*
[assembly: FolderView("PrezentacjaGeekOut2019/KatalogLotowViewInfo",
    Priority = 11,
    Description = "Katalog dostepnych lotów",
    TableName = "Lot",
    ViewType = typeof(KatalogLotowViewInfo)
)]
 */
namespace PrezentacjaGeekOut2019.LotyWidokowe
{
    public class KatalogLotowViewInfo : ViewInfo
    {
        public KatalogLotowViewInfo()
        {
            ResourceName = "KatalogLotow";

            // Inicjowanie contextu
            InitContext += KatalogLotowViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += KatalogLotowViewInfo_CreateView;
            NewRows = new[] { new NewRowAttribute("Lot widokowy", typeof(Lot)) };
            
        }
        
        void KatalogLotowViewInfo_InitContext(object sender, ContextEventArgs args)
        {
        }

        void KatalogLotowViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            View view = LotyWidokoweModule.GetInstance(args.Session).Loty.CreateView();
            args.View.AllowNew = true;
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
