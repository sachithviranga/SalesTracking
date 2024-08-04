using SalesTracking.Common.Common;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Data.Repositories;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Customer;
using System;
using System.Threading.Tasks;

namespace SalesTracking.Business.Managers
{
    public class CustomerDataManager : ICustomerDataManager
    {
        private readonly ICustomerDataRepository _customerDataRepository;

        private readonly IMapper<Object, ServiceResponse> _serviceResponseMapper;

        public CustomerDataManager(ICustomerDataRepository customerDataRepository, IMapper<object, ServiceResponse> serviceResponseMapper)
        {
            _customerDataRepository = customerDataRepository;
            _serviceResponseMapper = serviceResponseMapper;
        }

        public async Task<ServiceResponse> GetCustomers()
        {
            var returnObj = await _customerDataRepository.GetCustomers();
            return _serviceResponseMapper.Map(returnObj);
        }

        public async Task<ServiceResponse> AddCustomer(CustomerDTO customer)
        {
            customer.CreateDate = DateTime.UtcNow;
            customer.CreateBy = UserContext.Current;
            return _serviceResponseMapper.Map(await _customerDataRepository.AddCustomer(customer));
        }

        public async Task<ServiceResponse> UpdateCustomer(CustomerDTO customer)
        {
            customer.UpdateDate = DateTime.UtcNow;
            customer.UpdateBy = UserContext.Current;
            return _serviceResponseMapper.Map(await _customerDataRepository.UpdateCustomer(customer));
        }

        public async Task<ServiceResponse> GetCustomerById(int id)
        {
            var returnObj = await _customerDataRepository.GetCustomerById(id);
            return _serviceResponseMapper.Map(returnObj);
        }
    }
}
