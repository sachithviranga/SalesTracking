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
        public Task<ServiceResponse> GetRoles();

        public Task<ServiceResponse> AddRole(RoleDTO role);

        public Task<ServiceResponse> UpdateRole(RoleDTO role);

        public Task<ServiceResponse> GetRoleById(int id);
    }
}
