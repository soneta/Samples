using Soneta.Business;

namespace Soneta.Szkolenie.UI
{
    public class KatalogLotowViewInfo : ViewInfo
    {
        public KatalogLotowViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "KatalogLotow";

            // Inicjowanie contextu
            InitContext += KatalogLotowViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += KatalogLotowViewInfo_CreateView;
        }

        void KatalogLotowViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            args.Context.Remove(typeof(WParams));
            args.Context.TryAdd(() => new WParams(args.Context));
        }

        void KatalogLotowViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            KatalogLotowViewInfo.WParams parameters;
            if (!args.Context.Get(out parameters))
                return;
            args.View = ViewCreate(parameters);
        }

        public class WParams : ContextBase
        {
            public WParams(Context context) : base(context) {}
        }

        protected View ViewCreate(WParams pars)
        {
            View view = SzkolenieModule.GetInstance(pars.Session).Loty.CreateView();
            return view;
        }

    }
}
