using Core.Base_Classes;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Tables
{
    public partial class AccessGroup:EntityBase
    {
        public string AccessGroupName { get; set; }
    }
}
