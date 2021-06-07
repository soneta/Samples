using Soneta.Business;
using Soneta.Towary;

namespace WebApiService
{
    public class GetTowarInfo
    {
        Session session { get; set; }

        public GetTowarInfo(Session session)
        {
            this.session = session;
        }

        public Towar GetTowar(string ean)
        {
            var towaryModule = TowaryModule.GetInstance(session);

            return towaryModule.Towary.WgEAN[ean].GetFirst();
        }
    }
}
