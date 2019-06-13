using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Soneta.Business;

[assembly:NewRow(typeof(PrezentacjaGeekOut2019.LotyWidokowe.Lot))]

namespace PrezentacjaGeekOut2019.LotyWidokowe
{
    public class Lot : LotyWidokoweModule.LotRow
    {
        public Lot() : base()
        {
        }
            
        [AttributeInheritance]
        public new string KodUslugi
        {
            get => base.KodUslugi;
            set => base.KodUslugi = value;
        }

        [AttributeInheritance]
        public new string Nazwa
        {
            get => base.Nazwa;
            set => base.Nazwa = value;
        }

        [AttributeInheritance]
        public new int Cena
        {
            get => base.Cena;
            set => base.Cena = value;
        }

        [AttributeInheritance]
        public new string LokalizacjaMiejscowosc
        {
            get => base.LokalizacjaMiejscowosc;
            set => base.LokalizacjaMiejscowosc = value;
        }

        [AttributeInheritance]
        public new string LokalizacjaICAO
        {
            get => base.LokalizacjaICAO;
            set => base.LokalizacjaICAO = value;
        }

        [AttributeInheritance]
        public new string Opis
        {
            get => base.Opis;
            set => base.Opis = value;
        }

        public override string ToString()
        {
            return Nazwa + " " + LokalizacjaMiejscowosc;
        }

    }

}
