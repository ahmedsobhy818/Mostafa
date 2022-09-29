using Core.Base_Classes;
using Core.Misc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract_Classes
{
    public abstract class AbstractUnitOfWork
    {
        protected   DbContext _Context;
        public abstract  void Save();
        //{
            
       // }
        public DataSet data
        {
            get
            {
                DataSet ds = new DataSet();
                ListtoDataTableConverter converter = new ListtoDataTableConverter();
                if (_Context.ChangeTracker.Entries().Count() == 0)
                    return ds;

                var groups = _Context.ChangeTracker.Entries()/*.ToList()*/.GroupBy(e => e.Entity.GetType());
                groups.ToList().ForEach((g) =>
                {
                    var name = g.Key.Name;
                    var list = g.Select(c => c.Entity).ToList();
                    DataTable dt = converter.ToDataTable(list);
                    dt.TableName = name;
                    ds.Tables.Add(dt);

                });
                return ds;
            }
        }
    }
}
