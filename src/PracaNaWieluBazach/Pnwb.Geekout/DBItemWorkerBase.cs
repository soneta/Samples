using System;
using System.Collections.Generic;
using System.Linq;

using Soneta.Business;
using Soneta.Business.Db;
using Soneta.Business.UI;
using Soneta.Types;

namespace Pnwb.Geekout
{
    public abstract class DBItemWorkerBase
    {
        [Context]
        public Context Context { get; set; }

        private DBItem[] DBItems => (DBItem[])Context[typeof(DBItem[])];

        private DBItem DBItem => DBItems.FirstOrDefault();

        protected void ThrowIfMultipleDBItems()
        {
            if ((DBItems?.Length ?? 0) != 1)
                throw new BusException("Czynność można wykonać dla jednej zaznaczonej bazy.");
        }

        protected object AddBusinessRow(Type rowType, Action<DBItemContext> preCheck = null)
        {
            if (Context == null) return null;
            var dbic = new DBItemContext(DBItem);
            dbic.Init();
            preCheck?.Invoke(dbic);
            var contextParameters =
                from parameter in Table.GetRowConstructor(rowType).GetParameters()
                from ContextAttribute cxattr in parameter.GetCustomAttributes(typeof(ContextAttribute), true)
                select cxattr.ContextType ?? parameter.ParameterType;

            var qci = QueryContextInformation.CreateForTypes(contextParameters.ToArray());
            qci.Caption = "Dodawanie zapisu";
            qci.Context = dbic.Context;
            qci.AcceptHandler = () =>
            {
                var row = (Row)dbic.Context.CreateObject(null, Table.GetRowConstructor(rowType), null);
                if (row != null)
                {
                    var table = dbic.Session.Tables[rowType];
                    using (var t = dbic.Session.Logout(true))
                    {
                        table.AddRow(row);
                        t.Commit();
                    }

                    dbic.Context.Set(row);
                    return new FormActionResult()
                    {
                        EditValue = row,
                        Context = dbic.Context,
                        CommittedHandler = (cx) =>
                        {
                            dbic.Dispose();
                            return null;
                        }
                    };
                }
                dbic.Dispose();
                return null;
            };
            return qci;
        }

        private readonly object loginLock = new object();

        protected R AddInDatabases<R, T>(Func<DBItemContext, IEnumerable<Row>> AddRow)
            where T : DBItemWorkerBaseResultItem, new()
            where R : DBItemWorkerBaseResult, new()
        {
            var result = new R { Session = DBItems[0].Session };
            int c = 0;
            int l = DBItems.Length;
            var progress = new Log();
            var concurrent = new Concurrent();

            concurrent.ForEach(DBItems, dbitem =>
            {
                var dbic = new DBItemContext(dbitem);
                try
                {
                    dbic.Init(loginLock);
                    var rows = AddRow(dbic);
                    dbic.Session.Save();
                    lock (result.SyncRoot)
                    {
                        foreach (var row in rows)
                        {
                            result.Items.Add(new T()
                            {
                                Session = dbic.DBItem.Session,
                                DBItem = dbic.DBItem,
                                Row = row
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lock (result.SyncRoot)
                    {
                        result.Items.Add(new T()
                        {
                            Session = dbic.DBItem.Session,
                            DBItem = dbic.DBItem,
                            Exception = ex
                        });
                    }
                }
                finally
                {
                    dbic.Dispose();
                }
                progress.Write(new Percent(c++, l));
            });
            return result;
        }
    }
}
