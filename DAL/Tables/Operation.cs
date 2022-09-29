using Core.Base_Classes;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Tables
{
    public partial class Operation: EntityBase
    {
        public int SiteId { get; set; }
        public int PlaceId { get; set; }
        public int ActivityId { get; set; }
        public double? Cost { get; set; }
        public int? FrequencyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
