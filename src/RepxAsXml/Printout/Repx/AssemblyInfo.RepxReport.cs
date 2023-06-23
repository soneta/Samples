using Soneta.Business;
using Soneta.Handel;

[assembly: DxReport(
  typeof(DokumentHandlowy),
  "Faktura sprzedaży", "Sprzedaz2.repx",
  KategoriaHandlowa.Sprzedaż,
  KategoriaHandlowa.KorektaSprzedaży,
  "HandelKorekta-Sprzedaz",
  Priority = 1)]
