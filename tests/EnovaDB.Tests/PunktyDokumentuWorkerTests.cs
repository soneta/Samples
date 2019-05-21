using EnovaDB.Punktacja;
using NUnit.Framework;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Magazyny;
using Soneta.Test;
using Soneta.Towary;

namespace EnovaDB.Integration.Tests
{
    class PunktyDokumentuWorkerTests : DbTransactionTestBase
    {
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(6, ExpectedResult = 6)]
        public int SumaPunktow_Edycja_PunktyPrzeliczaneWedlugStandardowegoMnoznika(int iloscPozycji)
        {
            var dokument = DodajDokumentZPozycjami(iloscPozycji);
            return new PunktyDokumentuWorker {Dokument = dokument}.SumaPunktów;
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 2)]
        [TestCase(6, ExpectedResult = 12)]
        public int SumaPunktow_Edycja_PunktyPrzeliczaneWedlugZmodyfikowanegoMnoznika(int iloscPozycji)
        {
            InConfigTransaction(() =>
                        ConfigEditSession.GetPunktacja().DefPunkty.Standardowa.Mnoznik = 2);
            SaveDisposeConfig();
            var dokument = DodajDokumentZPozycjami(iloscPozycji);
            return new PunktyDokumentuWorker { Dokument = dokument }.SumaPunktów;
        }

        [TestCase(1, ExpectedResult = 0)]
        [TestCase(6, ExpectedResult = 5)]
        public int SumaPunktow_Usuwanie_PunktyPrzeliczaneWedlugStandardowegoMnoznika(int iloscPozycji)
        {
            var dokument = DodajDokumentZPozycjami(iloscPozycji);
            UsunPierwszaPozycje(dokument);
            return new PunktyDokumentuWorker { Dokument = dokument }.SumaPunktów;
        }

        [TestCase(1, ExpectedResult = 0)]
        [TestCase(6, ExpectedResult = 10)]
        public int SumaPunktow_Usuwanie_PunktyPrzeliczaneWedlugZmodyfikowanegoMnoznika(int iloscPozycji)
        {
            InConfigTransaction(() =>
                ConfigEditSession.GetPunktacja().DefPunkty.Standardowa.Mnoznik = 2);
            SaveDisposeConfig();
            var dokument = DodajDokumentZPozycjami(iloscPozycji);
            UsunPierwszaPozycje(dokument);
            return new PunktyDokumentuWorker { Dokument = dokument }.SumaPunktów;
        }

        private void UsunPierwszaPozycje(DokumentHandlowy dokument)
        {
            InUITransaction(()=> dokument.PozycjaWgIdent(1).Delete());
        }

        private DokumentHandlowy DodajDokumentZPozycjami(int iloscPozycji)
        {
            var dokument = DodajDokument();
            for (int i = 0; i < iloscPozycji; i++)
            {
                DodajPozycje(dokument);
            }
            return dokument;
        }

        private void DodajPozycje(DokumentHandlowy doc)
        {
            InUITransaction(() =>
            {
                var pos = new PozycjaDokHandlowego(doc);
                Session.GetHandel().PozycjeDokHan.AddRow(pos);
                pos.Towar = Session.GetTowary().Towary.WgKodu["Bikini"];
            });
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
    }
}
