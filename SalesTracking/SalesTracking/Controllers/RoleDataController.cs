using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Product;
using SalesTracking.Entities.User;
using System.Threading.Tasks;

namespace SalesTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleDataController : ControllerBase
    {
        private readonly IRoleDataManager _roleDataManager;

        public RoleDataController(IRoleDataManager roleDataManager)
        {
            _roleDataManager = roleDataManager;
        }

        
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _roleDataManager.GetRoles());
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] RoleDTO role)
        {
            return Ok(await _roleDataManager.AddRole(role));
        }

        [HttpPost("UpdateRole")]

        public async Task<IActionResult> UpdateRole([FromBody] RoleDTO role)
        {
            return Ok(await _roleDataManager.UpdateRole(role));

        }


        [HttpGet("GetRoleById")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            return Ok(await _roleDataManager.GetRoleById(id));
        }
    }
}
