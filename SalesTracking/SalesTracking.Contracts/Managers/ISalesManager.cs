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
        public ServiceResponse GetSales();
        public ServiceResponse AddSales(SalesDTO sales);
        public ServiceResponse UpdateSales(SalesDTO sales);
        public ServiceResponse ApproveSales(SalesDTO sales);
        public ServiceResponse GetSalesById(int id);
    }
}
