using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class SalesDetails
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SalesId { get; set; }
        public int StockBalanceId { get; set; }
        public int Qty { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public int PriceId { get; set; }
        public decimal SellPrice { get; set; }
        public decimal Amount { get; set; }

        public virtual ProductPrice Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual Sales Sales { get; set; }
    }
}
