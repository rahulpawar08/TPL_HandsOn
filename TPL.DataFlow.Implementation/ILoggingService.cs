using System;
using System.Collections.Generic;
using System.Text;

namespace TPL.DataFlow.Implementation
{
    public interface ILoggingService
    {
        bool LogData(List<string> logs);

        bool LogData(string name, List<string> logs);
    }
}
