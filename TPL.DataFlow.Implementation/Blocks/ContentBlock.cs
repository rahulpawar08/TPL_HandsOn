using System;
using System.Collections.Generic;
using System.Text;

namespace TPL.DataFlow.Implementation.Blocks
{
    abstract class ContentBaseBlock:BaseBlock
    {
        public abstract object LinkBlock(BaseBlock baseBlock);
    }
}
