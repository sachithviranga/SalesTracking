using SalesTracking.Entities.Sales;
using SalesTracking.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Repositories
{
    public interface ISalesRepository
    {
        public Task<List<SalesDTO>> GetSales();
        public Task<int> AddSales(SalesDTO sales);
        public Task<SalesDTO> UpdateSales(SalesDTO sales);
        public Task<SalesDTO> ApproveSales(SalesDTO sales);
        public Task<SalesDTO> GetSalesById(int id);

       
    }
}
