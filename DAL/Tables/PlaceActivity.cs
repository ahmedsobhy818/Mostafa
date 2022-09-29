using Core.Base_Classes;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Tables
{
    public partial class PlaceActivity: EntityBase
    {
        public int ActivityId { get; set; }
        public int PlaceId { get; set; }
    }
}
