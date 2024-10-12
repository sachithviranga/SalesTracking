using SalesTracking.Common.Common;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Data.Repositories;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Customer;
using System;
using System.Collections.Generic;
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

        public async Task<List<CustomerDTO>> GetCustomers()
        {
            return await _customerDataRepository.GetCustomers();
        }

        public async Task<int> AddCustomer(CustomerDTO customer)
        {
            customer.CreateDate = DateTime.UtcNow;
            customer.CreateBy = UserContext.Current;
            return await _customerDataRepository.AddCustomer(customer);
        }

        public async Task<CustomerDTO> UpdateCustomer(CustomerDTO customer)
        {
            customer.UpdateDate = DateTime.UtcNow;
            customer.UpdateBy = UserContext.Current;
            return await _customerDataRepository.UpdateCustomer(customer);
        }

        public async Task<CustomerDTO> GetCustomerById(int id)
        {
            return await _customerDataRepository.GetCustomerById(id);
        }
    }
}
