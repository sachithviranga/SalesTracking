using SalesTracking.Common.Common;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Data.Repositories;
using SalesTracking.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Business.Managers
{
    public class MasterDataManager : IMasterDataManager
    {
        private readonly IMasterDataRepository _masterDataRepository;

        private readonly IMapper<Object, ServiceResponse> _serviceResponseMapper;

        public MasterDataManager(IMasterDataRepository masterDataRepository, IMapper<object, ServiceResponse> serviceResponseMapper)
        {
            _masterDataRepository = masterDataRepository;
            _serviceResponseMapper = serviceResponseMapper;
        }

        public async Task<ServiceResponse> GetPaymentTypes()
        {
            var returnObj = await _masterDataRepository.GetPaymentTypes();
            return _serviceResponseMapper.Map(returnObj);
        }
        public async Task<ServiceResponse> GetCustomerTypes()
        {
            var returnObj = await _masterDataRepository.GetCustomerTypes();
            return _serviceResponseMapper.Map(returnObj);
        }

        public async Task<ServiceResponse> GetModules()
        {
            return _serviceResponseMapper.Map(await _masterDataRepository.GetModules());
        }
    }
}
