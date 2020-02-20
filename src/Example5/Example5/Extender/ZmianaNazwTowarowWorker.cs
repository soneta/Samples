
using System.Linq;
using Soneta.Business;
using Samples.Example5.Extender;
using Soneta.Tools;
using Soneta.Towary;


[assembly: Worker(typeof(ZmianaNazwTowarowWorker), typeof(Towary))]

namespace Samples.Example5.Extender {

    public class ZmianaNazwTowarowWorker : ContextBase {

        public ZmianaNazwTowarowWorker(Context context) : base(context) {
        }

        // Potrzebne dla akcji parametry
        [Context]
        public ZmianaNazwTowarowParams PrefixParams {
            get;
            set;
        }

        // Potrzebne dane na których zostanie wykonana akcja
        [Context]
        public Towar[] Towary {
            get; set;
        }

        // Akcja jaka zostanie wykonana na danych w oparciu o ustawione parametry
        [Action("Soneta Examples/Zmiana postfix-prefix", Mode = ActionMode.SingleSession | ActionMode.ConfirmSave | ActionMode.Progress)]
        public void ZmianaNazw() {
            using (var t = PrefixParams.Session.Logout(true)) {
                foreach (var towar in Towary.Where(towar => PrefixParams.TypTowaru == towar.Typ)) {

                    if (!PrefixParams.DodajPrefix.IsNullOrEmpty() && !towar.Nazwa.StartsWith(PrefixParams.DodajPrefix)) {
                        towar.Nazwa = PrefixParams.DodajPrefix + towar.Nazwa;
                    }

                    if (!PrefixParams.DodajPostfix.IsNullOrEmpty() && !towar.Nazwa.StartsWith(PrefixParams.DodajPostfix)) {
                        towar.Nazwa += PrefixParams.DodajPostfix;
                    }

                    if (!PrefixParams.UsunPrefix.IsNullOrEmpty() && towar.Nazwa.StartsWith(PrefixParams.UsunPrefix)) {
                        towar.Nazwa = towar.Nazwa.Substring(PrefixParams.UsunPrefix.Length);
                    }

                    if (!PrefixParams.UsunPostfix.IsNullOrEmpty() && towar.Nazwa.EndsWith(PrefixParams.UsunPostfix)) {
                        towar.Nazwa = towar.Nazwa.Substring(0, towar.Nazwa.Length - PrefixParams.UsunPostfix.Length);
                    }

                }
                t.Commit();
            }
        }
    }

    public class ZmianaNazwTowarowParams : ContextBase {

        public ZmianaNazwTowarowParams(Context context) : base(context) {
            TypTowaru = TypTowaru.Towar;
        }

        public TypTowaru TypTowaru { get; set; }

        public string DodajPrefix { get; set; }

        public string DodajPostfix { get; set; }

        public string UsunPrefix { get; set; }

        public string UsunPostfix { get; set; }
    }
}
