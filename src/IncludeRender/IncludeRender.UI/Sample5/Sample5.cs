using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Core;

[assembly: Worker(typeof(IncludeRender.UI.Sample5.Sample5))]

namespace IncludeRender.UI.Sample5
{
    public class Sample5
    {

        [Context(Required = true)]
        public Session Session { get; set; }

        public void RenderLabel(ContainerElement container)
        {
            if (IsTest)
                ((TemplateElement)container.FindByName("ON")).CloneInPlace();
            else
                ((TemplateElement)container.FindByName("OFF")).CloneInPlace();
        }

        public string RenderKey => IsTest ? null : "ON";

        public bool IsTest { get; set; } = true;

        public bool VisibleLabel
        {
            get => Session.CoreModule().Config.Firma.DaneEwidencyjne.OsobaFizyczna;
        }
    }
}
