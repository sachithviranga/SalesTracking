using SalesTracking.Common.Common;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Data.Repositories;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Customer;
using System;

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

        public ServiceResponse GetCustomers()
        {
            var returnObj = _customerDataRepository.GetCustomers();
            return _serviceResponseMapper.Map(returnObj);
        }

        public ServiceResponse AddCustomer(CustomerDTO customer) 
        {
            customer.CreateDate = DateTime.UtcNow;
            customer.CreateBy = UserContext.Current;
            return _serviceResponseMapper.Map(_customerDataRepository.AddCustomer(customer));
        }

        public ServiceResponse UpdateCustomer(CustomerDTO customer)
        {
            customer.UpdateDate = DateTime.UtcNow;
            customer.UpdateBy = UserContext.Current;
            return _serviceResponseMapper.Map(_customerDataRepository.UpdateCustomer(customer));
        }

        public ServiceResponse GetCustomerById(int id)
        {
            var returnObj = _customerDataRepository.GetCustomerById(id);
            return _serviceResponseMapper.Map(returnObj);
        }
    }
}
