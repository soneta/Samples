using DodatekTreningowySortowanie;
using Soneta.Business;
using Soneta.Types;

[assembly: NewRow(typeof(SortObiekt))]
namespace DodatekTreningowySortowanie
{
    public partial class SortObiekt : DodatekTreningowySortowanieModule.SortObiektRow
    {
        public override string NazwaVirtual 
        {
            get => base.NazwaVirtual + "_get_override";
            set => base.NazwaVirtual = "set_override" + value ;
        }
        //[SqlResolving(ParentTableSubRow = "SortRelObiekty", RelationField = "SortObiekt")]
        public Date DataObiektu2
        {
            get { return Relacje.GetNext().OkresSortRelObiektu.From; }
            set { base.DataObiektu = value; }

        }
       // [SqlResolving(ParentTableSubRow = "SortRelObiekty", RelationField = "SortObiekt")]
        public new Date DataObiektu
        {
            get { return Relacje.GetNext().OkresSortRelObiektu.From; }
            set { base.DataObiektu = value; }

        }
        /// <summary>
        /// przy usuwaniu SortObiekt chcemy także usunąć SortPodObiekt, stąd tez potrzebny jest ten fragment
        /// </summary>
        protected override void OnDeleted()
        {
            base.OnDeleted();
            var podobiekt = DodatekTreningowySortowanieModule.GetInstance(this).SortPodObiekty.WgKod[SortPodObiekt.KodPodObiektu];
            podobiekt?.Delete();
        }

        public override string ToString()
        {
            return $"KodObiektu: {KodObiektu} Cena: {Cena} Data {DataObiektu} Okres {OkresObiektu}";
        }
    }
}
