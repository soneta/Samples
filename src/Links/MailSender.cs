using Soneta.Business;
using Soneta.CRM;

namespace Links
{ 
    internal static class MailSender
    {

        internal static void Send(string body, string recipient, Session session)
        {
            var konto = CRMModule.GetInstance(session).KontaPocztowe.WgNazwa["bryja_klaudiusz@poczta.fm"];
            using (var tran = session.Logout(true))
            {
                WiadomoscEmail w = new WiadomoscEmail();
                CRMModule.GetInstance(session).WiadomosciEmail.AddRow(w);
                w.KontoPocztowe = konto;
                w.Tresc = body;
                w.Do = $"<{recipient}>";
                w.Od = $"\"Klaudiusz Bryja\" <{konto.NazwaNadawcy}>";
                w.Temat = "Pozdrowienia z konferencji...";

                MailHelper.SendMessage(w);
            }
        }

    }

}