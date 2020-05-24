using System.Collections.Generic;
using Soneta.Business;
using Soneta.Business.App;
using Soneta.Business.Db;
using Soneta.Kasa;
using Soneta.Types;

namespace SpeedBoost
{
    public class QueryJoin
    {
        public class JoinResult
        {
            public string Operator { get; set; }
            public string Odbiorca { get; set; }
            public Currency Kwota { get; set; }
        }

        public QueryJoin(Session session) => Session = session;

        public Session Session { get; }

        public List<JoinResult> Join()
        {
            //select p.NazwaOdbiorcy1 as Odbiorca, p.KwotaValue as Kwota, o.Name as Operator
            //from Przelewy p 
            //inner join ChangeInfos c on c.SourceGuid = p.Guid and c.Type = 1 --Created
            //inner join Operators o on o.ID = c.Operator

            return new List<JoinResult>();
        }
    }
}
