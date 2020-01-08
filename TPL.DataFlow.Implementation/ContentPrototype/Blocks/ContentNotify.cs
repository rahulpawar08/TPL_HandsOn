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
    public class ContentNotifyBlock : BaseBlock
    {
        ActionBlock<List<Hotel>> _notifyBlock;
        IDownloaderMonitoringService _downloaderMonitoringService;

        public ContentNotifyBlock()
        {
            _downloaderMonitoringService = new DownloaderMonitoringService();
        }

        public override object GenerateBlock()
        {
            _notifyBlock = new ActionBlock<List<Hotel>>(hotels => Notify(hotels), GetExecutionOptions());
            return _notifyBlock;
        }

        private void Notify(List<Hotel> hotels)
        {
            foreach (var hotel in hotels)
            {
                hotel.BlockStatus.Add(TPLBlocks.Notifier, BlockStatus.ProcessingComplete);

                PublishStats(hotel);
                Console.WriteLine("In notify for " + hotel.Name);
                _downloaderMonitoringService.UpdateTransactionalProgress(new List<string>() { hotel.Name });
                //Console.WriteLine("Input Count: " + _notifyBlock.InputCount);
                //Console.WriteLine("Output Count: " + "NA");
                //Console.WriteLine("TaskSchedulerId:" + TaskScheduler.Current.Id.ToString());

                Thread.Sleep(200);
            }

            _downloaderMonitoringService.UpdateMilestoneProgress("Notify_Progress",
                   new List<string>() { "Notify Block complete for " + hotels.Count + "hotels" });
        }

        private void PublishStats(Hotel hotel)
        {
           
        }
    }
}
