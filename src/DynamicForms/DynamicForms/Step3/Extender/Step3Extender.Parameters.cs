using Soneta.Business;
using Soneta.Types;

namespace DynamicForms.Step3.Extender {
    public partial class Step3Extender {
        public class Params : ContextBase {
            static readonly string key = "DynamicForms.Step2";

            public Params(Context context) : base(context) {
                Load();
                NeedRefresh = true;
            }

            [Caption("Filtr")]
            public YearMonth Month {
                get {
                    return getYearMonth();
                }
                set {
                    var ym = getYearMonth();
                    NeedRefresh = ym != value;
                    setYearMonth(value);
                }
            }

            public bool NeedRefresh { get; set; }

            private YearMonth getYearMonth() {
                object o = Context[typeof(YearMonth), false];
                if (o != null)
                    return (YearMonth)o;

                return YearMonth.Today;
            }

            private void setYearMonth(YearMonth value) {
                Context[typeof(YearMonth)] = value;
                Save();
            }

            void Save() {
                Session.Login.Save(this, key, "Month");
            }

            void Load() {
                SetContext(typeof(YearMonth), Session.Login.Load(this, key, "Month"));
            }
        }
    }
}
