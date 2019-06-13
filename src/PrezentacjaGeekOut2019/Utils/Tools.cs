using System.Collections.Generic;
using System.Linq;

using System.Text;
using Soneta.Types;

namespace PrezentacjaGeekOut2019.Utils
{
    public static class Tools
    {
        /// <summary>
        /// Ważne ze względnu na wczytywanie bibliotek, które mają referencje do Soneta.Types
        /// Tylko biblioteki z referencją do Soneta.Types są wczytywane podczas analizowania form.xml
        /// </summary>
#pragma warning disable 414
        private static FromTo _ft = new FromTo();
#pragma warning restore 414

        public const string Version = "2.0";
    }
}
