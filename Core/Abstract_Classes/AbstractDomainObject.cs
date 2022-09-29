using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract_Classes
{
    public abstract class AbstractDomainObject {
        public  AbstractServiceLayer ServiceLayer { get; set; }
        public int Id { get; set; }
        public string Errors
        {
            get
            {
                string s = "";
                if (ErrorsList == null)
                    return "";
                foreach (IGrouping<string,DomainError> g in ErrorsList.GroupBy(e=>e.errorMessage))
                {
                    s += g.Key  + System.Environment.NewLine ;
                }
                return s;
            }
        }
       public string GetErrorsForProperty(string property)
        {
            if (ErrorsList == null)
                return "";
           var errors= ErrorsList.Where(e => e.propertyName == property).ToList();
            string s = "";
            foreach (DomainError error in errors)
            {
                s += error.errorMessage + System.Environment.NewLine ;
            }
            return s;
        }
        protected virtual List<DomainError> ErrorsList
        {
            get
            {
                return null;
            }
        }
        public List<string> ErrorsStrings
        {
            get
            {
               return  Errors.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
        }
        public AbstractDomainObject()
        {
            Id = -1;
            

        }

        public bool  isOld
        {
            get
            {
                return Id > 0;
            }
        }
        public bool isNew
        {
            get
            {
                return !isOld;
            }
        }
        public virtual bool isValid
        { 
            get 
            {
                return true;
            }
        }
        public virtual bool canDelete
        {
            get
            {
                return true;
            }
        }
    }

    public class DomainError
    {
        public string propertyName;
        public string errorMessage;
    }
}
