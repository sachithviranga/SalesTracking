using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Business.Managers;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Customer;
using SalesTracking.Entities.User;
using System.Threading.Tasks;

namespace SalesTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserDataController : ControllerBase
    {
        private readonly IUserDataManager _userDataManager;

        public UserDataController(IUserDataManager userDataManager)
        {
            _userDataManager = userDataManager;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userDataManager.GetUsers());
        }


        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDTO user) 
        {
            return Ok(await _userDataManager.AddUser(user));
        }

        [HttpPost("UpdateUser")]

        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
        {
            return Ok(await _userDataManager.UpdateUser(user));

        }

        [HttpGet("GetUserByUserId")]
        public async Task<IActionResult> GetUserByUserId(int id)
        {
            return Ok(await _userDataManager.GetUserByUserId(id));
        }
    }
}
