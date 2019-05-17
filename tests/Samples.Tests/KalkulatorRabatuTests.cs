using NUnit.Framework;
using Soneta.Types;

namespace Samples.Tests
{
    class KalkulatorRabatuTests
    {
        [TestCase(10, false, ExpectedResult = 0)]
        [TestCase(99, false, ExpectedResult = 0)]
        [TestCase(100, false, ExpectedResult = 0.2)]
        [TestCase(499, false, ExpectedResult = 0.2)]
        [TestCase(500, false, ExpectedResult = 0.5)]
        [TestCase(999, false, ExpectedResult = 0.5)]
        [TestCase(1000, false, ExpectedResult = 1)]
        // Lojalny kontrahent
        [TestCase(10, true, ExpectedResult = 0)]
        [TestCase(49, true, ExpectedResult = 0)]
        [TestCase(50, true, ExpectedResult = 0.2)]
        [TestCase(249, true, ExpectedResult = 0.2)]
        [TestCase(250, true, ExpectedResult = 0.5)]
        [TestCase(499, true, ExpectedResult = 0.5)]
        [TestCase(500, true, ExpectedResult = 1)]
        [TestCase(999, true, ExpectedResult = 1)]
        [TestCase(1000, true, ExpectedResult = 1)]
        public decimal Policz(decimal wartosc, bool lojalny) => KalkulatorRabatu.PoliczRabat( () => wartosc, () => lojalny);

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
