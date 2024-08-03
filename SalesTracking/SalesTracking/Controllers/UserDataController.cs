using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesTracking.Business.Managers;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Customer;
using SalesTracking.Entities.User;

namespace SalesTracking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly IUserDataManager _userDataManager;

        public UserDataController(IUserDataManager userDataManager)
        {
            _userDataManager = userDataManager;
        }

        [HttpGet("GetUsers")]
        public ServiceResponse GetUsers()
        {
            return _userDataManager.GetUsers();
        }


        [HttpPost("AddUser")]
        public ServiceResponse AddUser([FromBody] UserDTO user) 
        {
            return _userDataManager.AddUser(user);
        }

        [HttpPost("UpdateUser")]

        public ServiceResponse UpdateUser([FromBody] UserDTO user)
        {
            return _userDataManager.UpdateUser(user);

        }

        [HttpGet("GetUserByUserId")]
        public ServiceResponse GetUserByUserId(int id)
        {
            return _userDataManager.GetUserByUserId(id);
        }


    }
}
