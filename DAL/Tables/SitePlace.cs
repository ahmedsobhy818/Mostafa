using Core.Base_Classes;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Tables
{
    public partial class SitePlace: EntityBase
    {
        public int SiteId { get; set; }
        public int PlaceId { get; set; }
        public double? Area { get; set; }
        public double? Distance { get; set; }
    }
}
