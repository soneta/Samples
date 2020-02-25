using System;
using System.IO;
using Soneta.Business;
using Soneta.Business.Db;
using Soneta.Business.UI;
using Soneta.Core;
using Soneta.CRM;
using Soneta.Handel;

[assembly: Worker(typeof(ReportsInAddins.Workers.RRDoZalacznikaWorker), typeof(DokumentHandlowy))]

namespace ReportsInAddins.Workers
{
    class RRDoZalacznikaWorker
    {

        [Context]
        public DokumentHandlowy Dokument { get; set; }

        private Context context;

        [Action("ReportResult/Zapisz do załącznika",
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

            context = cx;

            var reportResult = new ReportResult
            {
                Context = cx,
                DataType = typeof(DokumentHandlowy),
                TemplateFileSource =
                    AspxSource.Local,
                TemplateFileName = "handel/sprzedaz.aspx",
                Format = ReportResultFormat.PDF,
                OutputHandler = ZapiszPlik
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

            using (var session = context.Login.CreateSession(false,false))
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

            return $"Dodano fakturę jako załącznik dla {Dokument.Kontrahent}";
        }

        public static bool IsVisibleDrukuj(DokumentHandlowy dokument)
        {
            return dokument.Kategoria == KategoriaHandlowa.Sprzedaż || dokument.Kategoria == KategoriaHandlowa.KorektaSprzedaży;
        }
    }
}
