using System;
using System.Linq;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Szkolenie.UI;

namespace Soneta.Szkolenie.UI
{
    public class RezerwacjeViewInfo : ViewInfo
    {
        public RezerwacjeViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "Rezerwacje";

            // Inicjowanie contextu
            InitContext += RezerwacjeViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += RezerwacjeViewInfo_CreateView;
        }

        public class WParams : ContextBase
        {
            public WParams(Context context) : base(context) { }
        }

        private void RezerwacjeViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            args.Context.Remove(typeof(WParams));
            args.Context.TryAdd(() => new WParams(args.Context));
        }

        private void RezerwacjeViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            RezerwacjeViewInfo.WParams parameters;
            if (!args.Context.Get(out parameters))
                return;
            args.View = ViewCreate(parameters);
        }

        private View ViewCreate(WParams pars)
        {
            View view = SzkolenieModule.GetInstance(pars.Session).Rezerwacje.CreateView();
            return view;
        }
    }
}
