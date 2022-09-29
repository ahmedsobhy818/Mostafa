using Core.Abstract_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Core.Base_Classes
{
    public class EntityBase
    {
        //public Guid Id { get; set; }
        public int Id { get; set; }

        [NotMapped]
        public AbstractDomainObject BussinesObjet { get; set; }

       [NotMapped]
       public bool Changed { get; set; }

       
    }
}
