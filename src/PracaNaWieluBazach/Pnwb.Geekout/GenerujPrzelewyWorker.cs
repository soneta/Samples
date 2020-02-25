using System;
using System.Linq;

using Soneta.Business;
using Soneta.Business.Db;
using Soneta.Kasa;
using Soneta.Types;

[assembly: Worker(typeof(Pnwb.Geekout.GenerujPrzelewyWorker), typeof(DBItems))]

namespace Pnwb.Geekout
{
    public class GenerujPrzelewyWorker : DBItemWorkerBase
    {
        [Action("Zbiorcze naliczanie przelewów",
            Icon = ActionIcon.Coffee,
            Mode = ActionMode.SingleSession | ActionMode.ReadOnlySession,
            Priority = 1302, Target = ActionTarget.ToolbarWithText)]
        public object GenerujPrzelewy()
        {
            return AddInDatabases<NaliczaniePrzelewowResult, PrzelewResultItem>(
                (dbic) =>
                {
                    var wrk = new NaliczaniePrzelewówWorker
                    {
                        Pars = new NaliczaniePrzelewowCore.Params(dbic.Context),
                        Rozrachunki = dbic.Session.GetKasa().RozrachunkiIdx.WgData.ToArray()
                    };
                    wrk.Nalicz();
                    return wrk.Przelewy.Cast<PrzelewBase>();
                });
        }
    }
    
    public class PrzelewResultItem : DBItemWorkerBaseResultItem
    {
        protected override void RefreshValues()
        {
            var przelew = (PrzelewBase)Row;
            Numer = przelew.Numer.ToString();
            Zatwierdzony = przelew.Zatwierdzony ? "Tak" : "Nie";
            Data = przelew.Data;
            Podmiot = przelew.Podmiot.ToString();
        }

        public string Numer { get; private set; }
        public string Zatwierdzony { get; private set; }
        public Date Data { get; private set; }
        public string Podmiot { get; private set; }
    }

    [Caption("Zbiorcze naliczanie przelewów")]
    public class NaliczaniePrzelewowResult : DBItemWorkerBaseResult { }
}
