using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class StockBalance
    {
        public int Id { get; set; }
        public int ReferenceType { get; set; }
        public int ReferenceId { get; set; }
        public int ReferenceLineId { get; set; }
        public string BatchId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SellPrice { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }

        public virtual Product Product { get; set; }
    }
}
