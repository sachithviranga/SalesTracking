using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class RoleClaim
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int ClaimId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual Claim Claim { get; set; }
        public virtual Role Role { get; set; }
    }
}
