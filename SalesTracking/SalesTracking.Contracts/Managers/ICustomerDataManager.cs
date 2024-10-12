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
        public Task<List<CustomerDTO>> GetCustomers();

        public Task<int> AddCustomer(CustomerDTO customer);

        public Task<CustomerDTO> UpdateCustomer(CustomerDTO customer);

        public Task<CustomerDTO> GetCustomerById(int id);
    }
}
