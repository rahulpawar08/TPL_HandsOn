using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;
using TPL.DataFlow.Implementation.Blocks;

namespace TPL.DataFlow.Implementation
{
    public class DataFlowExecutor : IDataFlowExecutor
    {
        private TransformBlock<string, IEnumerable<List<string>>> _fetcherBlock;
        private TransformBlock<IEnumerable<List<string>>, IEnumerable<List<string>>> _deltaCalculatorBlock;
        private TransformBlock<IEnumerable<List<string>>, IEnumerable<List<string>>> _storeBlock;
        private ActionBlock<IEnumerable<List<string>>> _notifyBlock;

        public DataFlowExecutor()
        {
            _fetcherBlock =(TransformBlock <string, IEnumerable<List<string>>>) new FetcherBlock().GenerateBlock();
            _deltaCalculatorBlock = (TransformBlock<IEnumerable<List<string>>, IEnumerable<List<string>>>)new DeltaCalculatorBlock().GenerateBlock();
            _storeBlock = (TransformBlock <IEnumerable<List<string>>, IEnumerable<List<string>>>)new StoreBlock().GenerateBlock();
            _notifyBlock = (ActionBlock<IEnumerable<List<string>>>)new NotifyBlock().GenerateBlock();
        }
        public bool Start()
        {
            try
            {
                Console.WriteLine("Creating Pipeline");

                CreatePipeline();

                Console.WriteLine("Starting Pipeline");

                StartPipeline();

                Console.WriteLine("Pipeline Complete");
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Log exception
                Console.WriteLine("Exception: "+ ex.Message);
                return false;
            }
        }

        private void StartPipeline()
        {
            _fetcherBlock.Post("TestSupplier");
            _fetcherBlock.Complete();
           
        }

        private void CreatePipeline()
        {
            DataflowLinkOptions options = new DataflowLinkOptions();
            _fetcherBlock.LinkTo(_deltaCalculatorBlock, options);
            _deltaCalculatorBlock.LinkTo(_storeBlock, options);
            _storeBlock.LinkTo(_notifyBlock, options);
        }

        public bool Start(IEnumerable<string> supplierHotelIds)
        {
            _fetcherBlock.Post("TestSupplier");
            _fetcherBlock.Complete();
            return true;
        }
    }
}
