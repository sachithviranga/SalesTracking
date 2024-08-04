using SalesTracking.Entities.Auth;
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
    public interface IUserRepository
    {
        public Task<UserDTO> GetUserByUserName(string email);
        public Task<List<UserDTO>> GetUsers();

        public Task<int> AddUser(UserDTO user);

        public Task<UserDTO> UpdateUser(UserDTO user);

        public Task<UserDTO> GetUserByUserId(int id);
    }
}
