using System.Linq;
using NUnit.Framework;
using Soneta.Business;
using Soneta.Business.Db;
using Soneta.Kasa;
using Soneta.Types;

namespace SpeedBoost.Test
{
    class TestQuery : MyTestBase
    { 
        [Test]
        public void TestSum()
        {
            var total = new QuerySum(Session).Sum();
            Assert.AreEqual(new Currency(1233.30m), total.First(p => p.PodmiotID == 2).Kwota);
            Assert.AreEqual(new Currency(2466.60m), total.First(p => p.PodmiotID == 6).Kwota);
        }

        [Test]
        public void TestJoin()
        {
            var result = new QueryJoin(Session).Join();
            Assert.AreEqual(30, result.Count);
        }

        [Test]
        public void TestFeature()
        {
            if (ConfigSession.GetBusiness().FeatureDefs.ByName[nameof(Przelewy), nameof(QueryFeature.Cecha)] == null)
            {
                InUIConfigTransaction(() =>
                {
                    var feature = new FeatureDefinition(nameof(Przelewy));
                    ConfigSession.GetBusiness().FeatureDefs.AddRow(feature);
                    feature.Name = nameof(QueryFeature.Cecha);
                });
                SaveDisposeConfig();
            }

            var kasa = Session.GetKasa();
            var ewidencjasp = (RachunekBankowyFirmy)kasa.EwidencjeSP.RachunekBankowy;
            var przelewy = kasa.Przelewy.WgEwidencjaSP[ewidencjasp];
            InUITransaction(() =>
            {
                foreach (var przelew in przelewy)
                {
                    przelew.Features[nameof(QueryFeature.Cecha)] = "Cecha dla ID=" + przelew.ID;
                }
            });
            SaveDispose();

            new QueryFeature(Session).Cecha().ForEach(f => Assert.AreEqual("Cecha dla ID=" + f.PrzelewID, f.Cecha));
        }
    }
}
