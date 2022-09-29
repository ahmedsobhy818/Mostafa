using Core.Base_Classes;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Tables
{
    public partial class Cleaning:EntityBase
    {
       public int? SiteId { get; set; }
        public int? PlaceId { get; set; }
        public int? ActivityId { get; set; }
        public DateTime? CleanStartDate { get; set; }
        public DateTime? CleanEndDate { get; set; }
        public double? Cost { get; set; }
    }
}
