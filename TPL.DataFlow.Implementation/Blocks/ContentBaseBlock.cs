using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace TPL.DataFlow.Implementation.Blocks
{
    public abstract class ContentBaseBlock:BaseBlock
    {
        //public abstract ITargetBlock<TInput> GetBlock();
        public abstract object LinkBlock(ContentBaseBlock baseBlock);

        
    }
}
