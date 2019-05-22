using NUnit.Framework;
using Soneta.Types;

namespace Samples.Tests
{
    class KalkulatorRabatuTests
    {
        [TestCase(10, ExpectedResult = 0)]
        [TestCase(99, ExpectedResult = 0)]
        [TestCase(100, ExpectedResult = 0.2)]
        [TestCase(499, ExpectedResult = 0.2)]
        [TestCase(500, ExpectedResult = 0.5)]
        [TestCase(999, ExpectedResult = 0.5)]
        [TestCase(1000, ExpectedResult = 1)]
        public decimal Policz_KontrahentNielojalny(decimal wartosc) => 
            KalkulatorRabatu.PoliczRabat( () => wartosc, () => false);

        [TestCase(10, ExpectedResult = 0)]
        [TestCase(49, ExpectedResult = 0)]
        [TestCase(50, ExpectedResult = 0.2)]
        [TestCase(249, ExpectedResult = 0.2)]
        [TestCase(250, ExpectedResult = 0.5)]
        [TestCase(499, ExpectedResult = 0.5)]
        [TestCase(500, ExpectedResult = 1)]
        [TestCase(999, ExpectedResult = 1)]
        [TestCase(1000, ExpectedResult = 1)]
        public decimal Policz_KontrahentLojalny(decimal wartosc) => 
            KalkulatorRabatu.PoliczRabat(() => wartosc, () => true);

        [Test]
        public void LojalnyKontrahent_5DokumentowZOstatniegoPolrocza_True()
        {
            var result = KalkulatorRabatu.LojalnyKontrahent(() => new[]
            {
                Date.Today,
                Date.Today.AddMonths(-1),
                Date.Today.AddMonths(-2),
                Date.Today.AddMonths(-3),
                Date.Today.AddMonths(-4),
            });
            Assert.IsTrue(result);
        }

        [Test]
        public void LojalnyKontrahent_4DokumentyZOstatniegoPolrocza_False()
        {
            var result = KalkulatorRabatu.LojalnyKontrahent(() => new[]
            {
                Date.Today,
                Date.Today.AddMonths(-1),
                Date.Today.AddMonths(-3),
                Date.Today.AddMonths(-4)
            });
            Assert.IsFalse(result);
        }

        [Test]
        public void LojalnyKontrahent_Powyzej4DokumentowAleTylkoTrzyZOstatniegoPolrocza_False()
        {
            var result = KalkulatorRabatu.LojalnyKontrahent(() => new[]
            {
                Date.Today,
                Date.Today.AddMonths(-4),
                Date.Today.AddMonths(-5),
                Date.Today.AddMonths(-6),
                Date.Today.AddMonths(-7),
                Date.Today.AddMonths(-8),
                Date.Today.AddMonths(-9),
            });
            Assert.IsFalse(result);
        }
    }
}
