using System.Collections.Generic;
using System.Linq;
using Samples;
using Soneta.Handel;
using Soneta.Towary;
using Soneta.Types;

[assembly: Soneta.Business.Service(typeof(IZmianaDokumentuHandlowego), typeof(ZmianaDokumentuHandlowego))]

namespace Samples
{
    class ZmianaDokumentuHandlowego : IZmianaDokumentuHandlowego
    {
        public void ZmianaStanu(ZmianaStanuDokumentuHandlowegoArgs args)
        {
            if (args.PrzedZmianą && args.NowyStan == StanDokumentuHandlowego.Zatwierdzony)
            {
                DodajTransport(args.Dokument, KalkulatorRabatu.PoliczRabat(WartoscDokumentu, Lojalny));
            }

            bool Lojalny() => KalkulatorRabatu.LojalnyKontrahent(() => PobierzDokumenty(args.Dokument).Select(x => x.Data));
            decimal WartoscDokumentu() => args.Dokument.Suma.Netto;
        }

        public IEnumerable<DokumentHandlowy> PobierzDokumenty(DokumentHandlowy dh)
        {
            return dh.Session.GetHandel().DokHandlowe.WgKontrahent[dh.Kontrahent];
        }

        public void DodajTransport(DokumentHandlowy dokument, decimal rabat)
        {
            var pos = new PozycjaDokHandlowego(dokument);
            dokument.Session.GetHandel().PozycjeDokHan.AddRow(pos);
           
            pos.Towar =  dokument.Session.GetTowary().Towary.WgKodu["TRANSPORT"];
            pos.Rabat = new Percent(rabat);
        }

#region Niezaimplementowane metody
        public void ZmianaWartości(ZmianaDokumentuHandlowegoArgs args)
        {
        }

        public void ZmianaPozycji(ZmianaPozycjiDokumentuArgs args)
        {
        }

        public void WyliczenieCenyPozycji(WyliczenieCenyPozycjiDokumentuArgs args)
        {
        }

        public void ZmianaPłatności(ZmianaDokumentuHandlowegoArgs args)
        {
        }

        public void Zatwierdzanie(ZmianaDokumentuHandlowegoArgs args)
        {
        }

        public void Zatwierdzony(ZmianaDokumentuHandlowegoArgs args)
        {
        }
#endregion
    }
}
