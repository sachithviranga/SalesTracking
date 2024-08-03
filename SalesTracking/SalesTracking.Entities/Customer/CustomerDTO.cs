using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.Customer
{
    public class CustomerDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int PhoneNo { get; set; }
        public int CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
    }
}
