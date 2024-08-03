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
        public List<PaymentTypeDTO> GetPaymentTypes();
        public List<CustomerTypeDTO> GetCustomerTypes();
        public List<ModuleDTO>GetModules();
        
    }
}
