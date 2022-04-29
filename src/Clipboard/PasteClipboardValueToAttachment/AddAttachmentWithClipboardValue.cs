using System;
using PasteClipboardValueToAttachment;
using Soneta.Business;
using Soneta.Business.Db;
using Soneta.Business.UI;
using Soneta.Commands;
using Soneta.Tools;

[assembly: Worker(typeof(AddAttachmentWithClipboardValue), typeof(GuidedRow))]

namespace PasteClipboardValueToAttachment
{
    internal class AddAttachmentWithClipboardValue
    {
        [Context] public GuidedRow GuidedRow { get; set; }

        [Context] public Session Session { get; set; }

        [Action("Wklej obrazek ze schowka",
            Target = ActionTarget.ToolbarWithText | ActionTarget.Menu | ActionTarget.LocalMenu | ActionTarget.Divider,
            Mode = ActionMode.SingleSession,
            Priority = 1,
            CommandShortcut = CommandShortcut.Control | CommandShortcut.V)]
        public object CopyData()
        {
            return new ClipboardStream
            {
                RequestedDataType = ClipboardDataType.Png,
                GetDataFromClipboard = true,
                OutputHandler = ClipboardDataHandler
            };
        }

        private object ClipboardDataHandler(ClipboardStream arg)
        {
            if (arg == null) throw new Exception("Brak obrazka w clipoardzie");

            using (var tran = Session.Logout(true))
            {
                var attachment = new Attachment(GuidedRow, AttachmentType.Attachments);
                Session.AddRow(attachment);
                var data = arg.GetData();
                attachment.DataStream = new NamedStream(UniqueName, data);
                attachment.LoadIconFromRawData(data);
                tran.Commit();
            }

            return null;
        }

        private string UniqueName => CoreTools.MakeUniqueName(GuidedRow.Guid.ToString("N"),
            fn => Session.GetBusiness().Attachments
                .WgParent[GuidedRow, AttachmentType.Attachments, fn + ".png"] != null) + ".png";
    }
}