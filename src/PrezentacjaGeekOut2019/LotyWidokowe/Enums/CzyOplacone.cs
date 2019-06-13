using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soneta.Types;

namespace PrezentacjaGeekOut2019.LotyWidokowe
{
    public enum CzyOplacone
    {
        [Caption("Opłacone")]
        Oplacone = 0,
        [Caption("Nieopłacone")]
        Nieoplacone = 1,
        [Caption("Razem")]
        Razem = 2
    }
}
