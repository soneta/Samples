using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrezentacjaGeekOut2019.LotyWidokowe;
using Soneta.Business;

[assembly: Worker(typeof(RezerwacjeExtender))]
namespace PrezentacjaGeekOut2019.LotyWidokowe
{
    class RezerwacjeExtender
    {
        [Context]
        public Lot Lot { get; set; }
        public Maszyna Maszyna { get; set; }
    }
}
