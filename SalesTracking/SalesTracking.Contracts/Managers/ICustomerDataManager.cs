using SalesTracking.Entities.Common;
using SalesTracking.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Managers
{
    public interface ICustomerDataManager
    {
        public Task<ServiceResponse> GetCustomers();

        public Task<ServiceResponse> AddCustomer(CustomerDTO customer);

        public Task<ServiceResponse> UpdateCustomer(CustomerDTO customer);

        public Task<ServiceResponse> GetCustomerById(int id);
    }
}
