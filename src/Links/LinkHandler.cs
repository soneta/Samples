using System;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Core;

namespace Links
{

    internal class LinkHandler : HTTPLinkInfo.IHTTPLinkAction
    {

        object HTTPLinkInfo.IHTTPLinkAction.Invoke
            (IGuidedRow document, string pars) => new MessageBoxInformation("Zlinkuj się z enova365",
            $"Wywołanie link'ów enova365{Environment.NewLine}Dokument: {document}{Environment.NewLine}Parametry: {pars}");

    }

}