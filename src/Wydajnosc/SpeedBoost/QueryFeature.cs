using System.Collections.Generic;
using Soneta.Business;
using Soneta.Kasa;

namespace SpeedBoost
{
    public class QueryFeature
    {
        public class FeatureResult
        {
            public int PrzelewID { get; set; }
            public string Cecha { get; set; }
        }

        public QueryFeature(Session session) => Session = session;

        public Session Session { get; }

        public List<FeatureResult> Cecha()
        {
            //Pobranie cechy przelewu o nazwie "Cecha"

            var przelewy = new Query.Table(nameof(Przelewy));

            przelewy += new Query.Field(nameof(Przelew.ID))
            { PropertyName = nameof(FeatureResult.PrzelewID) };
            przelewy += new Query.Field($"{nameof(Row.Features)}.{nameof(QueryFeature.Cecha)}")
            { PropertyName = nameof(FeatureResult.Cecha) };

            return new List<FeatureResult>(Session.Execute<FeatureResult>(przelewy));
        }
    }
}
