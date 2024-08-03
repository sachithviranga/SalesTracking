﻿using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class Module
    {
        public Module()
        {
            Claim = new HashSet<Claim>();
        }

        public int Id { get; set; }
        public string ModuleName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual ICollection<Claim> Claim { get; set; }
    }
}
