using SalesTracking.Entities.Common;
using SalesTracking.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Managers
{
    public interface ISalesManager
    {
        public Task<ServiceResponse> GetSales();
        public Task<ServiceResponse> AddSales(SalesDTO sales);
        public Task<ServiceResponse> UpdateSales(SalesDTO sales);
        public Task<ServiceResponse> ApproveSales(SalesDTO sales);
        public Task<ServiceResponse> GetSalesById(int id);
    }
}
