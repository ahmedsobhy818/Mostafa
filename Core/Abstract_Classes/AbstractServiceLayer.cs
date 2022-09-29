using Core.Base_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract_Classes
{
    public abstract class AbstractServiceLayer    {
        
        public AbstractUnitOfWork UnitOfWork { get; set; }
        public abstract void  Save();
        //{

        //}

//#if DESKTOP   //it is for desktop binding to editable grid only
//if your project is only desktop so you dont need #if and #endif
        public IList<T> GetBindingList<T>(List<T> InputList)
        {
            if (InputList == null)
                return null;

            Type listType = typeof(BindingList<>).MakeGenericType(typeof(T));

            IList<T> list = (IList<T>)Activator.CreateInstance(listType);

            InputList.ForEach((obj) =>
            {
                list.Add(obj);
            });
            return list;
        }
//#endif 
        public DataSet data
        {
            get
            {
                return UnitOfWork.data;
            }
        }
    }
}
