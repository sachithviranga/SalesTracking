using System;
using System.Collections.Generic;

namespace SalesTracking.DataContext
{
    public partial class StatusHistory
    {
        public int Id { get; set; }
        public int ReferenceType { get; set; }
        public int ReferenceId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
    }
}
