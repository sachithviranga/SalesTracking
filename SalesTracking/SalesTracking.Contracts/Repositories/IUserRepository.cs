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
        public UserDTO GetUserByUserName(string email);
        public List<UserDTO> GetUsers();

        public int AddUser(UserDTO user);

        public UserDTO UpdateUser(UserDTO user);

        public UserDTO GetUserByUserId(int id);
    }
}
