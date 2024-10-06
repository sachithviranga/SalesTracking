using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesTracking.Contracts.Repositories;
using SalesTracking.DataContext;
using SalesTracking.Entities.MasterData;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Data.Repositories
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public MasterDataRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PaymentTypeDTO>> GetPaymentTypes()
        {
            var paymentTypes = await _context.PaymentType.Where(a => a.IsActive == true).ToListAsync();
            return _mapper.Map<List<PaymentTypeDTO>>(paymentTypes);
        }

        public async Task<List<CustomerTypeDTO>> GetCustomerTypes()
        {
            var customerTypes = await _context.CustomerType.Where(a => a.IsActive == true).ToListAsync();
            return _mapper.Map<List<CustomerTypeDTO>>(customerTypes);
        }

        public async Task<List<ModuleDTO>> GetModules()
        {

            var modules = await _context.Module.Where(a => a.IsActive == true)
                                    .Include(i => i.Claim)
                                    .ToListAsync();
            return _mapper.Map<List<ModuleDTO>>(modules);

        }
    }
}
