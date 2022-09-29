using Core.Base_Classes;
using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Tables
{
    public partial class User: EntityBase
    {
        public string UserName { get; set; }
        public int? AccessGroupId { get; set; }
        public string Pass { get; set; }
    }
}
