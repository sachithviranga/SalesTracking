using SalesTracking.Entities.Auth;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Auth
{
    public interface IAuthHelper
    {
        public string GenerateToken(UserDTO user);

        public string EncryptPassword(string password);

        public bool VerifyPassword(string password, string passwordHash);
    }
}
