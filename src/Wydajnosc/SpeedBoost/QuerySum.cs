using System.Collections.Generic;
using Soneta.Business;
using Soneta.Kasa;
using Soneta.Types;

namespace SpeedBoost
{
    public class QuerySum
    {
        public class SumResult
        {
            public int PodmiotID { get; set; }
            public Currency Kwota { get; set; }
        }

        public QuerySum(Session session) => Session = session;

        public Session Session { get; }

        public List<SumResult> Sum()
        {
            //select Podmiot as PodmiotId, sum(KwotaValue) as Kwota 
            //from Przelewy group by Podmiot
            return new List<SumResult>();
        }
    }
}
