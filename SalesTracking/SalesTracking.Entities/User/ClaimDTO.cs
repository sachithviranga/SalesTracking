using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.User
{
    public class ClaimDTO : BaseDTO
    {
        public string ClaimName { get; set; }
        public int ModuleId { get; set; }

        public List<RoleClaimDTO> RoleClaim { get; set; }

        //public List<UserRoleDTO> UserRole { get; set; }
    }
}
