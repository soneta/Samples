using Soneta.Business;
using Soneta.CRM;
using Soneta.Types;
using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

// Jednen worker zarejestrowany jest na type wiersza, więc pojawiłby się na liście i na formatce
[assembly: Worker(typeof(OperacjeNaKontrahentach.UI.OperacjaNaKontrahencieWorker), typeof(Kontrahent))]
// Drugi worker zarejestrowany jest na typie tabeli, więc pokaże się wyłącznie na liście
[assembly: Worker(typeof(OperacjeNaKontrahentach.UI.OperacjeNaKontrahentachWorker), typeof(Kontrahenci))]

namespace OperacjeNaKontrahentach.UI
{
    public class OperacjaNaKontrahencieWorker : OperacjaNaKontrahencieBase
    {
        [Context]
        public Kontrahent kontrahent { get; set; }

        // worker zarejestrowany na typie wiersza pojawiłby się zarówno na formatce tego obiektu, jak i na listach tych obiektów,
        // jednak Mode = ActionMode.OnlyForm zapewnia pojawienie się workera wyłącznie na formatce obiektu
        [Action("Pokaż kontrahenta", Target = ActionTarget.ToolbarWithText, Mode = ActionMode.SingleSession | ActionMode.OnlyForm)]
        public object AkcjaNaKontrahencie()
        {
            //// w tym miejscu przykład uzyskania elementów potrzebnych do operacji
            //// na przykład dostępu do modułu CRM, potrzebnego serwisu itp.
            //var crm = sesjaRobocza.GetCRM();
            //var mailService = sesjaRobocza.GetRequiredService<IExtMailer>();

            WykonajNaKontrahencie(kontrahent);

            return "Otwarty kontrahent: " + kontrahent.Nazwa; // string zwracany przez metodę Action workera zostanie wyświetlony jako MessageBox
        }
    }

    public class OperacjeNaKontrahentachWorker : OperacjaNaKontrahencieBase
    {
        internal const string LogMessagesCategory = "Operacje na kontrahentach";

        [Context]
        public Kontrahent[] kontrahenci { get; set; }

        // ActionMode.OnlyTable zapewnia pojawienie się workera tylko na liście obiektów danego typu
        // W tym miejscu jest nadmiarowy, ponieważ worker zarejestrowany na typie obiektu tabeli (tu Kontrahenci)
        // pojawi się wyłącznie na listach kontrahentów
        [Action("Pokaż kontrahentów", Target = ActionTarget.ToolbarWithText, Mode = ActionMode.SingleSession | ActionMode.OnlyTable)]
        public object AkcjaNaKontrahentach()
        {
            Trace.Write("SHOWOUTPUT", LogMessagesCategory); // wymuszenie otwarcia logu systemowego dla konkretnej kategorii

            //// w tym miejscu przykład uzyskania elementów potrzebnych do operacji
            //// na przykład dostępu do modułu CRM, potrzebnego serwisu itp.
            //var modulCRM = sesjaRobocza.GetCRM();
            //var mailService = sesjaRobocza.GetRequiredService<IExtMailer>();

            foreach (var kontrahent in kontrahenci)
            {
                WykonajNaKontrahencie(kontrahent);

                Trace.WriteLine(kontrahent.NazwaPierwszaLinia, LogMessagesCategory);
            }

            // string zwracany przez metodę Action workera zostanie wyświetlony jako MessageBox
            // Dodatkowa informacja o szczegółach w logu systemowym w kategorii 'Operacje na kontrahentach'
            return "Zaznaczeni kontrahenci wypisani w logu (" + LogMessagesCategory + ").";
        }
    }

    public abstract class OperacjaNaKontrahencieBase
    {
        internal void WykonajNaKontrahencie(Kontrahent kth)
        {
            // Jakieś tam operacje na kontrahencie. Potrzebną ewentualnie sesję można wziąć z samego kontrahenta: kth.Session.
        }
    }
}
