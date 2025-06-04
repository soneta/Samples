using System;
using Soneta.Business;
using Soneta.ProdukcjaPro;
using Soneta.ProdukcjaPro.Panel;
using Soneta.Tools;

namespace PanelMeldunkowy_Sample_01
{
    public class MaterialyPlanowane : PanelBase
        , INewBarCode
    {
        private ProOperacjaZlecenia _operacja;

        public string Lp => _operacja?.Lp.ToString();

        public string DefinicjaOperacji => _operacja?.DefinicjaOperacji?.ToString();

        public string Zlecenie => _operacja?.Zlecenie?.ToString();

        public View<ProMaterialOperacjiZlecenia> Materialy =>
            Context.Session.GetProdukcjaPro().ProMaterialyOZ.WgOperacja[_operacja].CreateView();

        private void Init(Context context, int id)
        {
            var cond = new FieldCondition.Equal("ID", id);
            var operacja = context.Session.GetProdukcjaPro().ProOperacjeZlec.WgZlecenie[cond].GetFirst();
            _operacja = operacja ?? throw new ApplicationException(
                "Nie znaleziono operacji zlecenia produkcyjnego o wskazanym identyfikatorze.".Translate());
            Context.InvokeChanged();
        }

        #region INewBarCode
        object INewBarCode.Enter(Context cx, string code, double quantity)
        {
            if (int.TryParse(code, out var id))
                Init(cx, id);
            else
                throw new ApplicationException("Nieprawidłowy format danych.".Translate());
            return DBNull.Value;
        }
        #endregion INewBarCode
    }
}
