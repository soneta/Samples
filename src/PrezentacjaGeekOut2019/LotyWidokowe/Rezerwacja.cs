using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrezentacjaGeekOut2019.LotyWidokowe;
using Soneta.Business;
using Soneta.CRM;

[assembly:NewRow(typeof(PrezentacjaGeekOut2019.LotyWidokowe.Rezerwacja))]

namespace PrezentacjaGeekOut2019.LotyWidokowe
{
    public class Rezerwacja : LotyWidokoweModule.RezerwacjaRow
    {
        public Rezerwacja() : base()
        {

        }

        [AttributeInheritance]
        public new string NrRezerwacji
        {
            get => base.NrRezerwacji;
            set => base.NrRezerwacji = value;
        }

        [AttributeInheritance]
        public new Lot Lot
        {
            get => base.Lot;
            set => base.Lot = value;
        }

        [AttributeInheritance]
        public new Maszyna Maszyna
        {
            get => base.Maszyna;
            set => base.Maszyna = value;
        }

        [AttributeInheritance]
        public new Kontrahent Klient
        {
            get => base.Klient;
            set => base.Klient = value;
        }

        [AttributeInheritance]
        public new CzyOplacone CzyOplacona
        {
            get => base.CzyOplacona;
            set => base.CzyOplacona = value;
        }

        public CzyOplacone[] GetListCzyOplacona()
        {
            return new[] { CzyOplacone.Nieoplacone, CzyOplacone.Oplacone };

        }
    }

    public sealed class RezerwacjaZaplata : ContextBase
    {
        public RezerwacjaZaplata(Context context) : base(context) { }
        public CzyOplacone Typ { get; set; } = CzyOplacone.Nieoplacone;

        public CzyOplacone[] GetListTyp()
        {
            return new[] {CzyOplacone.Nieoplacone, CzyOplacone.Oplacone};

        }
    
    }
}
