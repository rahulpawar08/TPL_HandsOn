using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TPL.DataFlow.Implementation.Blocks;

namespace TPL.DataFlow.Implementation.ContentPrototype.Blocks
{
    public class ContentNotifyBlock : BaseBlock
    {
        ActionBlock<List<string>> _notifyBlock;
        public override object GenerateBlock()
        {
            _notifyBlock = new ActionBlock<List<string>>(async hotels => await Notify(hotels));
            return _notifyBlock;
        }

        private async Task Notify(List<string> hotels)
        {
            foreach (var hotel in hotels)
            {
                Console.WriteLine("In notify for " + hotel);

                //Console.WriteLine("Input Count: " + _notifyBlock.InputCount);
                //Console.WriteLine("Output Count: " + "NA");
                //Console.WriteLine("TaskSchedulerId:" + TaskScheduler.Current.Id.ToString());


                Thread.Sleep(200);
            }
        }
    }
}
