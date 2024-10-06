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
        public Task<int> InsertStockBalance(List<StockBalanceDTO> StockBalances);

        public Task<int> UpdateStockBalance(List<StockBalanceDTO> Stockbalances, DateTime TransactionDate);

        public Task<(bool, List<ProductDTO>)> CheckStockBalance(List<SalesDetailsDTO> CheckstockBalances, DateTime TransactionDate);

        public Task<List<ProductQtyDTO>> GetAvaibleProductQty(bool IsAllProduct = true);
    }
}
