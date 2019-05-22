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

        public static decimal PoliczRabat(decimal value, bool isLoyal)
        {
            var modifier = isLoyal ? 1 : 2;

            if (value < 50 * modifier)
                return Prog0;

            if (value < 250 * modifier)
                return Prog1;

            if (value < 500 * modifier)
                return Prog2;

            return DarmowaWysylka;
        }

        public static bool LojalnyKontrahent(IEnumerable<Date> dates) => 
            dates.Count(x => x > Date.Today.AddMonths(-6)) >= 5;

    }
}