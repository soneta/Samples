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
            //ekwiwalent w SQL:
            //
            //select p.NazwaOdbiorcy1 as Odbiorca, p.KwotaValue as Kwota, o.Name as Operator
            //from Przelewy p 
            //inner join ChangeInfos c on c.SourceGuid = p.Guid and c.Type = 1 --Created
            //inner join Operators o on o.ID = c.Operator

            var przelewy = new Query.Table(nameof(Przelewy));
            var changeInfos = new Query.InnerJoin(nameof(ChangeInfos),
                new FieldCondition.Equal(nameof(ChangeInfo.SourceGuid), przelewy, nameof(PrzelewBase.Guid))
                & new FieldCondition.Equal(nameof(ChangeInfo.Type), ChangeInfoType.Created));
            var operators = new Query.InnerJoin(nameof(Operators),
                new FieldCondition.Equal(nameof(Operator.ID), changeInfos, nameof(ChangeInfo.Operator)));

            przelewy += new Query.Field(nameof(PrzelewBase.NazwaOdbiorcy1)) { PropertyName = nameof(JoinResult.Odbiorca) };
            przelewy += new Query.Field(nameof(PrzelewBase.Kwota)) { PropertyName = nameof(JoinResult.Kwota) };
            operators += new Query.Field(nameof(Operator.Name)) { PropertyName = nameof(JoinResult.Operator) };

            changeInfos.Add(operators);
            przelewy.Add(changeInfos);

            return new List<JoinResult>(Session.Execute<JoinResult>(przelewy));
        }
    }
}
