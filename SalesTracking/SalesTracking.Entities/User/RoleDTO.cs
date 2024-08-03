using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.User
{
    public class RoleDTO : BaseDTO
    {
        public string RoleName { get; set; }

        public List<RoleClaimDTO> RoleClaim { get; set; }

        public List<UserRoleDTO> UserRole { get; set; }
    }
}
