using PrzykladHandel;
using Soneta.Business;
using Soneta.Core;
using Soneta.CRM;
using Soneta.EwidencjaVat;
using Soneta.Ksiega;
using Soneta.Types;

[assembly: Worker(typeof(GenerowanieEwidencjiSprzedazyWorker), typeof(Kontrahenci))]

namespace PrzykladHandel
{
    class GenerowanieEwidencjiSprzedazyWorker
    {
		[Context]
		public Kontrahent[] Kontrahenci { get; set; }

		[Context]
		public Session Session { get; set; }

		[Action("Przykład Handel/Generuj ewidencję sprzedaży",
			Mode = ActionMode.SingleSession | ActionMode.Progress | ActionMode.ConfirmFinished)]
		public void GenerujEwidencjeSprzedazy()
        {
			CoreModule coreModule = CoreModule.GetInstance(Session);
			EwidencjaVatModule ewidencjaVatModule = EwidencjaVatModule.GetInstance(Session);
			KsiegaModule kasaModule = KsiegaModule.GetInstance(Session);

			using (ITransaction t = Session.Logout(true))
			{
				foreach (var kontrahent in Kontrahenci)
				{
					// Utworzenie ewidencji sprzedaży i dodanie do tabeli ewidencji
					SprzedazEwidencja ewidencja = new SprzedazEwidencja();
					coreModule.DokEwidencja.AddRow(ewidencja);

					// Ustawienie dat
					ewidencja.DataWplywu = Date.Today;
					ewidencja.DataDokumentu = Date.Today;
					ewidencja.DataOperacji = Date.Today;

					// Ustawienie numeru dokumentu, podmiotu i opisu
					ewidencja.NumerDokumentu = "FV/2007/123456";
					ewidencja.Podmiot = kontrahent;
					ewidencja.Opis = "Faktura sprzedaży";

					// Dodanie elementów VAT
					ElemEwidencjiVATSprzedaz elemVAT = new ElemEwidencjiVATSprzedaz(ewidencja);
					ewidencjaVatModule.EleEwidencjiVATT.AddRow(elemVAT);
					elemVAT.DefinicjaStawki = coreModule.DefStawekVat[StatusStawkiVat.Opodatkowana, new Percent(0.22m), false];
					elemVAT.Netto = 1000m;

					// Płatności generują się automatycznie po każdej zmianie wartości ewidencji

					// Dodanie opisu analitycznego
					ElementOpisuEwidencji elemOpisu = new ElementOpisuEwidencji(ewidencja);
					kasaModule.OpisAnalityczny.AddRow(elemOpisu);
					elemOpisu.Wymiar = "Przychody";
					elemOpisu.Symbol = "701-0";
					elemOpisu.Kwota = 1000m;
				}

				t.Commit();
			}
		}
    }
}
