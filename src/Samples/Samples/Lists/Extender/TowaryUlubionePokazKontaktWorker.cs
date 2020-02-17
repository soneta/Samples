using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soneta.Business;
using Soneta.Commands;
using Soneta.Towary;
using Soneta.Types;
using Samples.Lists.Extender;

[assembly: Worker(typeof(TowaryUlubionePokazKontaktWorker), typeof(TowarUlubiony))]

namespace Samples.Lists.Extender
{
    class TowaryUlubionePokazKontaktWorker
    {
        [Context]
        public TowarUlubiony TowarUlubiony
        {
            get;
            set;
        }

        [Action("Pokaż osobę", Target = ActionTarget.ToolbarWithText, Mode = ActionMode.SingleSession,
            CommandShortcut = CommandShortcut.Shift | CommandShortcut.F8, Priority = 10, Icon =ActionIcon.Phone)]
        public Row PokazZapis()
        {
            Row zapis = TowarUlubiony.Zapis.Root;

            if (zapis is IRowWithHistory)
            {
                return (zapis as IRowWithHistory).Historia[Date.Today];
            }

            return zapis;
        }

        [Action("Pokaż towar", Target = ActionTarget.ToolbarWithText, Mode = ActionMode.SingleSession,
            Priority = 11, Icon = ActionIcon.Open)]
        public Row PokazTowar()
        {
            Row towar = TowarUlubiony.Towar;

            if (towar is IRowWithHistory)
            {
                return (towar as IRowWithHistory).Historia[Date.Today];
            }

            return towar;
        }
    }
}
