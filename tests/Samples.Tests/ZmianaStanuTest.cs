// =============================================================================
// <copyright file="ZmianaStanuTest.cs" company="Soneta sp. z o.o.">
//     Copyright (c) 2019 Soneta sp. z o.o. All rights reserved.
// </copyright>
// =============================================================================

using NSubstitute;
using NUnit.Framework;
using Soneta.Handel;

namespace Samples.Tests
{
  class ZmianaStanuTest
  {
    ZmianaDokumentuHandlowego _zmianaDokumentu;

    [Test]
    [Description( "Nigdy nie oczekuje się reakcji po zmianie stanu." )]
    public void Ignoruj(
      [Values(
        StanDokumentuHandlowego.Zatwierdzony,
        StanDokumentuHandlowego.Anulowany,
        StanDokumentuHandlowego.Bufor,
        StanDokumentuHandlowego.Zablokowany )]
      StanDokumentuHandlowego stan,
      [Values( false )] bool przed )
    {
      var args =
        new ZmianaStanuDokumentuHandlowegoArgs( default, stan, przed );
      _zmianaDokumentu.ZmianaStanu( args );

      _zmianaDokumentu.Logika.Received( 0 )
        .PoliczRabat( args );
      _zmianaDokumentu.Logika.Received( 0 )
        .DodajTransport( args, Arg.Any<decimal>() );
    }

    [Test]
    [Description(
      "Przypadki, kiedy reakcja nie jest oczekiwana przed zmianą stanu" )]
    public void NieReaguj(
      [Values(
        StanDokumentuHandlowego.Anulowany,
        StanDokumentuHandlowego.Bufor,
        StanDokumentuHandlowego.Zablokowany )]
      StanDokumentuHandlowego stan,
      [Values( false )] bool przed )
    {
      var args =
        new ZmianaStanuDokumentuHandlowegoArgs( default, stan, przed );
      _zmianaDokumentu.ZmianaStanu( args );

      _zmianaDokumentu.Logika.Received( 0 )
        .PoliczRabat( args );
      _zmianaDokumentu.Logika.Received( 0 )
        .DodajTransport( args, Arg.Any<decimal>() );
    }

    [Test]
    [Description(
      "Przypadek, kiedy reakcja jest oczekiwana przed zatwierdzeniem" )]
    public void Reaguj(
      [Values( StanDokumentuHandlowego.Zatwierdzony )]
      StanDokumentuHandlowego stan,
      [Values( true )] bool przed )
    {
      var args =
        new ZmianaStanuDokumentuHandlowegoArgs( default, stan, przed );
      _zmianaDokumentu.ZmianaStanu( args );

      _zmianaDokumentu.Logika.Received( 1 )
        .PoliczRabat( args );
      _zmianaDokumentu.Logika.Received( 1 )
        .DodajTransport( args, Arg.Any<decimal>() );
    }

    [OneTimeSetUp]
    public void SetupOnce() =>
      _zmianaDokumentu = new ZmianaDokumentuHandlowego
      {
        Logika = Substitute.For<ZmianaDokumentuHandlowego.ILogika>()
      };
  }
}
