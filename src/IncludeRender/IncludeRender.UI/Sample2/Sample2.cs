using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Worker(typeof(IncludeRender.UI.Sample2.Sample2))]

namespace IncludeRender.UI.Sample2
{
	public class Sample2
	{
		UIElement e;

		[Context(Required = true)]
		public Session Session { get; set; }

		public UIElement GetElement()
		{
			if (e == null) e = new FieldElement { CaptionHtml = "Podaj imię", EditValue = "{Imie}", Class = IsBold ? new UIClass[] { UIClass.BoldFont } : new UIClass[] { } };
			return e;
		}

		public bool IsBold
		{
			get => Session.CoreModule().Config.Firma.DaneEwidencyjne.OsobaFizyczna;
		}

	}
}
