using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class ProductPrice
    {
        public ProductPrice()
        {
            SalesDetails = new HashSet<SalesDetails>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal SellPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime StartData { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<SalesDetails> SalesDetails { get; set; }
    }
}
