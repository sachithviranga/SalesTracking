using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Product;
using SalesTracking.Entities.User;

namespace SalesTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleDataController : ControllerBase
    {
        private readonly IRoleDataManager _roleDataManager;

        public RoleDataController(IRoleDataManager roleDataManager)
        {
            _roleDataManager = roleDataManager;
        }

        
        [HttpGet("GetRoles")]
        public ServiceResponse GetRoles()
        {
            return _roleDataManager.GetRoles();
        }

        [HttpPost("AddRole")]
        public ServiceResponse AddRole([FromBody] RoleDTO role)
        {
            return _roleDataManager.AddRole(role);
        }

        [HttpPost("UpdateRole")]

        public ServiceResponse UpdateRole([FromBody] RoleDTO role)
        {
            return _roleDataManager.UpdateRole(role);

        }


        [HttpGet("GetRoleById")]
        public ServiceResponse GetRoleById(int id)
        {
            return _roleDataManager.GetRoleById(id);
        }
    }
}
