using Core.Abstract_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessObjects
{
    public class doClient : AbstractDomainObject
    {
        public string ClientName { get; set; }
        public string Responsible { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string Siret { get; set; }
        public string Ape { get; set; }
        public string Photo { get; set; }

        public override bool canDelete => base.canDelete;
    }
}
