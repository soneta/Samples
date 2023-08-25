using System.Text;
using System.Text.RegularExpressions;
using ReadFileFromList;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Types;

[assembly: Worker(typeof(ReadFile), typeof(DokHandlowe))]

namespace ReadFileFromList
{
    public class ReadFile
    {

        public class Params : ContextBase
        {
            public Params(Context context)
                : base(context)
            {
            }

            [Required]
            public string Plik { get; set; }

            public object GetListPlik()
            {
                FileDialogInfo fileDialogInfo = new FileDialogInfo
                {
                    Title = "Wybierz plik",
                    DefaultExt = ".xml",
                    ForbidMultiSelection = true,
                    InitialDirectory = @"C:\",

                };
                
                return fileDialogInfo.AddXmlFilter();
            }

        }

        [Context]
        public Session Session { get; set; }

        [Context]
        public Params BaseParams { get; set; }

        private static readonly Date Now = Date.Now;

        [Action("Odczytaj dane",
            Target = ActionTarget.ToolbarWithText | ActionTarget.Menu | ActionTarget.LocalMenu | ActionTarget.Divider,
            Mode = ActionMode.SingleSession)]
        public object ReadDataWorker()
        {
            Session.GetService(out IFileSystemService fss);
            if (fss == null)
                return null;

            byte[] fileContent = fss.ReadFile(BaseParams.Plik);

            string result = Encoding.UTF8.GetString(fileContent);
            string[] words = Regex.Split(result, "[^a-zA-Z]+");

            DokumentHandlowy dokhan;
            using (var tran = Session.Logout(true))
            {
                dokhan = new DokumentHandlowy
                {
                    Definicja = HandelModule.GetInstance(Session).DefDokHandlowych.WgSymbolu[words[1]],
                    Magazyn = HandelModule.GetInstance(Session).Magazyny.Magazyny.WgNazwa[words[3]],
                    Kontrahent = CRMModule.GetInstance(Session).Kontrahenci.WgKodu[words[5]],
                };
                HandelModule.GetInstance(Session).DokHandlowe.AddRow(dokhan);
                dokhan.Data = Now;
                tran.CommitUI();
            }
            return dokhan;
        }

    }

}
