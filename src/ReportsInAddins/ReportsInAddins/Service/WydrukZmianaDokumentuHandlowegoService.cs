using Soneta.Business;
using Soneta.Business.App;
using Soneta.Business.UI;
using Soneta.Handel;
using System.IO;
using Soneta.Business.Db;
using Soneta.Core;
using Soneta.CRM;
using Soneta.Tools;
using System;

[assembly: Service(typeof(IZmianaDokumentuHandlowego), typeof(ReportsInAddins.Service.WydrukZmianaDokumentuHandlowegoService))]

namespace ReportsInAddins.Service
{
    class WydrukZmianaDokumentuHandlowegoService : IZmianaDokumentuHandlowego
    {

        public void Zatwierdzony(ZmianaDokumentuHandlowegoArgs args)
        {
            if (args.Dokument.Kategoria != KategoriaHandlowa.Sprzedaż &&
                args.Dokument.Kategoria != KategoriaHandlowa.KorektaSprzedaży) return;

            Context cx = Soneta.Business.Context.Empty.Clone(args.Dokument.Session);

            cx.Set(args.Dokument);

            var parametry = UtworzParametry(cx);
            cx.Set(parametry);

            var reportResult = UtworzReportResult(cx, args.Dokument);

            BusApplication.Instance.GetService(out IReportService service);

            string nazwaPliku = GenrujNazwePliku(args.Dokument);

            using (var stream = service.GenerateReport(reportResult))
            {
                DodajZalacznik(cx, nazwaPliku, stream, args.Dokument);

                WyslijEmail(stream, args.Dokument);

                ZapiszNaDysku(nazwaPliku, stream);
            }

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

        private void WyslijEmail(Stream stream, DokumentHandlowy dokument)
        {
            stream.Seek(0L, SeekOrigin.Begin);

            var adres = dokument.Kontrahent.EMAIL;

            if (!EnovaMail.TestMail(adres))
                return;
            var enovaMail = new EnovaMail(dokument.Session);
            enovaMail.AddAttachment("Zalacznik.pdf", stream);
            enovaMail.AddTo(adres);
            enovaMail.AddSubject("Faktura");
            enovaMail.AddBody("Faktura jest w załączniku");

            enovaMail.SendMail();
        }

        private void DodajZalacznik(Context cx, string nazwaPliku, Stream stream, DokumentHandlowy dokument)
        {
            stream.Seek(0L, SeekOrigin.Begin);

            using (var session = cx.Login.CreateSession(false, false))
            {
                using (var transaction = session.Logout(true))
                {
                    var kontrahent = CRMModule.GetInstance(session).Kontrahenci[dokument.Kontrahent.Guid];
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

        private string GenrujNazwePliku(DokumentHandlowy dokument)
        {
            var kodKontrahenta = dokument.Kontrahent.Kod;
            var numerFaktury = String.Join("", dokument.NumerPelnyZapisany.Split(Path.GetInvalidFileNameChars()));
            var timeStamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
            var rozszerzenie = ".pdf";

            var nazwaPliku = $"{kodKontrahenta}_{numerFaktury}_{timeStamp}{rozszerzenie}";
            return nazwaPliku;
        }

        private ReportResult UtworzReportResult(Context cx, DokumentHandlowy dokument)
        {
            return new ReportResult()
            {
                Context = cx,
                DataType = dokument.GetType(),
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



        #region unused

        public void ZmianaWartości(ZmianaDokumentuHandlowegoArgs args){
        }

        public void ZmianaPozycji(ZmianaPozycjiDokumentuArgs args){
        }

        public void WyliczenieCenyPozycji(WyliczenieCenyPozycjiDokumentuArgs args){
        }

        public void ZmianaPłatności(ZmianaDokumentuHandlowegoArgs args){
        }

        public void ZmianaStanu(ZmianaStanuDokumentuHandlowegoArgs args){
        }

        public void Zatwierdzanie(ZmianaDokumentuHandlowegoArgs args){
        }

        #endregion unused

    }
}
