using System.Linq;
using NUnit.Framework;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Magazyny;
using Soneta.Test;
using Soneta.Towary;
using Soneta.Types;

namespace Samples.Integration.Tests
{
    [SetUpFixture]
    public class Config
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            _ = new KalkulatorRabatu();
        }
    }

    class ZmianaDokumentuHandlowegoTests : DbTransactionTestBase
    {
        [Test]
        public void Policz_NielojalnyKontrahentIWartoscPonizej100Zl_BrakRabatu()
        {
            var doc = DodajDokument();
            DodajPozycje(doc, new Currency(99m));
            InUITransaction(()=> doc.Stan = StanDokumentuHandlowego.Zatwierdzony);
            Assert.AreEqual(new Percent(0m), doc.Pozycje.Last().Rabat);
        }

        [Test]
        public void Policz_NielojalnyKontrahentIWartoscPonizej500Zl_Rabat20Procent()
        {
            var doc = DodajDokument();
            DodajPozycje(doc, new Currency(499m));
            InUITransaction(() => doc.Stan = StanDokumentuHandlowego.Zatwierdzony);
            Assert.AreEqual(new Percent(0.2m), doc.Pozycje.Last().Rabat);
        }

        [Test]
        public void Policz_NielojalnyKontrahentIWartoscPonizej1000Zl_Rabat50Procent()
        {
            var doc = DodajDokument();
            DodajPozycje(doc, new Currency(999m));
            InUITransaction(() => doc.Stan = StanDokumentuHandlowego.Zatwierdzony);
            Assert.AreEqual(new Percent(0.5m), doc.Pozycje.Last().Rabat);
        }

        [Test]
        public void Policz_NielojalnyKontrahentIWartoscPowyzej1000Zl_WysylkaGratis()
        {
            var doc = DodajDokument();
            DodajPozycje(doc, new Currency(1100m));
            InUITransaction(() => doc.Stan = StanDokumentuHandlowego.Zatwierdzony);
            Assert.AreEqual(new Percent(1m), doc.Pozycje.Last().Rabat);
        }

        [TestCase(49, ExpectedResult = 0)]
        [TestCase(50, ExpectedResult = 0.2)]
        [TestCase(249, ExpectedResult = 0.2)]
        [TestCase(250, ExpectedResult = 0.5)]
        [TestCase(499, ExpectedResult = 0.5)]
        [TestCase(500, ExpectedResult = 1)]
        [TestCase(999, ExpectedResult = 1)]
        [TestCase(1500, ExpectedResult = 1)]
        public decimal Policz_LojalnyKontrahent(decimal wartosc)
        {
            Dodaj4Dokumenty();
            var doc = DodajDokument();
            DodajPozycje(doc, new Currency(wartosc));
            InUITransaction(() => doc.Stan = StanDokumentuHandlowego.Zatwierdzony);
            return doc.Pozycje.Last().Rabat;
        }

        private void Dodaj4Dokumenty()
        {
            DodajDokument();
            DodajDokument();
            DodajDokument();
            DodajDokument();
        }

        private DokumentHandlowy DodajDokument()
        {
            DokumentHandlowy doc = null;

            InUITransaction(() =>
            {
                doc = new DokumentHandlowy
                {
                    Definicja = Session.GetHandel().DefDokHandlowych.WgSymbolu["ZO"]
                };
                Session.GetHandel().DokHandlowe.AddRow(doc);
                doc.Magazyn = Session.GetMagazyny().Magazyny.Firma;
                doc.Kontrahent = Session.GetCRM().Kontrahenci.WgKodu["ABC"];
            });
            return doc;
        }

        private void DodajPozycje(DokumentHandlowy doc, Currency wartosc)
        {
            InUITransaction(() =>
            {
                var pos = new PozycjaDokHandlowego(doc);
                Session.GetHandel().PozycjeDokHan.AddRow(pos);

                pos.Towar = Session.GetTowary().Towary.WgKodu["Bikini"];
                pos.WartoscCy = wartosc;
            });
        }
    }
}
