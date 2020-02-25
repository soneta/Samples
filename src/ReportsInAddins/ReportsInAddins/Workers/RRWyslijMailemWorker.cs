using System.IO;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Core;
using Soneta.Handel;

[assembly: Worker(typeof(ReportsInAddins.Workers.RRWyslijMailemWorker), typeof(DokumentHandlowy))]

namespace ReportsInAddins.Workers
{
    class RRWyslijMailemWorker
    {
        [Context]
        public DokumentHandlowy Dokument { get; set; }

        [Action("ReportResult/Wyślij mailem",
            Target = ActionTarget.Menu,
            Mode = ActionMode.SingleSession | ActionMode.OnlyForm)]
        public object Drukuj(Context cx)
        {
            var parametry = new ParametryWydrukuDokumentu(cx)
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
                    AspxSource.Local, 
                TemplateFileName = "handel/sprzedaz.aspx", 
                Format = ReportResultFormat.PDF, 
                OutputHandler = WyslijPlik
            };

            return reportResult;

        }

        private object WyslijPlik(Stream stream)
        {
            var adres = Dokument.Kontrahent.EMAIL;

            if (!EnovaMail.TestMail(adres))
                return "Niepowodzenie. Nieprawny adres email.";
            var enovaMail = new EnovaMail(Dokument.Session);
            enovaMail.AddAttachment("Zalacznik.pdf", stream);
            enovaMail.AddTo(adres);
            enovaMail.AddSubject("Faktura");
            enovaMail.AddBody("Faktura jest w załączniku");

            enovaMail.SendMail();

            return $"Wysłano mail z fakturą na adres: {adres}";
        }

        public static bool IsVisibleDrukuj(DokumentHandlowy dokument)
        {
            return dokument.Kategoria == KategoriaHandlowa.Sprzedaż || dokument.Kategoria == KategoriaHandlowa.KorektaSprzedaży;
        }
    }
}
