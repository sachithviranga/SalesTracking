using SalesTracking.Entities.Product;
using SalesTracking.Entities.Sales;
using SalesTracking.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Repositories
{
    public interface IStockBalanceRepository
    {
        public int InsertStockBalance(List<StockBalanceDTO> StockBalances);

        public int UpdateStockBalance(List<StockBalanceDTO> Stockbalances, DateTime TransactionDate);

        public bool CheckStockBalance(List<SalesDetailsDTO> CheckstockBalances, DateTime TransactionDate, out List<ProductDTO> Products);

        public List<ProductQtyDTO> GetAvaibleProductQty(bool IsAllProduct = true);
    }
}
