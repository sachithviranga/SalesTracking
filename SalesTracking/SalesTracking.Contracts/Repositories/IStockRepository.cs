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
        public List<StockPurchaseDTO> GetStock();
        public int AddStock(StockPurchaseDTO stock);
        public StockPurchaseDTO UpdateStock(StockPurchaseDTO stock);
        public List<StockPurchaseDTO> GetStockPayment();
        public StockPurchaseDTO GetStockById(int id);

        public StockPurchaseDTO ApproveStock(StockPurchaseDTO stock);

        public List<StockBalanceDTO> GetStockBySellprice(int id);
    }
}
