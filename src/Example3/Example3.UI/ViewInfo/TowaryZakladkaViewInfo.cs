using Soneta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Example3.UI.Extender
{
    public class TowaryZakladkaViewInfo : ViewInfo
    {
        public TowaryZakladkaViewInfo()
        {

        }

        /// <summary>
        /// Metoda pozwalająca na sterowanie widocznościa zakładki.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>
        ///     true - widoczność zakładki, 
        ///     false - zakładka niewidoczna
        /// </returns>
        public static bool IsVisible(Context context)
        {
            bool result;
            using (var session = context.Login.CreateSession(true, true))
            {
                result = ZakladkaTowaryConfigExtender.IsAktywneZakladkaTowary(session);
            }
            return result;
        }
    }
}
