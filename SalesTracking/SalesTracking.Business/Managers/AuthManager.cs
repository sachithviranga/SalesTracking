using SalesTracking.Auth;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Entities.Auth;
using SalesTracking.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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

        public async Task<LoginResponse> Login(LoginDTO login)
        {
            var user = await _userRepository.GetUserByUserName(login.UserName);
            string token = string.Empty;
            string errorMessage = string.Empty;
            if (user != null && _authHelper.VerifyPassword(login.PassWord, user.Password))
            {
                token = _authHelper.GenerateToken(user);
                return new LoginResponse
                {
                    AccessToken = token,
                    CanLogin = true,
                    LoginValidationMessage = errorMessage,
                };
            }
            else
            {
                errorMessage = "The user name or password is incorrect";
                throw new InvalidCredentialException(errorMessage);
            }
        }
    }
}
