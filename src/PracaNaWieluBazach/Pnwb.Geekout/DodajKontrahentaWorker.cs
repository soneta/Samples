using System;

using Soneta.Business;
using Soneta.Business.Db;
using Soneta.CRM;

[assembly: Worker(typeof(Pnwb.Geekout.DodajKontrahentaWorker), typeof(DBItems))]

namespace Pnwb.Geekout
{
    public class DodajKontrahentaWorker : DBItemWorkerBase
    {
        [Action("Dodaj kontrahenta",
            Icon = ActionIcon.Coffee,
            Mode = ActionMode.SingleSession | ActionMode.ReadOnlySession,
            Priority = 1301, Target = ActionTarget.ToolbarWithText)]
        public object DodajKontrahenta()
        {
            ThrowIfMultipleDBItems();
            return AddBusinessRow(typeof(Kontrahent));
        }
    }
}
