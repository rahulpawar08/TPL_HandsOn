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

        private TransformBlock<HotelResponse, List<Hotel>> _fetcherBlock;
        private TransformBlock<List<Hotel>, List<Hotel>> _deltaCalculatorBlock;
        private TransformBlock<List<Hotel>, List<Hotel>> _storeBlock;
        private ActionBlock<List<Hotel>> _notifyBlock;
        private ContentFetcherBlock _startBlock;
        IDownloaderMonitoringService _downloaderMonitoringService;

        public ContentDataFlowExecutor()
        {
            _startBlock = new ContentFetcherBlock();

            //TODO: need a way to encapsulate the linking of the blocks
            _fetcherBlock = (TransformBlock <HotelResponse, List<Hotel>>)_startBlock.GenerateBlock();
            _deltaCalculatorBlock = (TransformBlock<List<Hotel>, List<Hotel>>)new ContentDeltaCalculatorBlock().GenerateBlock();
            _storeBlock = (TransformBlock<List<Hotel>, List<Hotel>>)new ContentStoreBlock().GenerateBlock();
            _notifyBlock = (ActionBlock<List<Hotel>>)new ContentNotifyBlock().GenerateBlock();
            _downloaderMonitoringService = new DownloaderMonitoringService();
        }
        public async Task<bool> Start()
        {
            try
            {
                //Console.WriteLine("Creating Content DF Pipeline");

                CreatePipeline();

                //Console.WriteLine("Starting Content DF Pipeline");

                await StartExecution();

                //Console.WriteLine("Content DF Pipeline Complete");

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

            _downloaderMonitoringService.UpdateMilestoneProgress("Pipeline",
                  new List<string>() { "Pipeline Started" });

            _startBlock.GetHotels(request);

            //await Task.WhenAll(_fetcherBlock.Completion, _deltaCalculatorBlock.Completion, _storeBlock.Completion)
            //    .ContinueWith(_ => _notifyBlock.Complete());

            Task.WhenAll(_notifyBlock.Completion).Wait();

            _downloaderMonitoringService.UpdateMilestoneProgress("Pipeline",
                  new List<string>() { "The execution of pipeline is complete." });

            //Console.WriteLine("The execution of pipeline is complete");
        }

        private void CreatePipeline()
        {
            DataflowLinkOptions options = new DataflowLinkOptions();
            //options.MaxMessages = 5;
            options.PropagateCompletion = true;

            _fetcherBlock.LinkTo(_deltaCalculatorBlock,options);
            _deltaCalculatorBlock.LinkTo(_storeBlock, options);
            _storeBlock.LinkTo(_notifyBlock, options);

            _downloaderMonitoringService.UpdateMilestoneProgress("Pipeline",
                   new List<string>() { "Pipeline Created" });
        }

        public bool Start(IEnumerable<string> supplierHotelIds)
        {
            throw new NotImplementedException();
        }
    }
}
