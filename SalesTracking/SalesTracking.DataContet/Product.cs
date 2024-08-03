using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class Product
    {
        public Product()
        {
            ProductPrice = new HashSet<ProductPrice>();
            SalesDetails = new HashSet<SalesDetails>();
            StockBalance = new HashSet<StockBalance>();
            StockPurchaseDetails = new HashSet<StockPurchaseDetails>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string Createby { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Updateby { get; set; }

        public virtual ICollection<ProductPrice> ProductPrice { get; set; }
        public virtual ICollection<SalesDetails> SalesDetails { get; set; }
        public virtual ICollection<StockBalance> StockBalance { get; set; }
        public virtual ICollection<StockPurchaseDetails> StockPurchaseDetails { get; set; }
    }
}
