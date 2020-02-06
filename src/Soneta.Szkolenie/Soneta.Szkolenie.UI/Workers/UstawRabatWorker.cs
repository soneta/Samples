using Soneta.Business;
using Soneta.Business.UI;
using Soneta.CRM;
using Soneta.Tools;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;

[assembly: Worker(typeof(Soneta.Szkolenie.UI.UstawRabatWorker), typeof(Kontrahenci))]

namespace Soneta.Szkolenie.UI
{
    public class UstawRabatWorker
    {
        [Context]
        public UstawRabatWorkerParams @params
        {
            get;
            set;
        }

        [Action("Loty widokowe/Ustaw rabat", Mode = ActionMode.SingleSession | ActionMode.ConfirmSave | ActionMode.Progress)]
        public object UstawRabat()
        {
            var WybraniKontrahenci = @params.Context[typeof(Kontrahent[]), false] as Kontrahent[];

            if (WybraniKontrahenci.Length == 0)
                return "Nie wybrano żadnego kontrahenta.";

            var doZmiany = new List<Kontrahent>();

            foreach (var kth in WybraniKontrahenci)
            {
                var nowyRabat = @params.DodawacRabaty ? kth.RabatTowaru + @params.Rabat : @params.Rabat;

                if (!@params.ObnizacRabaty && nowyRabat < kth.Rabat)
                    continue;

                doZmiany.Add(kth);
            }

            if (doZmiany.Count == 0)
                return "Dla wybranych kontrahentów nie ma nic do zrobienia.";

            return new MessageBoxInformation("Ustawienie rabatu?")
            {
                Text = "Czy ustawić rabat ({0}) wybranym kontrahentom ({1})?"
                    .TranslateFormat(doZmiany.Count, @params.Rabat),
                YesHandler = () =>
                {
                    using (var t = @params.Session.Logout(true))
                    {
                        t.Commit();
                    }
                    return "Operacja została zakończona";
                },
                NoHandler = () => "Operacja przerwana"
            };

        }
    }

    [Caption("Ustawienie rabatu dla kontrahentów")]
    public class UstawRabatWorkerParams : ContextBase
    {
        public UstawRabatWorkerParams(Context context) : base(context){}

        [Caption("Wysokość rabatu")]
        public Percent Rabat { get; set; }

        [Caption("Czy dodawać rabaty")]
        [Description("Czy dodawać rabaty do siebie?")]
        public bool DodawacRabaty
        {
            get => _dodawacRabaty;
            set {
                _dodawacRabaty = value;
                ObnizacRabaty = !_dodawacRabaty;
                OnChanged(EventArgs.Empty);
            }
        }

        [Caption("Czy obniżać rabaty")]
        [Description("Czy zmniejszyć przypisany rabat jeśli już przypisany jest wyższy?")]
        public bool ObnizacRabaty { get; set; } = false;

        public bool IsReadOnlyObnizacRabaty() => DodawacRabaty;

        #region privates

        private bool _dodawacRabaty = false;

        #endregion privates
    }
}
