using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesTracking.Contracts.Repositories;
using SalesTracking.DataContext;
using SalesTracking.Entities.Customer;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Data.Repositories
{
    public class CustomerDataRepository : ICustomerDataRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public CustomerDataRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CustomerDTO>> GetCustomers()
        {
            var customers = await _context.Customer
                                .Where(a => a.IsActive == true)
                                .Include(i => i.CustomerType).ToListAsync();

            return _mapper.Map<List<CustomerDTO>>(customers);

        }

        public async Task<int> AddCustomer(CustomerDTO customer)
        {
            var saveObj = _mapper.Map<Customer>(customer);
            await _context.Customer.AddAsync(saveObj);
            await _context.SaveChangesAsync();
            return saveObj.Id;
        }

        public async Task<CustomerDTO> UpdateCustomer(CustomerDTO customer)
        {

            var updateObj = await _context.Customer.FirstOrDefaultAsync(a => a.Id == customer.Id);
            if (updateObj != null)
            {
                updateObj.Name = customer.Name;
                updateObj.Address = customer.Address;
                updateObj.PhoneNo = customer.PhoneNo;
                updateObj.CustomerTypeId = customer.CustomerTypeId;
                updateObj.IsActive = customer.IsActive;
                updateObj.UpdateBy = customer.UpdateBy;
                updateObj.UpdateDate = customer.UpdateDate;
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<CustomerDTO>(updateObj);

        }

        public async Task<CustomerDTO> GetCustomerById(int id)
        {

            var customer = await _context.Customer.Where(a => a.Id == id)
                                .SingleOrDefaultAsync();

            return _mapper.Map<CustomerDTO>(customer);

        }


    }
}
