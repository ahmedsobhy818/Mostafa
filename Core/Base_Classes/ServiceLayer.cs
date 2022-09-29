using Core.Abstract_Classes;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base_Classes
{
    

    public class ServiceLayer<T> : AbstractServiceLayer, IServiceLayer<T> where T : class
    {
        
        public ServiceLayer()
        {
           
        }
        public override void  Save()
        {
            //base.Save();
             UnitOfWork.Save();
        }
       
        

    }
}
