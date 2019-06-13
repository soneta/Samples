using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Tools;
using Soneta.Types;

[assembly:NewRow(typeof(PrezentacjaGeekOut2019.LotyWidokowe.Maszyna))]

namespace PrezentacjaGeekOut2019.LotyWidokowe
{
    public class Maszyna : LotyWidokoweModule.MaszynaRow
    {
       public Maszyna() : base()
        { }

        [AttributeInheritance]
        public new string NrBoczny
        {
            get => base.NrBoczny;
            set => base.NrBoczny = value;
        }

        [AttributeInheritance]
        public new string Producent
        {
            get => base.Producent;
            set => base.Producent = value;
        }

        [AttributeInheritance]
        public new string Model
        {
            get => base.Model;
            set => base.Model = value;
        }

        [AttributeInheritance]
        public new Date RokProd
        {
            get => base.RokProd;
            set => base.RokProd = value;
        }

        [AttributeInheritance]
        public new string Uwagi
        {
            get => base.Uwagi;
            set => base.Uwagi = value;
        }

        public override string ToString()
        {
            return Producent + " " + Model + " " + NrBoczny;
        }

        internal class WymaganaNazwa : RowVerifier
        {
            private readonly Maszyna maszyna;
            public WymaganaNazwa(Maszyna row) : base(row, "Producent")
            {
                maszyna = row;
            }

            public override VerifierType Type => VerifierType.Error;

            protected override bool IsValid()
            {
                return  maszyna.NrBoczny.Contains("-");
            }

            public override string Description => "Wymagane jest aby w numerze bocznym znajdował się znak -";
        }

        protected override void OnAdded()
        {
            RokProd = Date.Today;
        }

      
        
    }
}
