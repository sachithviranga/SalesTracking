using SalesTracking.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Contracts.Managers
{
    public interface IDashboardManager
    {
        public ServiceResponse GetAvailableProdcuts();
    }
}
