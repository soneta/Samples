using Microsoft.Extensions.DependencyInjection;
using Soneta.Business;
using Soneta.Handel;
using System;
using System.Diagnostics;

// Rejestracja workera na tabeli DokHandlowe spowoduje, że będzie on widoczny wyłącznie na liście dokumentów
[assembly: Worker(typeof(OperacjeNaDokumentach.UI.OperacjeNaDokHandlowychWorker), typeof(DokHandlowe))]

namespace OperacjeNaDokumentach.UI
{
    public class OperacjeNaDokHandlowychWorker
    {
        internal const string LogMessagesCategory = "Operacje na dokumentach";

        [Context]
        public Context context { get; set; }

        [Context]
        public DokumentHandlowy[] dokumenty { get; set; }

        [Action("Wypisz numery dokumentów", Mode = ActionMode.SingleSession)]
        public object AkcjaNaDokumentach()
        {
            if ((dokumenty?.Length ?? 0) == 0)
                return "Brak zaznaczonych dokumentów";

            var pojedynczyDokument = dokumenty.Length == 1;

            //// w tym miejscu przykład uzyskania elementów potrzebnych do operacji
            //// na przykład dostępu do modułu, potrzebnego serwisu itp.
            //var modulHandel = context.Session.GetHandel();
            //var mailService = context.Session.GetRequiredService<IExtMailer>();

            if (pojedynczyDokument)
            {
                var dok = dokumenty[0];

                WykonajNaDokumencie(dok);

                return "Zakończona operacja na dokumencie " + dok.Numer;
            }
            else
            {
                Trace.Write("SHOWOUTPUT", LogMessagesCategory); // wymuszenie otwarcia logu systemowego dla konkretnej kategorii

                foreach (var dokHandlowy in dokumenty)
                {
                    WykonajNaDokumencie(dokHandlowy);

                    Trace.WriteLine(dokHandlowy.Numer, LogMessagesCategory);
                }
            }

            // string zwracany przez metodę Action workera zostanie wyświetlony jako MessageBox
            // Dodatkowa informacja o szczegółach w logu systemowym w kategorii 'Operacje na kontrahentach'
            return "Zaznaczone dokumenty wypisano w logu " + LogMessagesCategory + ".";
        }

        private void WykonajNaDokumencie(DokumentHandlowy dokHan)
        {
            // Jakieś tam operacje na wierszu. Potrzebną ewentualnie sesję można wziąć z samego wiersza: dokHan.Session
        }
    }
}