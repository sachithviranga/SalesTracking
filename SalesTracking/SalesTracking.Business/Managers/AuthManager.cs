using SalesTracking.Auth;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Entities.Auth;
using SalesTracking.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Business.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly IAuthHelper _authHelper;

        private readonly IUserRepository _userRepository;

        private readonly IMapper<Object, ServiceResponse> _serviceResponseMapper;

        public AuthManager(IAuthHelper authHelper, IUserRepository userRepository, IMapper<object, ServiceResponse> serviceResponseMapper)
        {
            _authHelper = authHelper;
            _userRepository = userRepository;
            _serviceResponseMapper = serviceResponseMapper;
        }

        public LoginResponse Login(LoginDTO login)
        {
            var user = _userRepository.GetUserByUserName(login.UserName);
            string token = string.Empty;
            bool canLogin = false;
            string errorMessage = string.Empty;
            if (user != null && _authHelper.VerifyPassword(login.PassWord, user.Password))
            {
                token = _authHelper.GenerateToken(user);
                canLogin   = true;
            }
            else
            {
                errorMessage = "The user name or password is incorrect";
            }

            return new LoginResponse
            {
                AccessToken = token,
                CanLogin = canLogin,
                LoginValidationMessage = errorMessage,
            };
        }
    }
}
