using System.Reflection;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Commands;

[assembly: Worker(typeof(CopyDetailsFromObject.CopyDetailsFromObject), typeof(GuidedRow))]

namespace CopyDetailsFromObject
{
    internal class CopyDetailsFromObject
    {

        [Context] public GuidedRow[] GuidedRows { get; set; }

        [Action("Kopiuj dane",
            Target = ActionTarget.ToolbarWithText | ActionTarget.Menu | ActionTarget.LocalMenu | ActionTarget.Divider,
            Mode = ActionMode.SingleSession,
            Priority = 1,
            CommandShortcut = CommandShortcut.Control | CommandShortcut.C)]
        public object CopyData()
        {
            var text = "";
            foreach (var guidedRow in GuidedRows)
            foreach (var property in guidedRow.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly))
                if (property.CanRead)
                    text += property.Name + ": " + property.GetValue(guidedRow, null) + "\n";
            return new ClipboardStream(text);
        }
    }

}