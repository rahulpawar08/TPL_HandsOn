using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TPL.DataFlow.Implementation.Blocks;
using TPL.DataFlow.Implementation.ContentPrototype.Blocks;
using TPL.DataProviders;

namespace TPL.DataFlow.Implementation
{
    public class ContentDataFlowExecutor : IDataFlowExecutor
    {

        private TransformBlock<HotelResponse, List<string>> _fetcherBlock;
        private TransformBlock<List<string>, List<string>> _deltaCalculatorBlock;
        private TransformBlock<List<string>, List<string>> _storeBlock;
        private ActionBlock<List<string>> _notifyBlock;
        private ContentFetcherBlock _startBlock;

        public ContentDataFlowExecutor()
        {
            _startBlock = new ContentFetcherBlock();

            //TODO: need a way to encapsulate the linking of the blocks
            _fetcherBlock = (TransformBlock <HotelResponse, List<string>>)_startBlock.GenerateBlock();
            _deltaCalculatorBlock = (TransformBlock<List<string>, List<string>>)new ContentDeltaCalculatorBlock().GenerateBlock();
            _storeBlock = (TransformBlock<List<string>, List<string>>)new ContentStoreBlock().GenerateBlock();
            _notifyBlock = (ActionBlock<List<string>>)new ContentNotifyBlock().GenerateBlock();
        }
        public async Task<bool> Start()
        {
            try
            {
                Console.WriteLine("Creating Content DF Pipeline");

                CreatePipeline();

                Console.WriteLine("Starting Content DF Pipeline");

                await StartExecution();

                Console.WriteLine("Content DF Pipeline Complete");

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception occured:" + ex.Message);
                return false;
            }

        }

        private async Task StartExecution()
        {
            HotelRequest request = new HotelRequest();
            request.SupplierName = "Clarifi";

            await _startBlock.GetHotels(request);

            Console.WriteLine("The execution of pipeline is complete");
        }

        private void CreatePipeline()
        {
            DataflowLinkOptions options = new DataflowLinkOptions();
            //options.MaxMessages = 5;
            options.PropagateCompletion = true;

            _fetcherBlock.LinkTo(_deltaCalculatorBlock,options);
            _deltaCalculatorBlock.LinkTo(_storeBlock, options);
            _storeBlock.LinkTo(_notifyBlock, options);
        }

        public bool Start(IEnumerable<string> supplierHotelIds)
        {
            throw new NotImplementedException();
        }
    }
}
