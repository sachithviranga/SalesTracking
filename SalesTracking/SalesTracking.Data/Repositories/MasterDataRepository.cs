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

        public List<PaymentTypeDTO> GetPaymentTypes()
        {
            try
            {
                var paymentTypes = _context.PaymentType.Where(a => a.IsActive == true).ToList();
                return _mapper.Map<List<PaymentTypeDTO>>(paymentTypes);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CustomerTypeDTO> GetCustomerTypes()
        {
            try
            {
                var customerTypes = _context.CustomerType.Where(a => a.IsActive == true).ToList();

                return _mapper.Map<List<CustomerTypeDTO>>(customerTypes);


            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ModuleDTO> GetModules()
        {
            try
            {
                var modules = _context.Module.Where(a => a.IsActive == true)
                                        .Include(i => i.Claim)
                                        .ToList();

                return _mapper.Map<List<ModuleDTO>>(modules);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
