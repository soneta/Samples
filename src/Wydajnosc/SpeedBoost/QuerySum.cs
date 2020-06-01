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
            //ekwiwalent w SQL:
            //
            //select Podmiot as PodmiotId, sum(KwotaValue) as Kwota 
            //from Przelewy group by Podmiot

            var przelewy = new Query.Table(nameof(Przelewy));

            przelewy += new Query.Field(nameof(Przelew.Podmiot))
            { PropertyName = nameof(SumResult.PodmiotID) };
            przelewy += new Query.Sum(nameof(Przelew.Kwota));

            return new List<SumResult>(Session.Execute<SumResult>(przelewy));
        }
    }
}
