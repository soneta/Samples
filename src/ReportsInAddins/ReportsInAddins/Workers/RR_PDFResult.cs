using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Handel;
using System;
using System.IO;

[assembly: Worker(typeof(ReportsInAddins.Workers.RR_PDFResult), typeof(DokumentHandlowy))]

namespace ReportsInAddins.Workers
{
    public class RR_PDFResult
    {
        [Context]
        public DokumentHandlowy Dokument { get; set; }

        [Action("ReportResult/PDFResult",
            Target = ActionTarget.Menu,
            Mode = ActionMode.SingleSession | ActionMode.OnlyForm)]
        public object Drukuj(Context cx)
        {
            Stream s = null;
            foreach (Soneta.Business.Db.Attachment a in Dokument.Attachments)
            {
                if (a.Name == "test.pdf")        //należy wcześniej umieścić dowolny plik wydruku nazwany test.pdf w załącznikach
                {
                    s = a.SaveToStream();
                    break;
                }
            }

            var reportResult = new PdfResult
            {
                Caption = "Nazwa",
                Action = PdfResultAction.Print,
                Stream = s,
                //FileName = @"D:\MG\CennikREPX.pdf"      //mozliwosc wykorzystania pliku PDF z dysku lokalnego
            };

            return reportResult;
        }
    }
}
