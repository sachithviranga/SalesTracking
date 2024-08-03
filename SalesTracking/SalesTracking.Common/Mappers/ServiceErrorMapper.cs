using SalesTracking.Contracts.Common;
using SalesTracking.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Common.Mappers
{
    public class ServiceErrorMapper : IMapper<IList<Message>, ServiceResponse>
    {
        public ServiceResponse Map(IList<Message> input) => new ServiceResponse
        {
            IsError = true,
            Messages = input
        };
    }
}
