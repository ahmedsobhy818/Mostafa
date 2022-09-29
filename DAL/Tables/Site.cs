using Core.Base_Classes;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Tables
{
    public partial class Site: EntityBase
    {
        public string SiteName { get; set; }
        public string ClientId { get; set; }
        public string Contact { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Mail { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        public string ContractNo { get; set; }
        public DateTime? ContractDate { get; set; }
        public string Version { get; set; }
        public string Consultant { get; set; }
        public string ConsultantTel { get; set; }
        public string ConsultantIntern { get; set; }
        public string ConsultantMail { get; set; }
    }
}
