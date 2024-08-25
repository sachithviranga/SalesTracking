using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Data.Repositories;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Business.Managers
{
    public class DashboardManager : IDashboardManager
    {
        private readonly IStockBalanceRepository _stockBalanceRepository;

        private readonly IMapper<Object, ServiceResponse> _serviceResponseMapper;

        public DashboardManager(IStockBalanceRepository stockBalanceRepository, IMapper<object, ServiceResponse> serviceResponseMapper)
        {
            _stockBalanceRepository = stockBalanceRepository;
            _serviceResponseMapper = serviceResponseMapper;
        }

        public async Task<List<ProductQtyDTO>> GetAvailableProdcuts()
        {
            return await _stockBalanceRepository.GetAvaibleProductQty();
        }
    }
}
