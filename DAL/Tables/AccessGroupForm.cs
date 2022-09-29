using Core.Base_Classes;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Tables
{
    public partial class AccessGroupForm: EntityBase
    {
        public int FormId { get; set; }
        public bool? CanView { get; set; }
        public bool? CanEdit { get; set; }
    }
}
