using DodatekTreningowySortowanie;
using Soneta.Business;
[assembly: NewRow(typeof(SortPodObiekt))]
namespace DodatekTreningowySortowanie
{
    public  class SortPodObiekt : DodatekTreningowySortowanieModule.SortPodObiektRow
    {
        public override string NazwaPodObiektuVirtual
        {
            get => base.NazwaPodObiektuVirtual + "_get_override";
            set => base.NazwaPodObiektuVirtual = "set_override_" + value ;
        }

        public override string ToString() => $"KP: {KodPodObiektu} NV: {NazwaPodObiektuVirtual} O: {OkresPodObiektu} OP: {OpisPodObiektu}";
    }
}
