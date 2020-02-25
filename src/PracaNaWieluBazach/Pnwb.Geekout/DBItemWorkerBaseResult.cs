using System.Collections.Generic;

using Soneta.Business;
using Soneta.Business.Db;
using Soneta.Business.UI;


namespace Pnwb.Geekout
{
    public abstract class DBItemWorkerBaseResult : ISessionable
    {
        public readonly object SyncRoot = new object();

        private ViewInfo vi;

        public Session Session { get; set; }

        public List<DBItemWorkerBaseResultItem> Items { get; } = new List<DBItemWorkerBaseResultItem>();

        public ViewInfo ViewInfo
        {
            get
            {
                if (vi != null) return vi;
                vi = new ViewInfo();
                vi.CreateView += (o, args) =>
                {
                    Items.ForEach(p => p.RefreshRow());
                    args.DataSource = Items;
                };
                vi.Action += (o, args) =>
                {
                    if (args.Action == ActionEventArgs.Actions.Edit)
                    {
                        var orig = ((DBItemWorkerBaseResultItem)args.OriginalFocusedData);
                        var dbic = new DBItemContext(orig.DBItem);
                        try
                        {
                            dbic.Init();
                            args.FocusedData = new FormActionResult()
                            {
                                EditValue = dbic.Session.Get(orig.Row),
                                Context = dbic.Context,
                                CommittedHandler = (cx) =>
                                {
                                    dbic.Dispose();
                                    orig.RefreshNeeded = true;
                                    Session.InvokeChanged();
                                    return null;
                                }
                            };
                        }
                        catch
                        {
                            dbic.Dispose();
                            throw;
                        }
                    }
                    if (args.Action == ActionEventArgs.Actions.Remove)
                    {
                        var orig = ((DBItemWorkerBaseResultItem)args.OriginalFocusedData);
                        var dbic = new DBItemContext(orig.DBItem);
                        try
                        {
                            if (orig.Row != null)
                            {
                                dbic.Init();
                                using (var t = dbic.Session.Logout(true))
                                {
                                    dbic.Session.Get(orig.Row).Delete();
                                    t.Commit();
                                }
                                dbic.Session.Save();
                            }
                            Items.Remove(orig);
                            Session.InvokeChanged();
                        }
                        finally
                        {
                            dbic.Dispose();
                        }
                    }
                };
                return vi;
            }
        }
    }
}
