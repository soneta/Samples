using System;
using Soneta.Business;
using Soneta.Config;
using Samples.Example3.UI.Extender;

[assembly: Worker(typeof(ZakladkaTowaryConfigExtender))]

namespace Samples.Example3.UI.Extender
{
	/// <summary>
	/// Klasa wspiera zapis i odczyt parametrów konfiguracyjnych dodatku Samples Example3
	/// </summary>
	public class ZakladkaTowaryConfigExtender
	{
		[Context]
		public Session Session { get; set; }

		public bool AktywneZakladkaTowary
		{
			get { return GetValue("AktywneZakladkaTowary", false); }
			set { SetValue("AktywneZakladkaTowary", value, AttributeType._boolean); }
		}

		// Pobranie wartości parametru "AktywneZakladkaTowary"
		public static bool IsAktywneZakladkaTowary(Session session)
		{
			return GetValue(session, "AktywneZakladkaTowary", false);
		}

		// Pobranie wartości parametrów konfiguracyjnych
		private T GetValue<T>(string name, T defaultValue)
		{
			return GetValue(Session, name, defaultValue);
		}

		// Zapisanie wartośći parametrów konfiguracyjnych
		private void SetValue<T>(string name, T value, AttributeType type)
		{
			SetValue(Session, name, value, type);
		}

		// Pobranie wartości parametrów konfiguracyjnych
		private static T GetValue<T>(Session session, string name, T defaultValue)
		{
			var cfgManager = new CfgManager(session);
			var node = cfgManager.Root.FindSubNode("Samples", false);
			if (node == null) return defaultValue;

			var nodeLeaf = node.FindSubNode("Konfiguracja", false);
			if (nodeLeaf == null) return defaultValue;

			var attr = nodeLeaf.FindAttribute(name, false);
			if (attr == null) return defaultValue;

			if (attr.Value == null) return defaultValue;

			return (T)attr.Value;
		}

		// Ustawianie wartości parametrów konfiguracyjnych
		private static void SetValue<T>(Session session, string name, T value, AttributeType type)
		{
			using (var t = session.Logout(true))
			{
				var cfgManager = new CfgManager(session);
				var node1 = cfgManager.Root.FindSubNode("Samples", false) ??
					cfgManager.Root.AddNode("Samples", CfgNodeType.Node);

				var node2 = node1.FindSubNode("Konfiguracja", false) ??
					node1.AddNode("Konfiguracja", CfgNodeType.Leaf);

				var attr = node2.FindAttribute(name, false);
				if (attr == null)
					node2.AddAttribute(name, type, value);
				else
					attr.Value = value;

				t.CommitUI();
			}
		}
	}
}