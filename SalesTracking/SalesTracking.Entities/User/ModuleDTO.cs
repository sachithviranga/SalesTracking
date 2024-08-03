using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Entities.User
{
    public class ModuleDTO : BaseDTO
    {
        public string ModuleName { get; set; }

        public List<ClaimDTO> Claim { get; set; }
    }
}
