using SalesTracking.Entities.Customer;
using SalesTracking.Entities.Sales;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Repositories
{
    public interface IRoleDataRepository
    {
        public List<RoleDTO> GetRoles();
        public int AddRole(RoleDTO role);
        
        public RoleDTO UpdateRole(RoleDTO role);

        public RoleDTO GetRoleById(int id);
    }
}
