using System;
using System.Collections.Generic;
using System.Linq;
using Soneta.Types;

namespace Samples
{
    public class KalkulatorRabatu
    {
        private const decimal Prog0 = 0;
        private const decimal Prog1 = 0.2m;
        private const decimal Prog2 = 0.5m;
        private const decimal DarmowaWysylka = 1m;

        public static decimal PoliczRabat(Func<decimal> getValue, Func<bool> isLoyal)
        {
            var modifier = isLoyal() ? 1 : 2;

            if (getValue() < 50 * modifier)
                return Prog0;

            if (getValue() < 250 * modifier)
                return Prog1;

            if (getValue() < 500 * modifier)
                return Prog2;

            return DarmowaWysylka;
        }

        public static bool LojalnyKontrahent(Func<IEnumerable<Date>> dokumenty) => 
            dokumenty().Count(x => x > Date.Today.AddMonths(-6)) >= 5;

    }
}