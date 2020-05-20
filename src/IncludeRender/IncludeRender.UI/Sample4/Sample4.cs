using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Core;
using Soneta.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Worker(typeof(IncludeRender.UI.Sample4.Sample4))]

namespace IncludeRender.UI.Sample4
{
    public class Sample4
    {
        [Context(Required = true)]
        public Session Session { get; set; }

        public void RenderLabel(StackContainer container)
        {
            if (Session.CoreModule().Config.Firma.DaneEwidencyjne.OsobaFizyczna)
                ((TemplateElement)container.FindByName("BrakDanych")).CloneInPlace();
            else
                ((TemplateElement)container.FindByName("Dane")).CloneInPlace();

            ((TemplateElement)container.FindByName("BrakDanych")).CloneInPlace();
            ((TemplateElement)container.FindByName("Dane")).CloneInPlace();
            ((TemplateElement)container.FindByName("Dane")).RemoveInPlace();

            container.Elements.Add(new LabelElement
            {
                CaptionHtml = "Brak danych".Translate()
            });
        }
    }
}
