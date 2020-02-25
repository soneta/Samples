using System.IO;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Handel;
using Soneta.Tools;

[assembly: Worker(typeof(ReportsInAddins.Workers.RRZapiszWorker), typeof(DokumentHandlowy))]

namespace ReportsInAddins.Workers
{
    class RRZapiszWorker
    {
        [Action("ReportResult/Zapisz PDF",
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

            cx[typeof(ParametryWydrukuDokumentu)] = parametry; // dodanie parametrow do kontekstu

            var reportResult = new ReportResult
            {
                Context = cx,
                DataType = typeof(DokumentHandlowy),
                TemplateFileSource =
                    AspxSource.Local, //domyslnie local, jesli aspx przechowywany w storage'u to ustawiamy storage
                TemplateFileName = "handel/sprzedaz.aspx", //wpisujemy sciezke do pliku
                Format = ReportResultFormat.PDF, //format wykonanego wydruku
                OutputHandler = ZapiszPlik //podpiety handler, w ktorym wykonana bedzie dodatkowa akcja
            };

            return reportResult;
        }

        private static object ZapiszPlik(Stream stream)
        {
            var nameGenerator = new TempFileNameGenerator();
            var name = nameGenerator.GetFileName("Sprzedaz.pdf");
            const string temp = "C:/!Temp";
            var path = Path.Combine(temp, name);

            using (var file = File.Create(path))
            {
                CoreTools.StreamCopy(stream, file);
                file.Flush();
            }

            System.Diagnostics.Process.Start(path);

            return null;
        }

        public static bool IsVisibleDrukuj(DokumentHandlowy dokument)
        {
            return dokument.Kategoria == KategoriaHandlowa.Sprzedaż || dokument.Kategoria == KategoriaHandlowa.KorektaSprzedaży;
        }
    }
}
