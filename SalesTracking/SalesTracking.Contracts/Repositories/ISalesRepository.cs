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
        public List<SalesDTO> GetSales();
        public int AddSales(SalesDTO sales);
        public SalesDTO UpdateSales(SalesDTO sales);
        public SalesDTO ApproveSales(SalesDTO sales);
        public SalesDTO GetSalesById(int id);

       
    }
}
