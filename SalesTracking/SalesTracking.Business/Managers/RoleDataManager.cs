using SalesTracking.Common.Common;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Data.Repositories;
using SalesTracking.DataContext;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Product;
using SalesTracking.Entities.Stock;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Business.Managers
{
    public class RoleDataManager : IRoleDataManager
    {
        private readonly IRoleDataRepository _roleDataRepository;

        private readonly IMapper<Object, ServiceResponse> _serviceResponseMapper;

        public RoleDataManager(IRoleDataRepository roleDataRepository, IMapper<object, ServiceResponse> serviceResponseMapper)
        {
            _roleDataRepository = roleDataRepository;
            _serviceResponseMapper = serviceResponseMapper;
        }

        public async Task<ServiceResponse> AddRole(RoleDTO role)
        {
            role.CreateDate = DateTime.UtcNow;
            role.CreateBy = UserContext.Current;

            Parallel.ForEach(role.RoleClaim, a =>
            {
                a.Id = 0;
                a.IsActive = true;
                a.CreateBy = UserContext.Current;
                a.CreateDate = DateTime.UtcNow;
            });

            return _serviceResponseMapper.Map(await _roleDataRepository.AddRole(role));

        }

        public async Task<ServiceResponse> GetRoles()
        {
            var returnObj = await _roleDataRepository.GetRoles();
            return _serviceResponseMapper.Map(returnObj);
        }

        public async Task<ServiceResponse> UpdateRole(RoleDTO role)
        {
            role.UpdateDate = DateTime.UtcNow;
            role.UpdateBy = UserContext.Current;

            Parallel.ForEach(role.RoleClaim, a =>
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

            return _serviceResponseMapper.Map(await _roleDataRepository.UpdateRole(role));
        }

        public async Task<ServiceResponse> GetRoleById(int id)
        {
            var returnObj =await _roleDataRepository.GetRoleById(id);
            return _serviceResponseMapper.Map(returnObj);
        }
    }
}
