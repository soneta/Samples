using System;

using Soneta.Business;
using Soneta.Business.Db;


namespace Pnwb.Geekout
{
    public abstract class DBItemWorkerBaseResultItem : ISessionable
    {
        private Row _row;

        public override string ToString() => Name;

        public void RefreshRow()
        {
            if (!RefreshNeeded) return;
            RefreshNeeded = false;
            var dbic = new DBItemContext(DBItem);
            try
            {
                dbic.Init();
                Row = dbic.Session.Get(Row);
                RefreshValues();
            }
            finally
            {
                dbic.Dispose();
            }
        }

        protected virtual void RefreshValues()
        { }

        public bool RefreshNeeded { get; set; }
        public string Name => DBItem.Name;
        public Row Row
        {
            get => _row; set
            {
                _row = value;
                RefreshValues();
            }
        }
        public DBItem DBItem { get; set; }
        public Session Session { get; set; }
        public Exception Exception { get; set; }
    }
}
