using Core.Base_Classes;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Tables
{
    public partial class Frequency: EntityBase
    {
         public string FrequencyName { get; set; }
        public string YearMonthWeek { get; set; }
        public int? DaysNumber { get; set; }
    }
}
