using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Handel;

[assembly: Worker(typeof(ReportsInAddins.Workers.RRPodstawowyWorker), typeof(DokumentHandlowy))]

namespace ReportsInAddins.Workers
{
    class RRPodstawowyWorker
    {
        [Action("ReportResult/Domyślna akcja",
            Target = ActionTarget.Menu,
            Mode = ActionMode.SingleSession | ActionMode.OnlyForm)]
        public object Drukuj(Context cx)
        {
            var reportResult = ReportResult.Default;

            reportResult.Context = cx; //kontekst raportu - w kontekście znajduje się już dokument handlowy
            reportResult.DataType = typeof(DokumentHandlowy); //DataType - dla którego będzie wyszukiwany wydruk
            reportResult.Preview = false; //Preview - ustawiamy czy ma byc wykonany od razu podglad czy akcja drukowania
            reportResult.ViewNames = new[] {KategoriaHandlowa.Sprzedaż.ToString(),
                KategoriaHandlowa.KorektaSprzedaży.ToString(), "HandelKorekta-Sprzedaz" }; //ViewNames - dla których będzie wyszukiwany wydruk łącznie z DataType

            return reportResult;
        }

        public static bool IsVisibleDrukuj(DokumentHandlowy dokument)
        {
            return dokument.Kategoria == KategoriaHandlowa.Sprzedaż || dokument.Kategoria == KategoriaHandlowa.KorektaSprzedaży;
        }
    }
}
