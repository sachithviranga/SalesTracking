using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Common
{
    public class LoginResponse
    {
        public bool CanLogin { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string LoginValidationMessage { get; set; }
        public long ExpiresIn { get; set; }
        public bool IsUserNotExist { get; set; }
    }
}
