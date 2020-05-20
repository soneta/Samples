using Soneta.Business;
using Soneta.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Worker(typeof(IncludeRender.UI.Sample3.Sample3))]

namespace IncludeRender.UI.Sample3
{
	public class Sample3
	{
		[Context(Required = true)]
		public Session Session { get; set; }

		public bool VisibilityLabel
		{
			get => Session.CoreModule().Config.Firma.DaneEwidencyjne.OsobaFizyczna;
		}

	}
}
