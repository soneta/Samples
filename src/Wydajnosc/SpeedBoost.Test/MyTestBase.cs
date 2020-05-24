using NUnit.Framework;
using Soneta.CRM;
using Soneta.Kasa;
using Soneta.Test;
using Soneta.Types;

namespace SpeedBoost.Test
{
    class MyTestBase : TestBase
    {
        [SetUp]
        public void PrepareData()
        {
            var kasa = Session.GetKasa();
            var ewidencjasp = (RachunekBankowyFirmy)kasa.EwidencjeSP.RachunekBankowy;
            var przelewy = kasa.Przelewy.WgEwidencjaSP[ewidencjasp];
            InUITransaction(() =>
            {
                if (przelewy.Any)
                {
                    foreach (var przelew in przelewy)
                    {
                        przelew.Exported = false;
                        przelew.Bufor = true;
                        przelew.Delete();
                    }
                }
                var abc = Session.GetCRM().Kontrahenci.WgKodu["Abc"];
                var drynda = Session.GetCRM().Kontrahenci.WgKodu["Drynda"];

                for (int i = 0; i < 30; i++)
                {
                    var p = new Przelew(ewidencjasp);
                    Session.AddRow(p);
                    p.Kwota = new Currency(123.33m);
                    p.Data = new Date(2020, 1, 1);
                    p.Podmiot = i < 10 ? abc : drynda;
                    p.Tytulem1 = "Przelew " + i;
                    p.NazwaZleceniodawcy1 = "Soneta sp. z o.o.";
                    p.RachunekOdbiorcy.Numer = "1234567890";
                }
            });
            SaveDispose();
        }
    }
}
