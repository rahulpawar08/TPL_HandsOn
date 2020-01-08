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
        TransformBlock<List<Hotel>, List<Hotel>> _deltaBlock;
        IDownloaderMonitoringService _downloaderMonitoringService;

        public ContentDeltaCalculatorBlock()
        {
            _downloaderMonitoringService = new DownloaderMonitoringService();
        }

        public override object GenerateBlock()
        {
            _deltaBlock = new TransformBlock<List<Hotel>, List<Hotel>>(list => CalculateDelta(list));
            return _deltaBlock;
        }

        private List<Hotel> CalculateDelta(List<Hotel> hotels)
        {
            List<Hotel> deltaResponse = new List<Hotel>();
            foreach(var hotel in hotels)
            {
                
                hotel.BlockStatus.Add(TPLBlocks.Delta, BlockStatus.ProcessingComplete);
                hotel.Name += " delta ";
                deltaResponse.Add(hotel);
                Console.WriteLine("In Delta for " + hotel.Name);

                _downloaderMonitoringService.UpdateTransactionalProgress(new List<string>() { hotel.Name });
                //Console.WriteLine("Input Count: " + _deltaBlock.InputCount);
                //Console.WriteLine("Output Count: " + _deltaBlock.OutputCount);
                //Console.WriteLine("TaskScheduler Id:" + TaskScheduler.Current.Id.ToString());


                Thread.Sleep(200);
            }
            _downloaderMonitoringService.UpdateMilestoneProgress("Fetcher_Progress",
                    new List<string>() { "Delta Block complete for " + hotels.Count + "hotels" });
            return deltaResponse;
        }

        public override object LinkBlock(ContentBaseBlock baseBlock)
        {
            throw new NotImplementedException();
        }
    }
}
