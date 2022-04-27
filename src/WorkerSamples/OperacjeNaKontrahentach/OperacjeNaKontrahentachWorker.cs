using Soneta.Business;
using Soneta.CRM;
using Soneta.Types;
using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

[assembly: Worker(typeof(OperacjeNaKontrahentach.UI.OperacjaNaKontrahencieWorker), typeof(Kontrahent))]
[assembly: Worker(typeof(OperacjeNaKontrahentach.UI.OperacjeNaKontrahentachWorker), typeof(Kontrahenci))]

namespace OperacjeNaKontrahentach.UI
{
    public class OperacjaNaKontrahencieWorker : OperacjeNaKontrahentachBase
    {
        [Context]
        public Kontrahent kontrahent { get; set; }

        // worker zarejestrowany na typie wiersza pojawiłby się zarówno na formatce tego obiektu, jak i na listach tych obiektów,
        // jednak Mode = ActionMode.OnlyForm zapewnia pojawienie się workera wyłącznie na formatce obiektu
        [Action("Wyślij mail", Target = ActionTarget.ToolbarWithText, Mode = ActionMode.SingleSession | ActionMode.OnlyForm)]
        public object AkcjaNaKontrahencie()
        {
            Trace.Write("SHOWOUTPUT", LogMessagesCategory);

            // utworzenie sesji roboczej z sesji dostępnej w kontekście - jeśli potrzebne
            using (var sesjaRobocza = context.Session.Login.CreateSession(false, false, "Akcja na kontrahencie"))
            {
                // tu otwieramy i commitujemy transakcję, ponieważ sama metoda 'WykonajNaKontrahencie' tego nie robi
                // aby dać nam dowolność w sterowaniu transakcjami
                using (var transakcjaRobocza = sesjaRobocza.Logout(true))
                {
                    //// w tym miejscu przykład uzyskania elementów potrzebnych do operacji
                    //// na przykład dostępu do modułu CRM, potrzebnego serwisu itp.
                    //var crm = sesjaRobocza.GetCRM();
                    //var mailService = sesjaRobocza.GetRequiredService<IExtMailer>();

                    // wywołanie operacji na kontrahencie z wczytaniem go z sesji roboczej - jeśli to potrzebne
                    WykonajNaKontrahencie(sesjaRobocza.Get(kontrahent));

                    transakcjaRobocza.Commit();
                }
            }

            return "Wysłano mail do " + kontrahent.Nazwa; // string zwracany przez metodę Action workera zostanie wyświetlony jako MessageBox
        }
    }

    public class OperacjeNaKontrahentachWorker : OperacjeNaKontrahentachBase
    {
        [Context]
        public Kontrahent[] kontrahenci { get; set; }

        // ActionMode.OnlyTable zapewnia pojawienie się workera tylko na liście obiektów danego typu
        // W tym miejscu jest nadmiarowy, ponieważ worker zarejestrowany na typie obiektu tabeli (tu Kontrahenci)
        // pojawi się wyłącznie na listach kontrahentów
        [Action("Wyślij mail", Target = ActionTarget.ToolbarWithText, Mode = ActionMode.SingleSession | ActionMode.OnlyTable)]
        public object AkcjaNaKontrahentach()
        {
            Trace.Write("SHOWOUTPUT", LogMessagesCategory); // wymuszenie otwarcia logu systemowego dla konkretnej kategorii

            using (var sesjaRobocza = context.Session.Login.CreateSession(false, false, "Maile do kontrahentów"))
            {
                //// w tym miejscu przykład uzyskania elementów potrzebnych do operacji
                //// na przykład dostępu do modułu CRM, potrzebnego serwisu itp.
                //var modulCRM = sesjaRobocza.GetCRM();
                //var mailService = sesjaRobocza.GetRequiredService<IExtMailer>();

                // tu otwieramy i commitujemy transakcję, ponieważ sama metoda 'WykonajNaKontrahencie' tego nie robi
                // aby dać nam dowolność w sterowaniu transakcjami
                // Pierwsza metoda to jedna transakcja do operacji na wszystkich kontrahentach
                // Otwieramy ją na zewnątrz pętli foreach
                using (var transakcjaZbiorcza = sesjaRobocza.Logout(true))
                {
                    foreach (var kontrahent in kontrahenci)
                    {
                        // jeśli chcemy operację na każdym kontrahencie przeprowadzać w oddzielnej transakcji
                        // otwieramy ją tu:

                        //using (var transakcjaIndywidualna = sess.Logout(true))
                        //{

                        WykonajNaKontrahencie(sesjaRobocza.Get(kontrahent));

                        //transakcjaIndywidualna.Commit(); // i taką oddzielną transakcję commitujemy tu
                        //}
                    }

                    transakcjaZbiorcza.Commit(); // tutaj commit dla pojedynczej transakcji na wszystkich kontrahentach
                }
            }

            // string zwracany przez metodę Action workera zostanie wyświetlony jako MessageBox
            // Dodatkowa informacja o szczegółach w logu systemowym w kategorii 'Operacje na kontrahentach'
            return "Wysłano maile (szczegóły w logu " + LogMessagesCategory + ").";
        }

        // Metoda statyczna IsVisible, decydująca o widoczności odpowiedniej metody workera.
        // W tym przypadku worker (metoda workera) będzie widoczna wyłącznie na liście "Kontrahenci i urzędy / Kontrahenci".
        public static bool IsVisibleAkcjaNaKontrahentach(Context context) =>
            (context[typeof(UILocation)] as UILocation)?.FolderNormalizedPath == "KontrahenciIUrzedy/Kontrahenci" == true;

        // Metoda statyczna IsVisible, decydująca o dostępności (aktywności) odpowiedniej metody workera.
        // W tym przypadku worker (metoda workera) będzie widoczna wyłącznie na liście "Kontrahenci i urzędy / Kontrahenci".
        public static bool IsEnabledAkcjaNaKontrahentach(Context context) =>
            (context[typeof(Kontrahent[])] as Kontrahent[])?.Length != 0 == true;
    }

    public abstract class OperacjeNaKontrahentachBase
    {
        internal const string LogMessagesCategory = "Operacje na kontrahentach";

        [Context]
        public Context context { get; set; }

        public void WykonajNaKontrahencie(Kontrahent kth)
        {
            // Jakieś tam operacje na kontrahencie. Potrzebną ewentualnie sesję można wziąć z samego kontrahenta: kth.Session.

            Trace.WriteLine(kth.NazwaPierwszaLinia, LogMessagesCategory);
        }
    }


}
