using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Common.Common
{
    public static class GenerateBatch
    {
        public static string GenerateBatchId(int ReferenceType , int ProductId)
        {
           return ReferenceType.ToString() + ProductId.ToString() + DateTime.Now.ToString("yyMMddHHmmssFFF");
        }
    }
}
