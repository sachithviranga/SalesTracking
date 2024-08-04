using SalesTracking.Entities.Customer;
using SalesTracking.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Repositories
{
    public interface IStockRepository
    {
        public Task<List<StockPurchaseDTO>> GetStock();
        public Task<int> AddStock(StockPurchaseDTO stock);
        public Task<StockPurchaseDTO> UpdateStock(StockPurchaseDTO stock);
        public Task<List<StockPurchaseDTO>> GetStockPayment();
        public Task<StockPurchaseDTO> GetStockById(int id);

        public Task<StockPurchaseDTO> ApproveStock(StockPurchaseDTO stock);

        public Task<List<StockBalanceDTO>> GetStockBySellprice(int id);
    }
}
