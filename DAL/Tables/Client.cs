using Core.Base_Classes;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Tables
{
    public partial class Client: EntityBase
    {
        public string ClientName { get; set; }
        public string Responsible { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string Siret { get; set; }
        public string Ape { get; set; }
        public byte[] Photo { get; set; }

        public override string ToString()
        {
            return this.Id + "-" + this.ClientName + "-" + this.Responsible + "-" + this.Tel + "-" + this.Fax + "-" + this.Mail  +"-" + this.Address + "-" + this.Siret + "-" +  this.Ape + "-" + this.Photo.ToString();
        }
    }
}
