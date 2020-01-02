using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TPL.DataFlow.Implementation.Blocks;
using TPL.DataProviders;

namespace TPL.DataFlow.Implementation.ContentPrototype.Blocks
{
    public class ContentDeltaCalculatorBlock : ContentBaseBlock
    {
        TransformBlock<List<string>, List<string>> _deltaBlock;
        public override object GenerateBlock()
        {
            _deltaBlock = new TransformBlock<List<string>, List<string>>(list => CalculateDelta(list));
            return _deltaBlock;
        }

        private List<string> CalculateDelta(List<string> hotels)
        {
            List<string> deltaResponse = new List<string>();
            foreach(var hotel in hotels)
            {
                Console.WriteLine("In Delta for " + hotel);
                deltaResponse.Add(hotel + " delta ");


                //Console.WriteLine("Input Count: " + _deltaBlock.InputCount);
                //Console.WriteLine("Output Count: " + _deltaBlock.OutputCount);
                //Console.WriteLine("TaskScheduler Id:" + TaskScheduler.Current.Id.ToString());


                Thread.Sleep(200);
            }
            return deltaResponse;
        }

        public override object LinkBlock(ContentBaseBlock baseBlock)
        {
            throw new NotImplementedException();
        }
    }
}
