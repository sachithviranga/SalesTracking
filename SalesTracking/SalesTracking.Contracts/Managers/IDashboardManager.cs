using SalesTracking.Entities.Common;
using SalesTracking.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Managers
{
    public interface IDashboardManager
    {
        public Task<List<ProductQtyDTO>> GetAvailableProdcuts();
    }
}
