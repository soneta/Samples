using System.IO;
using System.Linq;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Kasa;
using Soneta.Tools;

namespace SpeedBoost
{
    //****************************************************************
    //   UWAGA!!! To jest przykład niewłaściwego kodu!!!
    //   Poniższa klasa po refaktoryzacji znajduje się
    //   w pliku EksportPrzelewowGood.cs
    //****************************************************************
    public class EksportPrzelewowBad
    {
        private readonly string _rok = "2020";
        private readonly string _nazwapliku = Path.GetTempFileName();

        public EksportPrzelewowBad(Context context) => Context = context;

        public Context Context { get; }

        private Session Session => Context.Session;

        public string Eksportuj()
        {
            Session.GetKasa()
                .Przelewy.Rows
                .Cast<PrzelewBase>()
                .Where(PoprzedniMiesiac)
                .Where(KontrahentPolski)
                .ToList()
                .ForEach(EksportujPrzelew);
            return File.ReadAllText(_nazwapliku);
        }

        private bool PoprzedniMiesiac(PrzelewBase przelew)
        {
            return przelew.Data.ToString("yyyy") == _rok;
        }

        private bool KontrahentPolski(PrzelewBase przelew)
        {
            var crm = Session.GetCRM();
            var kontrahenciPolscy = crm.Kontrahenci.WgSposobZaplaty
                .CreateView().Cast<Kontrahent>()
                .Where(k => k.KodKraju == "PL").ToArray();
            return kontrahenciPolscy.Contains(przelew.Podmiot);
        }

        private void EksportujPrzelew(PrzelewBase przelew)
        {
            string dane = string.Empty;
            dane += przelew.RachunekOdbiorcy.ToPlain();
            dane += ";";
            dane += przelew.NazwaOdbiorcy1;
            dane += ";";
            dane += przelew.Kwota;
            dane += ";";
            dane += przelew.Opis;
            dane += "\n";
            File.AppendAllText(_nazwapliku, dane);

            var session = Context.Login.CreateSession(false, false);
            using (var t = session.Logout(true))
            {
                var p = session.Get(przelew);
                p.Exported = true;
                t.Commit();
            }
            session.Save();
        }
    }
}
