using SalesTracking.Entities.Common;
using SalesTracking.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Managers
{
    public interface IStockManager
    {
        public Task<ServiceResponse> GetStock();

        public Task<ServiceResponse> AddStock(StockPurchaseDTO stock);

        public Task<ServiceResponse> UpdateStock(StockPurchaseDTO stock);

        public Task<ServiceResponse> GetStockPayment();

        public Task<ServiceResponse> GetStockById(int id);

        public Task<ServiceResponse> ApproveStock(StockPurchaseDTO stock);

        public Task<ServiceResponse> GetStockBySellprice(int id);
    }
}
