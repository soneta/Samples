using System.IO;
using System.Text;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Kasa;
using Soneta.Types;

namespace SpeedBoost
{
    //****************************************************************
    //   EksportPrzelewowBad po refaktoryzacji
    //****************************************************************
    public class EksportPrzelewowGood
    {
        private readonly int _rok = 2020;
        private readonly string _nazwapliku = Path.GetTempFileName();

        public EksportPrzelewowGood(Context context) => Context = context;

        public Context Context { get; }

        public string Eksportuj()
        {
            var sb = new StringBuilder();

            using (var session = Context.Login.CreateSession(false, false, "Eksport przelewów"))
            {
                var st = new SubTable(session.GetKasa().Przelewy.WgData, new FromTo(new Date(_rok, 1, 1), new Date(_rok, 12, 31)));
                        
                using (var t = session.Logout(true))
                {
                    foreach (PrzelewBase przelew in st)
                    {
                        if (przelew.Podmiot is Kontrahent k && k.KodKraju == "PL")
                        {
                            sb.AppendFormat($"{przelew.RachunekOdbiorcy.ToPlain()};{przelew.NazwaOdbiorcy1};{przelew.Kwota};{przelew.Opis}\n");
                            przelew.Exported = true;
                        }
                    }
                    t.Commit();
                }
                session.Save();
            }

            //Tylko do celów warsztatów, wg zasady SRP poniższy kod powinien znaleźć się w oddzielnej klasie
            File.AppendAllText(_nazwapliku, sb.ToString());
            return File.ReadAllText(_nazwapliku);
        }
    }
}
