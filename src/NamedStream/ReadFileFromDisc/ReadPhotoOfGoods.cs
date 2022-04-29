using System.IO;
using ReadFileFromDisc;
using Soneta.Business;
using Soneta.Business.Db;
using Soneta.Business.UI;
using Soneta.Towary;
using Soneta.Types;

[assembly: Worker(typeof(ReadPhotoOfGoods), typeof(Towary))]

namespace ReadFileFromDisc
{
    public class ReadPhotoOfGoods
    {

        //public class Params : ContextBase
        //{
        //    public Params(Context context)
        //        : base(context)
        //    {
        //    }

        //    public NamedStream[] ChosenFiles { get; set; }

        //    public object GetListChosenFiles()
        //    {
        //        return new FileDialogInfo
        //        {
        //            Title = "Wybierz plik",
        //            DefaultExt = ".png",
        //        };
        //    }
        //}

        [Context] public Session Session { get; set; }
        //[Context] public Params Parameters { get; set; }
        //private static readonly Date Now = Date.Now;

        [Context] [Required] public NamedStream[] ChosenFiles { get; set; }

        [Action("Aktualizuj zdjęcia towarów",
            Target = ActionTarget.Menu,
            Mode = ActionMode.SingleSession,
            Priority = 0)]
        public void ReadPhotoAndAddAttachment()
        {
            using (var tran = Session.Logout(true))
            {
                foreach (var file in ChosenFiles)
                //foreach (var file in Parameters.ChosenFiles)
                {
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
                    var towar = Session.GetTowary().Towary.WgKodu[fileNameWithoutExtension];
                    if (towar == null)
                    {
                        towar = new Towar();
                        Session.AddRow(towar);
                        towar.Kod = fileNameWithoutExtension;
                        towar.Nazwa = fileNameWithoutExtension;
                    }

                    var fileName = Path.GetFileName(file.FileName);
                    Session.GetBusiness().Attachments.WgParent[towar, AttachmentType.Attachments, fileName]?.Delete();
                    var attachment = new Attachment(towar, AttachmentType.Attachments);
                    Session.AddRow(attachment);
                    attachment.DataStream = file;
                    attachment.Name = fileName;
                    attachment.LoadIconFromRawData(file.GetData());
                }

                tran.Commit();
            }
        }

    }
}