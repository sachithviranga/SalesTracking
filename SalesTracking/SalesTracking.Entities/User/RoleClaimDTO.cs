using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.User
{
    public class RoleClaimDTO : BaseDTO
    {
        public int RoleId { get; set; }
        public int ClaimId { get; set; }
    }
}
