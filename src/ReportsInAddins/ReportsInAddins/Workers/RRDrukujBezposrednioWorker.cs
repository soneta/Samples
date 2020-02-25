using System.Drawing.Printing;
using System.IO;
using PdfiumViewer;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Handel;
using Soneta.Tools;

[assembly: Worker(typeof(ReportsInAddins.Workers.RRDrukujBezposrednioWorker), typeof(DokumentHandlowy))]

namespace ReportsInAddins.Workers
{
    class RRDrukujBezposrednioWorker
    {
        [Action("ReportResult/Drukuj bezpośrednio",
            Target = ActionTarget.Menu,
            Mode = ActionMode.SingleSession | ActionMode.OnlyForm)]
        public object Drukuj(Context cx)
        {

            var parametry = new ParametryWydrukuDokumentu(cx) // tworzymy obiekt klasy parametrow, z ktorej korzysta wydruk
            {
                Oryginał = false,
                IloscKopii = 1,
                Duplikat = true
            };

            cx[typeof(ParametryWydrukuDokumentu)] = parametry;

            var reportResult = new ReportResult
            {
                Context = cx,
                DataType = typeof(DokumentHandlowy),
                TemplateFileSource =
                    AspxSource.Local, //domyslnie local, jesli aspx przechowywany w storage'u to ustawiamy storage
                TemplateFileName = "handel/sprzedaz.aspx", //wpisujemy sciezke do pliku
                Format = ReportResultFormat.PDF, //format wykonanego wydruku
                OutputHandler = DrukujPlik //podpiety handler, w ktorym wykonana bedzie dodatkowa akcja
            };

            return reportResult;
        }

        private object DrukujPlik(Stream stream)
        {
            var tg = new TempFileNameGenerator();
            var name = tg.GetFileName("Sprzedaz.pdf");
            var temp = "C:/!Temp";
            var path = Path.Combine(temp, name);
            using (var file = File.Create(path))
            {
                CoreTools.StreamCopy(stream, file);
                file.Flush();
            }

            var printerSettings = new PrinterSettings
            {
                PrinterName = "Microsoft Print to PDF",
                Copies = 1,
            };

            var pageSettings = new PageSettings(printerSettings)
            {
                Margins = new Margins(0, 0, 0, 0),
            };
            foreach (PaperSize paperSize in printerSettings.PaperSizes)
            {
                if (paperSize.Kind == PaperKind.A4)
                {
                    pageSettings.PaperSize = paperSize;
                    break;
                }
            }

            using (var document = PdfDocument.Load(path))
            {
                using (var printDocument = document.CreatePrintDocument())
                {
                    printDocument.PrinterSettings = printerSettings;
                    printDocument.DefaultPageSettings = pageSettings;
                    printDocument.PrintController = new StandardPrintController();
                    printDocument.Print();
                }
            }

            File.Delete(path);

            return $"Raport został wysłany na drukarkę: {printerSettings.PrinterName}";
        }
    }

}
