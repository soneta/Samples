using System.Collections.Generic;
using System.Linq;
using Samples;
using Soneta.Handel;
using Soneta.Towary;
using Soneta.Types;

[assembly: Soneta.Business.Service(typeof(IZmianaDokumentuHandlowego), typeof(ZmianaDokumentuHandlowego))]

namespace Samples
{
  public class ZmianaDokumentuHandlowego : IZmianaDokumentuHandlowego
  {
    public ILogika Logika { get; set; } = new Domyslna();

    public void ZmianaStanu( ZmianaStanuDokumentuHandlowegoArgs args )
    {
      if (args.PrzedZmianą &&
          args.NowyStan == StanDokumentuHandlowego.Zatwierdzony)
      {
        Logika.DodajTransport( args, Logika.PoliczRabat( args ) );
      }
    }

    #region Niezaimplementowane metody

    public void ZmianaWartości( ZmianaDokumentuHandlowegoArgs args )
    {
    }

    public void ZmianaPozycji( ZmianaPozycjiDokumentuArgs args )
    {
    }

    public void WyliczenieCenyPozycji( WyliczenieCenyPozycjiDokumentuArgs args )
    {
    }

    public void ZmianaPłatności( ZmianaDokumentuHandlowegoArgs args )
    {
    }

    public void Zatwierdzanie( ZmianaDokumentuHandlowegoArgs args )
    {
    }

    public void Zatwierdzony( ZmianaDokumentuHandlowegoArgs args )
    {
    }

    #endregion

    class Domyslna : ILogika
    {
      decimal ILogika.PoliczRabat( ZmianaStanuDokumentuHandlowegoArgs args ) =>
        KalkulatorRabatu.PoliczRabat(
          () => args.Dokument.Suma.Netto,
          () => KalkulatorRabatu.LojalnyKontrahent(
            () => PobierzDokumenty( args.Dokument ).Select( x => x.Data ) ) );

      void ILogika.DodajTransport(
        ZmianaStanuDokumentuHandlowegoArgs args,
        decimal rabat )
      {
        var pos = new PozycjaDokHandlowego( args.Dokument );
        args.Dokument.Session.GetHandel().PozycjeDokHan.AddRow( pos );

        pos.Towar = args.Dokument.Session.GetTowary().Towary
          .WgKodu[ "TRANSPORT" ];
        pos.Rabat = new Percent( rabat );
      }

      IEnumerable<DokumentHandlowy> PobierzDokumenty( DokumentHandlowy dokument ) =>
        dokument.Table.WgKontrahent[ dokument.Kontrahent ];
    }

    public interface ILogika
    {
      void DodajTransport(
        ZmianaStanuDokumentuHandlowegoArgs args,
        decimal rabat );

      decimal PoliczRabat( ZmianaStanuDokumentuHandlowegoArgs args );
    }
  }
}
