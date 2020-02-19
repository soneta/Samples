using DynamicForms.Step3.Business;
using Soneta.Business;

[assembly: Worker(typeof(DynamicForms.Step3.Extender.Step3Extender))]

namespace DynamicForms.Step3.Extender {
    public partial class Step3Extender : ContextBase {

        private Params _parameters;

        public Step3Extender(Context cx) : base(cx) {
        }

        public Params FilterParameters => _parameters ?? (_parameters = new Params(Context));

        public TaskProxy[] Tasks {
            get {
                checkBuffer();
                if (tasks == null) {
                    RefreshTasks();
                }
                return tasks;
            }
        }
    }
}
