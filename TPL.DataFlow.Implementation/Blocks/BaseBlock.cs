using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace TPL.DataFlow.Implementation.Blocks
{
    public abstract class BaseBlock
    {
       public abstract object GenerateBlock();

       public virtual ExecutionDataflowBlockOptions GetExecutionOptions()
        {
            return new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 5,
                MaxMessagesPerTask = 3
            };
        }

    }
}
