using System.Linq;
using NUnit.Framework;
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
    }
}
