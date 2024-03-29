﻿using Soneta.Business;
using Soneta.Types;
using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Soneta.Towary;

// Rejestracja workera na typie wiersza Towar spowoduje, że będzie on widoczny na liście towarów oraz na formatce towaru
// Dodatkowo, uruchomiony z listy towarów, będzie wywoływany oddzielnie dla każdego wiersza
[assembly: Worker(typeof(OperacjeNaTowarach.UI.OperacjeNaTowarzeWorker), typeof(Towar))]

namespace OperacjeNaTowarach.UI
{
    public class OperacjeNaTowarzeWorker
    {
        internal const string LogMessagesCategory = "Operacje na towarach";

        [Context]
        public Session sesjaRobocza { get; set; }

        [Context]
        public Towar towar { get; set; }

        [Context]
        public Towar[] towary { get; set; }

        [Action("Wypisz nazwy towarów", Mode = ActionMode.SingleSession)]
        public object AkcjaNaTowarze()
        {
            if ((towary?.Length ?? 0) == 0)
                return "Brak zaznaczonych towarów";

            var jedenZaznaczony = towary.Length == 1;

            if (!jedenZaznaczony) Trace.Write("SHOWOUTPUT", LogMessagesCategory);

            //// w tym miejscu przykład uzyskania elementów potrzebnych do operacji
            //// na przykład dostępu do modułu CRM, potrzebnego serwisu itp.
            //var towMod = sesjaRobocza.GetTowary();
            //var mailService = sesjaRobocza.GetRequiredService<IExtMailer>();

            WykonajNaTowarze(towar);

            if (!jedenZaznaczony)
                return "Obsłużony towar: " + towar.Nazwa; // string zwracany przez metodę Action workera zostanie wyświetlony jako MessageBox
            else
            {
                Trace.WriteLine(towar.Nazwa, LogMessagesCategory); // zapis do loga

                if (towar == towary[towary.Length - 1]) 
                    return "Lista zaznaczonych towarów znajduje się w logu '" + LogMessagesCategory + "'."; // przy ostatnim z zanaczonych wyświetlamy MsgBox z informacją
                else
                    return null; // w przeciwnym przypadku zwracamy null, czyli żadne okno nie zostanie otwarte
            }
        }

        private void WykonajNaTowarze(Towar twr)
        {
            // Jakieś tam operacje na towarze. Potrzebną ewentualnie sesję można wziąć z samego wiersza towaru: twr.Session.

        }
    }
}