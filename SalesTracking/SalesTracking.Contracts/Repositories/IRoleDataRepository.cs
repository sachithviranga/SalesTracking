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
        public Task<List<RoleDTO>> GetRoles();
        public Task<int> AddRole(RoleDTO role);
        
        public Task<RoleDTO> UpdateRole(RoleDTO role);

        public Task<RoleDTO> GetRoleById(int id);
    }
}
