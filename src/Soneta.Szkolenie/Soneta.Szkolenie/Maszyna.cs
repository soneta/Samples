using Soneta.Business;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly:NewRow(typeof(Soneta.Szkolenie.Maszyna))]

namespace Soneta.Szkolenie
{
    public class Maszyna : SzkolenieModule.MaszynaRow
    {
        public override string ToString()
        {
            return Producent + " " + Model + " " + NrBoczny;
        }
/*
        Ponieważ w business.xml został dla pola zdefiniowany weryfikator (dyrektywą <verifier>Maszyna.WymaganaNazwa</verifier>,
        definiujemy go jako klasę zagnieżdzoną klasy Maszyna 
*/
        internal class NrBocznyPoprawny : RowVerifier
        {
            private readonly Maszyna maszyna;
            public NrBocznyPoprawny(Maszyna row) : base(row, "Producent")
            {
                maszyna = row;
            }

            public override VerifierType Type => VerifierType.Error; // To jest weryfiikator typu Error

            protected override bool IsValid() // Funkcja sprawdzająca poprawność
            {
                return maszyna.NrBoczny.Contains("-");
            }

            // funkcja zwracająca komunikat wyświetlany w przypadku błędu
            public override string Description => "Wymagane jest aby w numerze bocznym znajdował się znak -";
        }

        protected override void OnAdded()
        {
            base.OnAdded();

            // w tym miejscu można zainicjować interesujące nas pola nowego wiersza
            DataProd = Date.Today;
        }
    }
}
