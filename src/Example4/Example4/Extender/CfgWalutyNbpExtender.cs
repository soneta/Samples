
using Soneta.Business;
using Soneta.Config;
using Samples.Example4.Extender;

// Rejestracja extender'a użytego na interfejsie
[assembly: Worker(typeof(CfgWalutyNbpExtender))]

namespace Samples.Example4.Extender {
    public class CfgWalutyNbpExtender
    {
        [Context]
        public Session Session { get; set; }

        #region Property formularza

        public string UrlNbp {
            get { return GetValue("UrlNbp", ""); }
            set { SetValue("UrlNbp", value, AttributeType._string); }
        }

        public static string GetUrlNbp(Session session, string def)
        {
            return GetValue(session, "UrlNbp", def);
        }

        #endregion Property formularza

        #region Metody pomocnicze

        //Metoda odpowiada za ustawianie wartosci parametrów konfiguracji
        private void SetValue<T>(string name, T value, AttributeType type) {
            SetValue(Session, name, value, type);
        }

        //Metoda odpowiada za pobieranie wartosci parametrów konfiguracji
        private T GetValue<T>(string name, T def) {
            return GetValue(Session, name, def);
        }

        //Metoda odpowiada za ustawianie wartosci parametrów konfiguracji
        private static void SetValue<T>(Session session, string name, T value, AttributeType type) {
            using (var t = session.Logout(true)) {
                var cfgManager = new CfgManager(session);
                //wyszukiwanie gałęzi głównej 
                var node1 = cfgManager.Root.FindSubNode("Samples", false) ??
                            cfgManager.Root.AddNode("Samples", CfgNodeType.Node);

                //wyszukiwanie liścia 
                var node2 = node1.FindSubNode("Kursy Walut NBP", false) ??
                            node1.AddNode("Kursy Walut NBP", CfgNodeType.Leaf);

                //wyszukiwanie wartosci atrybutu w liściu 
                var attr = node2.FindAttribute(name, false);
                if (attr == null)
                    node2.AddAttribute(name, type, value);
                else
                    attr.Value = value;

                t.CommitUI();
            }
        }

        //Metoda odpowiada za pobieranie wartosci parametrów konfiguracji
        private static T GetValue<T>(Session session, string name, T def) {
            var cfgManager = new CfgManager(session);

            var node1 = cfgManager.Root.FindSubNode("Samples", false);

            //Jeśli nie znaleziono gałęzi, zwracamy wartosć domyślną
            if (node1 == null) return def;

            var node2 = node1.FindSubNode("Kursy Walut NBP", false);
            if (node2 == null) return def;

            var attr = node2.FindAttribute(name, false);
            if (attr == null) return def;

            if (attr.Value == null) return def;

            return (T) attr.Value;
        }

        #endregion Metody pomocnicze
    }


}
