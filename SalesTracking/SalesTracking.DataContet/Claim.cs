using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class Claim
    {
        public Claim()
        {
            RoleClaim = new HashSet<RoleClaim>();
        }

        public int Id { get; set; }
        public string ClaimName { get; set; }
        public int ModuleId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual Module Module { get; set; }
        public virtual ICollection<RoleClaim> RoleClaim { get; set; }
    }
}
