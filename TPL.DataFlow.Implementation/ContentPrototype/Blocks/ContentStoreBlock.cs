using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TPL.DataFlow.Implementation.Blocks;

namespace TPL.DataFlow.Implementation.ContentPrototype.Blocks
{
    public class ContentStoreBlock : BaseBlock
    {
        TransformBlock<List<string>, List<string>> _storeBlock;
        public override object GenerateBlock()
        {
            _storeBlock = new TransformBlock<List<string>, List<string>>(hotels => StoreHotels(hotels));
            return _storeBlock;
        }

        private List<string> StoreHotels(List<string> hotels)
        {
            List<string> storeResponse = new List<string>();
            foreach (var hotel in hotels)
            {
                Console.WriteLine("In Store for " + hotel);
                storeResponse.Add(hotel + "store");

                //Console.WriteLine("Input Count: " + _storeBlock.InputCount);
                //Console.WriteLine("Output Count: " + _storeBlock.OutputCount);
                //Console.WriteLine("TaskScheduler Id:" + TaskScheduler.Current.Id.ToString());


                Thread.Sleep(200);
            }
            return storeResponse;
        }
    }
}
