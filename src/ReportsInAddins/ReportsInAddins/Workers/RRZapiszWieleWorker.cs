using System;
using System.IO;
using Soneta.Business;
using Soneta.Business.App;
using Soneta.Business.UI;
using Soneta.Core;
using Soneta.Handel;

[assembly: Worker(typeof(ReportsInAddins.Workers.RRZapiszWieleWorker), typeof(DokHandlowe))]

namespace ReportsInAddins.Workers
{
    class RRZapiszWieleWorker
    {
        [Context]
        public DokumentHandlowy[] Dokumenty { get; set; }

        [Action("ReportResult/Zapisz wiele PDF",
            Target = ActionTarget.Menu,
            Mode = ActionMode.SingleSession | ActionMode.OnlyTable)]
        public object Drukuj(Context cx)
        {
            var parametry = new ParametryWydrukuDokumentu(cx) // tworzymy obiekt klasy parametrow, z ktorej korzysta wydruk
            {
                Oryginał = false,
                IloscKopii = 1,
                Duplikat = true
            };

            cx.Set(parametry);

            int licznik = 0;
            foreach (var dokument in Dokumenty)
            {
                cx.Set(dokument);
                licznik++;
                DrukujJeden(cx, dokument);
            }

            return null;
        }

        private void DrukujJeden(Context cx, DokumentHandlowy dokument)
        {

            var kodKontrahenta = dokument.Kontrahent.Kod;
            var numerFaktury = String.Join("", dokument.NumerPelnyZapisany.Split(Path.GetInvalidFileNameChars()));
            var timeStamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var rozszerzenie = ".pdf";

            var nazwaPliku = $"{kodKontrahenta}_{numerFaktury}_{timeStamp}{rozszerzenie}";

            var folder = "C:\\Wydruki";
            Directory.CreateDirectory(folder);

            var path = Path.Combine(folder, nazwaPliku);

            var reportResult = new Soneta.Business.UI.ReportResult()
            {
                Context = cx,
                DataType = dokument.GetType(),
                TemplateFileName = @"handel/sprzedaz.aspx",
                Format = Soneta.Business.UI.ReportResultFormat.PDF,
            };

            BusApplication.Instance.GetService(out IReportService service);

            using (var stream = service.GenerateReport(reportResult))
            {
                using (var file = System.IO.File.Create(path))
                {
                    Soneta.Tools.CoreTools.StreamCopy(stream, file);
                    file.Flush();
                }
            }
        }
    }
}
