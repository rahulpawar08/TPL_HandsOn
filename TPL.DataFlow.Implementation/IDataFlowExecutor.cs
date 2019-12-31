using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TPL.DataFlow.Implementation
{
    public interface IDataFlowExecutor
    {
        Task<bool> Start();

        bool Start(IEnumerable<string> supplierHotelIds);
    }
}
