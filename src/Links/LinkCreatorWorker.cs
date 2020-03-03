using System.Linq;
using Links;
using Soneta.Business;
using Soneta.Core;
using Soneta.Core.DbTuples;

[assembly: Worker(typeof(LinkCreatorWorker), typeof(DbTuple))]

namespace Links
{

    internal class LinkCreatorWorker
    {

        [Context]
        public DbTuple Tuple { get; set; }

        [Action("Utwórz link i wyślij",
            Target = ActionTarget.Menu | ActionTarget.ToolbarWithText,
            Icon = ActionIcon.Envelope,
            Mode = ActionMode.SingleSession
        )]
        public void CreateAndSend()
        {
            var content = Tuple.Fields["Content"]?.ToString();
            if (string.IsNullOrWhiteSpace(content)) return;

            var link = CreateLink();
            SendLink(link, content);
        }

        private string CreateLink()
        {
            using (var tran = Tuple.Session.Logout(true))
            {
                var pars = Tuple.Fields["Pars"]?.ToString() ?? "Nie określono parametrów";
                var info = Tuple.Fields["Info"]?.ToString() ?? "Nie określono pola Info";
                var link = HTTPLinkInfo.Create<LinkHandler>(Tuple, pars).GetLink();

                tran.Commit();

                return $"<a href=\"{link}\" target=\"_blank\" title=\"{info}\">{info}</a>";
            }
        }

        private void SendLink
            (string link, string content) => MailSender.Send($"{content}<br/>{link}", "klaudiusz.bryja@enova.pl", Tuple.Session);

        public static bool IsVisibleCreateAndSend
            (DbTuple tuple) => tuple?.Definicja
                                   .Fields.Where(field => field.Name == "Content"
                                                          || field.Name == "Info"
                                                          || field.Name == "Pars")
                                   .Count() == 3;

    }

}