using Core.Abstract_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Misc
{
   public class MyList<T> : List<T>
     {
        public delegate void AddEventHandler(MyList<T> list,MyListAddEventArg arg);
        public event AddEventHandler OnAdd;

        public delegate void DelEventHandler(MyList<T> list, T  item);
        public event DelEventHandler OnDelete;

        public Summary<T> Summary;
        public MyList()
        {
            Summary = new Summary<T>(this);
        }
        public new void   Add(T item) // "new" to avoid compiler-warnings, because we're hiding a method from base-class
        {
            base.Add(item);
            if (null != OnAdd)
            {
                OnAdd(this, new MyListAddEventArg
                {
                    _new = item as AbstractDomainObject

                }) ;
                
            }
            Summary.Invoke("HaveData");
            Summary.Invoke("NotHaveData");
            Summary.Invoke("HaveNewRecord");
            Summary.Invoke("NotHaveNewRecord");

        }
        
        public new void RemoveAt(int index)
        {
            var obj = this[index];
            base.RemoveAt(index);
            if (null != OnDelete )
            {
                OnDelete(this, obj);

                
            }
            Summary.Invoke("HaveData");
            Summary.Invoke("NotHaveData");
            Summary.Invoke("HaveNewRecord");
            Summary.Invoke("NotHaveNewRecord");
        }

        public bool HaveData
        {
            get
            {
                return this.Count > 0;

               // if (this.Count == 0)
               //     return false;

               //return  (this.Last() as AbstractDomainObject).isOld;
            }
        }
        public bool NotHaveData
        {
            get
            {
                return !HaveData;
            }

        }
        public bool HaveNewRecord
        {
            get
            {
                if (NotHaveData)
                    return false;

                return (this.Last() as AbstractDomainObject).isNew;
            }
        }
        public bool NotHaveNewRecord
        {
            get
            {
                return !HaveNewRecord;
            }
        }

        public bool isValidData
        {
            get
            {
                foreach(T obj in this)
                {
                    if (!(obj as AbstractDomainObject).isValid)
                        return false;
                }
                return true;
            }
        }

        public string Errors
        {
            get
            {
                string errors = "";
                foreach(T domain in this)
                {
                    errors += (domain as AbstractDomainObject ).Errors + System.Environment.NewLine;
                }
                return errors.Trim();
            }
        }

            }

    public class MyListAddEventArg:EventArgs
    {
        public AbstractDomainObject _new;
    }
    public class Summary<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void Invoke(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        MyList<T> _list = null;
        public Summary(MyList<T> list)
        {
            _list = list;
        }
        public bool HaveData
        {
            get
            {
                return _list.HaveData;
            }
        }
        public bool NotHaveData
        {
            get
            {
                return _list.NotHaveData;
            }

        }
        public bool HaveNewRecord
        {
            get
            {
                return _list.HaveNewRecord;
            }
        }
        public bool NotHaveNewRecord
        {
            get
            {
                return _list.NotHaveNewRecord;
            }
        }

        public bool isValidData
        {
            get
            {
                return _list.isValidData;
            }
        }
    }
}
