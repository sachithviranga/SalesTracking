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
        public ServiceResponse GetUsers();

        public ServiceResponse AddUser(UserDTO user);

        public ServiceResponse UpdateUser(UserDTO user);

        public ServiceResponse GetUserByUserId(int id);
    }
}
