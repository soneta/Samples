using Soneta.Business;
using Samples.Example10.Extender;

[assembly: Service(typeof(ICennikSerwis), typeof(CennikSerwis), Published = true)]
namespace Samples.Example10.Extender {

    public partial class CennikSerwis : ICennikSerwis {

        private readonly Session _session;

        public CennikSerwis(Session session) {
            _session = session;
        }
    }
}
