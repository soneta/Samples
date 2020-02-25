using System;
using System.Drawing.Text;
using System.IO;
using Soneta.Business;
using Soneta.Business.App;
using Soneta.Business.Db;
using Soneta.Business.UI;
using Soneta.Core;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Tools;

[assembly: Worker(typeof(ReportsInAddins.Workers.RRComboWorker), typeof(DokumentHandlowy))]

namespace ReportsInAddins.Workers
{
    class RRComboWorker
    {

        [Context]
        public DokumentHandlowy Dokument { get; set; }

        [Action("ReportResult/Zapisz, wyślij, załącz",
            Target = ActionTarget.Menu,
            Mode = ActionMode.SingleSession | ActionMode.OnlyForm)]
        public object Drukuj(Context cx)
        {
            var parametry = UtworzParametry(cx);
            cx.Set(parametry);

            var reportResult = UtworzReportResult(cx);

            BusApplication.Instance.GetService(out IReportService service);

            string nazwaPliku = GenrujNazwePliku();

            using (var stream = service.GenerateReport(reportResult))
            {
                DodajZalacznik(cx, nazwaPliku, stream);

                WyslijEmail(stream);

                ZapiszNaDysku(nazwaPliku, stream);
            }

            return "Faktura została przekaza we wszystkie miejsca";
        }

        private static void ZapiszNaDysku(string nazwaPliku, Stream stream)
        {
            stream.Seek(0L, SeekOrigin.Begin);

            var folder = "C:\\Wydruki";
            Directory.CreateDirectory(folder);

            var path = Path.Combine(folder, nazwaPliku);

            using (var file = File.Create(path))
            {
                CoreTools.StreamCopy(stream, file);
                file.Flush();
            }
        }

        private void WyslijEmail(Stream stream)
        {
            stream.Seek(0L, SeekOrigin.Begin);

            var adres = Dokument.Kontrahent.EMAIL;

            if (!EnovaMail.TestMail(adres))
                return;
            var enovaMail = new EnovaMail(Dokument.Session);
            enovaMail.AddAttachment("Zalacznik.pdf", stream);
            enovaMail.AddTo(adres);
            enovaMail.AddSubject("Faktura");
            enovaMail.AddBody("Faktura jest w załączniku");

            enovaMail.SendMail();
        }

        private void DodajZalacznik(Context cx, string nazwaPliku, Stream stream)
        {
            stream.Seek(0L, SeekOrigin.Begin);

            using (var session = cx.Login.CreateSession(false, false))
            {
                using (var transaction = session.Logout(true))
                {
                    var kontrahent = CRMModule.GetInstance(session).Kontrahenci[Dokument.Kontrahent.Guid];
                    var zalacznik = new Attachment(kontrahent, AttachmentType.Attachments)
                    {
                        Name = nazwaPliku
                    };

                    var bm = BusinessModule.GetInstance(session);
                    bm.Attachments.AddRow(zalacznik);
                    zalacznik.LoadFromStream(stream);


                    transaction.Commit();
                }
                session.Save();
            }
        }

        private string GenrujNazwePliku()
        {
            var kodKontrahenta = Dokument.Kontrahent.Kod;
            var numerFaktury = String.Join("", Dokument.NumerPelnyZapisany.Split(Path.GetInvalidFileNameChars()));
            var timeStamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var rozszerzenie = ".pdf";

            var nazwaPliku = $"{kodKontrahenta}_{numerFaktury}_{timeStamp}{rozszerzenie}";
            return nazwaPliku;
        }

        private ReportResult UtworzReportResult(Context cx)
        {
            return new ReportResult()
            {
                Context = cx,
                DataType = Dokument.GetType(),
                TemplateFileName = @"handel/sprzedaz.aspx",
                Format = Soneta.Business.UI.ReportResultFormat.PDF,
            };
        }

        private static ParametryWydrukuDokumentu UtworzParametry(Context cx)
        {
            return new ParametryWydrukuDokumentu(cx)
            {
                Oryginał = true,
                IloscKopii = 0,
                Duplikat = false
            };
        }
    }
}
