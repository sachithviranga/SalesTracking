using SalesTracking.Auth;
using SalesTracking.Common.Common;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Data.Repositories;
using SalesTracking.Entities.Auth;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Sales;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Business.Managers
{
    public class UserDataManager : IUserDataManager
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper<Object, ServiceResponse> _serviceResponseMapper;

        private readonly IAuthHelper _authHelper;

        public UserDataManager(IUserRepository userRepository, IMapper<object, ServiceResponse> serviceResponseMapper, IAuthHelper authHelper)
        {
            _userRepository = userRepository;
            _serviceResponseMapper = serviceResponseMapper;
            _authHelper = authHelper;
        }

        public async Task<ServiceResponse> GetUsers()
        {
            var returnObj = await _userRepository.GetUsers();
            return  _serviceResponseMapper.Map(returnObj);
        }

        public async Task<ServiceResponse> AddUser(UserDTO user)
        {
            user.Id = 0;
            user.Password = _authHelper.EncryptPassword(user.Password);
            user.CreateDate = DateTime.UtcNow;
            user.CreateBy = UserContext.Current;

            Parallel.ForEach(user.UserRole, a =>
            {
                a.Id = 0;
                a.IsActive = true;
                a.CreateBy = UserContext.Current;
                a.CreateDate = DateTime.UtcNow;
            });
    
            return _serviceResponseMapper.Map(await _userRepository.AddUser(user));

        }

        public async Task<ServiceResponse> GetUserByUserId( int id)
        {
            var returnObj = await _userRepository.GetUserByUserId(id);
            return _serviceResponseMapper.Map(returnObj);
        }


        public async Task<ServiceResponse> UpdateUser(UserDTO user)
        {
            user.UpdateDate = DateTime.UtcNow;
            user.UpdateBy = UserContext.Current;
            Parallel.ForEach(user.UserRole, a =>
            {
                if (a.Id > 0)
                {
                    a.UpdateBy = UserContext.Current;
                    a.UpdateDate = DateTime.UtcNow;
                }
                else
                {
                    a.IsActive = true;
                    a.CreateBy = UserContext.Current;
                    a.CreateDate = DateTime.UtcNow;
                }
            });
            return _serviceResponseMapper.Map(await _userRepository.UpdateUser(user));
        }

    }
}
