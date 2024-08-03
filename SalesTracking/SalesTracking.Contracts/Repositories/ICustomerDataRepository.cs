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
        public List<CustomerDTO> GetCustomers();
        public int AddCustomer(CustomerDTO customer);

        public CustomerDTO UpdateCustomer(CustomerDTO customer);

        public CustomerDTO GetCustomerById(int id);
    }

    
}
