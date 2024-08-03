using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class Customer
    {
        public Customer()
        {
            Sales = new HashSet<Sales>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal PhoneNo { get; set; }
        public int CustomerTypeId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        public virtual CustomerType CustomerType { get; set; }
        public virtual ICollection<Sales> Sales { get; set; }
    }
}
