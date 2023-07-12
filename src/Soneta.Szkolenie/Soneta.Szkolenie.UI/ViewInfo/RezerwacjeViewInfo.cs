using Soneta.Business;
using Soneta.CRM;
using System;

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

        public class RezerwacjeParams : ContextBase // Klasa parametrów używanych w filtrze. Musi dziedziczyć z klasy ContextBase
        {
            private const string paramsKey = "Szkolenie.RezerwacjeViewParams"; // klucz używany do ujednoznacznienia zapisu parametrów

            public RezerwacjeParams(Context context) : base(context) {
                // pobieramy z kontekstu sesji logowania ewentualne zapisane parametry dla tego filtra
                _maszyna = LoadProperty<Maszyna>(nameof(Maszyna), paramsKey, null);
                _klient = LoadProperty<Kontrahent>(nameof(Klient), paramsKey, null);
            }

            private Maszyna _maszyna;
            public Maszyna Maszyna
            {
                get { return _maszyna; }
                set
                {
                    _maszyna = value; 
                    OnChanged(); // odświeżenie listy
                    SaveProperty(nameof(Maszyna), paramsKey); // zapis parametru do kontekstu sesji logowania
                }
            }

            private Kontrahent _klient;
            public Kontrahent Klient
            {
                get { return _klient; }
                set
                {
                    _klient = value;
                    OnChanged(); // odświeżenie listy
                    SaveProperty(nameof(Klient), paramsKey); // zapis parametru do kontekstu sesji logowania
                }
            }
        }

        private void RezerwacjeViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            if (!args.Context.Contains(typeof(RezerwacjeParams)))
                args.Context.Set(new RezerwacjeParams(args.Context)); // dodanie parametrów do kontekstu jeśli nie istnieją
        }

        private void RezerwacjeViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            var parameters = new RezerwacjeParams(args.Context);
            args.Context.Get(out parameters); // ew. pobranie parametrów z kontekstu

            args.View = ViewCreate(parameters); // utworzenie View
            var cond = RowCondition.Empty; // i uzupełnienie warunków wg ustawionych parametrów

            if (parameters.Maszyna != null)
                cond &= new FieldCondition.Equal("Maszyna", parameters.Maszyna);
            if (parameters.Klient != null)
                cond &= new FieldCondition.Equal("Klient", parameters.Klient);

            args.View.Condition = cond;
        }

        private View ViewCreate(RezerwacjeParams pars)
        {
            View view = SzkolenieModule.GetInstance(pars.Session).Rezerwacje.CreateView();
            return view;
        }
    }
}
