using SalesTracking.Entities.Customer;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Repositories
{
    public  interface ICustomerDataRepository
    {
        public Task<List<CustomerDTO>> GetCustomers();
        public Task<int> AddCustomer(CustomerDTO customer);

        public Task<CustomerDTO> UpdateCustomer(CustomerDTO customer);

        public Task<CustomerDTO> GetCustomerById(int id);
    }

    
}
