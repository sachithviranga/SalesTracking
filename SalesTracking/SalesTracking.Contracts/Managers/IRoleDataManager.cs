using SalesTracking.Entities.Common;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Managers
{
    public interface IRoleDataManager
    {
        public ServiceResponse GetRoles();

        public ServiceResponse AddRole(RoleDTO role);

        public ServiceResponse UpdateRole(RoleDTO role);

        public ServiceResponse GetRoleById(int id);
    }
}
