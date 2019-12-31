using System;
using System.Collections.Generic;
using System.Text;

namespace TPL.DataFlow.Implementation
{
    public interface IDataFlowExecutor
    {
        bool Start();

        bool Start(IEnumerable<string> supplierHotelIds);
    }
}
