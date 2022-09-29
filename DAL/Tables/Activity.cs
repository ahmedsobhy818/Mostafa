using Core.Base_Classes;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Tables
{
    public partial class Activity:EntityBase
    {
        public string ActivityName { get; set; }
        public double? UnitCost { get; set; }
    }
}
