using SalesTracking.Entities.Auth;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Sales;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Managers
{
    public interface IUserDataManager
    {
        public Task<ServiceResponse> GetUsers();

        public Task<ServiceResponse> AddUser(UserDTO user);

        public Task<ServiceResponse> UpdateUser(UserDTO user);

        public Task<ServiceResponse> GetUserByUserId(int id);
    }
}
