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
        public ServiceResponse GetStock();

        public ServiceResponse AddStock(StockPurchaseDTO stock);

        public ServiceResponse UpdateStock(StockPurchaseDTO stock);

        public ServiceResponse GetStockPayment();

        public ServiceResponse GetStockById(int id);

        public ServiceResponse ApproveStock(StockPurchaseDTO stock);

        public ServiceResponse GetStockBySellprice(int id);
    }
}
