using System.Collections.Generic;
using DynamicForms.Step3.Business;
using Soneta.Business;
using Soneta.Types;
using Soneta.Zadania;

namespace DynamicForms.Step3.Extender {
    public partial class Step3Extender {
        private TaskProxy[] tasks;
        private YearMonth recentMonth;

        public void RefreshTasks() {
            if (!FilterParameters.NeedRefresh) {
                return;
            }
            FilterParameters.NeedRefresh = false;

            var list = new List<TaskProxy>();
            var zadaniaModule = ZadaniaModule.GetInstance(Context);
            var condition = RowCondition.Empty;
            condition &= new FieldCondition.GreaterEqual("DataOd", FilterParameters.Month.FirstDay);
            condition &= new FieldCondition.LessEqual("DataOd", FilterParameters.Month.LastDay);

            var zadania = zadaniaModule.Zadania.WgKontrahent.CreateView();
            zadania.Sort = "DataOd, CzasOd";
            zadania.Condition = condition;

            foreach (Zadanie zadanie in zadania) {
                list.Add(new TaskProxy(zadanie, Context));
            }
            tasks = list.ToArray();
        }


        private void checkBuffer() {
            if (FilterParameters.Month != recentMonth) {
                root = null;
                tasks = null;
                recentMonth = FilterParameters.Month;
            }
        }
    }
}
