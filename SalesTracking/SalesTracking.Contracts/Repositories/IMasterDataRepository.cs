using SalesTracking.Entities.MasterData;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Repositories
{
    public interface IMasterDataRepository
    {
        public Task<List<PaymentTypeDTO>> GetPaymentTypes();
        public Task<List<CustomerTypeDTO>> GetCustomerTypes();
        public Task<List<ModuleDTO>> GetModules();

    }
}
