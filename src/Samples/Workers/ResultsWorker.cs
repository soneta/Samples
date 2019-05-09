using Samples.Workers;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.CRM;
using Soneta.Types;

[assembly: Worker(typeof(ResultsWorker), typeof(Kontrahent))]

namespace Samples.Workers
{
    /// <summary>
    /// Wokrer pochodzi z repozytorium Soneta.Examples - https://github.com/soneta/Examples (Example 6)
    /// </summary>
    public class ResultsWorker
    {
        [Context]
        public Kontrahent Kontrahent { get; set; }

        [Action(@"Samples/MessageBox YesNo", Mode = ActionMode.SingleSession, Priority = 90001)]
        public MessageBoxInformation MsgYesNo()
        {
            return new MessageBoxInformation("Wybierz coś")
            {
                Text = "To jest jakaś wyświetlana informacja",
                YesHandler = () => Kontrahent,
                NoHandler = () => "Wciśnięty NO"
            };
        }

        [Action(@"Samples/QueryContext1 ...", Mode = ActionMode.SingleSession, Priority = 90002)]
        public QueryContextInformation QueryContextExample()
        {
            return QueryContextInformation.Create<WParams>(args => new MessageBoxInformation
            {
                Text = "Dane zostały zmienione. Potwierdzasz zapis ?",
                OKHandler = () =>
                {
                    using (var trans = Kontrahent.Session.Logout(true))
                    {
                        Kontrahent.Adres.Telefon = args.NowyTelefon;
                        Kontrahent.Adres.Faks = args.NowyFax;
                        trans.CommitUI();
                    }
                    return null;
                },
                CancelHandler = () => null,
                IsCancelVisible = true,
            });
        }

        [Action(@"Samples/QueryContext2 ...", Mode = ActionMode.SingleSession, Priority = 90003)]
        public QueryContextInformation QueryContextExample2()
        {
            return QueryContextInformation.Create<WParams>(args =>
            {
                using (var trans = Kontrahent.Session.Logout(true))
                {
                    Kontrahent.Adres.Telefon = args.NowyTelefon;
                    Kontrahent.Adres.Faks = args.NowyFax;
                    trans.CommitUI();
                }
                return null;
            });
        }

        [Action(@"Samples/QueryContext3 ...", Mode = ActionMode.SingleSession, Priority = 90004)]
        public QueryContextInformation KontrahentQuery()
        {
            var qci = new QueryContextInformation(Kontrahent)
            {
                Caption = "Dodawanie ooby kontaktowej",
                AcceptHandler = () =>
                {
                    KontaktOsoba kontakt;
                    using (var trans = Kontrahent.Session.Logout(true))
                    {
                        kontakt = new KontaktOsoba();
                        Kontrahent.Module.KontaktyOsoby.AddRow(kontakt);
                        kontakt.Kontrahent = Kontrahent;
                        trans.CommitUI();
                    }
                    return kontakt;
                }
            };
            return qci;
        }
    }

    public class WParams : ContextBase
    {
        public WParams(Context cx) : base(cx)
        {
        }

        [Caption("Nowy numer telefonu")]
        public string NowyTelefon { get; set; }

        [Caption("Nowy numer faksu")]
        public string NowyFax { get; set; }
    }
}
