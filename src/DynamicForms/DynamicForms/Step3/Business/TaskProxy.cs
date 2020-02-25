using System;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Core;
using Soneta.Zadania;

namespace DynamicForms.Step3.Business {
    public class TaskProxy {
        private readonly Zadanie zadanie;
        private readonly Context context;

        public TaskProxy(Zadanie zd, Context cx) {
            zadanie = zd;
            context = cx;
        }

        public string Title => $"Termin spotkania: {zadanie.DataOd}, {zadanie.CzasOd}";

        public Zadanie Zadanie => zadanie;

        public HyperlinkResult ShowLocalization() {
            context.Set(typeof(IAdresHost), zadanie.Kontrahent, false);
            var type = Type.GetType("Soneta.Zadania.KontrahentLokalizatorWorker,Soneta.Zadania");
            var lokalizator = context.CreateObject(null, type, null);
            var method = type.GetMethod("PokazLokalizacje");
            if (method != null) {
                var result = method.Invoke(lokalizator, null);
                return (HyperlinkResult)result;
            }
            return null;
        }
    }
}
