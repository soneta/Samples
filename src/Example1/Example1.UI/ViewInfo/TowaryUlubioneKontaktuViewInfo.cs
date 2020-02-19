using Soneta.Business;
using Soneta.CRM;
using Soneta.Towary;

namespace Samples.Example1.UI.Extender
{
    /// <summary>
    /// Lista oparta na przykładzie pochodzącym z repozytorium Soneta.Examples - https://github.com/soneta/Examples (Example 1)
    /// 
    /// </summary>
    public class TowaryUlubioneKontaktuViewInfo : ViewInfo
    {

        public TowaryUlubioneKontaktuViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "TowaryUlubioneKontaktu";

            // Inicjowanie contextu
            InitContext += (sender, args) => { args.Context.TryAdd(() => new WParams(args.Context)); };

            // Tworzenie view zawierającego konkretne dane
            CreateView += TowaryWlasneViewInfo_CreateView;
        }

        void TowaryWlasneViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            WParams parameters;
            if (!args.Context.Get(out parameters))
                return;
            args.View = ViewCreate(parameters);
            //args.View.AllowNew = false;
        }

        protected View ViewCreate(WParams pars)
        {

            var rc = RowCondition.Empty;
            var tm = TowaryModule.GetInstance(pars.Context.Session);
            var view = tm.TowaryUlubione.CreateView();

            if (pars.KontaktOsoba != null)
                rc &= new FieldCondition.Equal("Zapis", pars.KontaktOsoba);

            view.Condition &= rc;

            return view;
        }

        #region Widoczność zakładki

        /// <summary>
        /// Metoda pozwalająca na sterowanie widocznościa zakładki.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>
        ///     true - widoczność zakładki, 
        ///     false - zakładka niewidoczna
        /// </returns>
        public static bool IsVisible(Context context)
        {
            bool result;
            using (var session = context.Login.CreateSession(true, true))
            {
                result = TowaryUlubioneConfigExtender.IsAktywneZakladkaSamples(session);
            }
            return result;
        }

        #endregion Widoczność zakładki
    }

    public class WParams : ContextBase
    {
        private const string Key = "Samples.TowaryWlasne";

        public WParams(Context context) : base(context)
        {
            Load();
        }

        public KontaktOsoba KontaktOsoba
        {
            get
            {
                if (Context.Contains(typeof(KontaktOsoba)))
                    return (KontaktOsoba)Context[typeof(KontaktOsoba)];
                return null;
            }
            set
            {
                Context[typeof(KontaktOsoba)] = value;
                Save();
            }
        }

        /// <summary>
        /// Ładowanie parametrów z kontekstu login'a
        /// </summary>
        protected void Load()
        {
            var property = Context.LoadProperty(this, "KontaktOsoba", Key);
            SetContext(typeof(KontaktOsoba), property);
        }

        /// <summary>
        /// Zapisywanie parametrów w kontekście login'a
        /// </summary>
        protected void Save()
        {
            Context.SaveProperty(this, "KontaktOsoba", Key);
        }
    }

}
