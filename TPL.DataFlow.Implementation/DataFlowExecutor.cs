using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TPL.DataFlow.Implementation.Blocks;

namespace TPL.DataFlow.Implementation
{
    public class DataFlowExecutor : IDataFlowExecutor
    {
        private TransformBlock<string, List<string>> _fetcherBlock;
        private TransformBlock<List<string>, List<string>> _deltaCalculatorBlock;
        private TransformBlock<List<string>, List<string>> _storeBlock;
        private ActionBlock<List<string>> _notifyBlock;

        public DataFlowExecutor()
        {
            _fetcherBlock =(TransformBlock <string, List<string>>) new FetcherBlock().GenerateBlock();
            _deltaCalculatorBlock = (TransformBlock<List<string>, List<string>>)new DeltaCalculatorBlock().GenerateBlock();
            _storeBlock = (TransformBlock <List<string>, List<string>>)new StoreBlock().GenerateBlock();
            _notifyBlock = (ActionBlock<List<string>>)new NotifyBlock().GenerateBlock();
        }
        public async Task<bool> Start()
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
