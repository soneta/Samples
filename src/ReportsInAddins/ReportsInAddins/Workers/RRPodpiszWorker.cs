using System;
using System.IO;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Handel;
using Soneta.Tools;

[assembly: Worker(typeof(ReportsInAddins.Workers.RRPodpiszWorker), typeof(DokumentHandlowy))]

namespace ReportsInAddins.Workers
{
    class RRPodpiszWorker
    {
        [Context]
        public DokumentHandlowy Dokument { get; set; }

        [Action("ReportResult/Podpisz PDF",
            Target = ActionTarget.Menu,
            Mode = ActionMode.SingleSession | ActionMode.OnlyForm)]
        public object Drukuj(Context cx)
        {
            var reportResult = new ReportResult
            {
                Context = cx,
                Format = ReportResultFormat.PDF,
                TemplateFileName = "handel/sprzedaz.aspx",
                Sign = true,
                VisibleSignature = true,
                Encrypt = "GeekOut2018",
                DataType = Dokument.GetType(),
                OutputHandler = ZapiszPlik,
            };


            return reportResult;
        }

        private object ZapiszPlik(Stream stream)
        {
            var kodKontrahenta = Dokument.Kontrahent.Kod;
            var numerFaktury = String.Join("", Dokument.NumerPelnyZapisany.Split(Path.GetInvalidFileNameChars()));
            var timeStamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var rozszerzenie = ".pdf";

            var nazwaPliku = $"{kodKontrahenta}_{numerFaktury}_{timeStamp}{rozszerzenie}";

            var folder = "C:\\Wydruki";
            Directory.CreateDirectory(folder);
            
            var path = Path.Combine(folder, nazwaPliku);

            using (var file = File.Create(path))
            {
                CoreTools.StreamCopy(stream, file);
                file.Flush();
            }

            return $"Utworzono: {path}";
        }

        public static bool IsVisibleDrukuj(DokumentHandlowy dokument)
        {
            return dokument.Kategoria == KategoriaHandlowa.Sprzedaż || dokument.Kategoria == KategoriaHandlowa.KorektaSprzedaży;
        }
    }
}
