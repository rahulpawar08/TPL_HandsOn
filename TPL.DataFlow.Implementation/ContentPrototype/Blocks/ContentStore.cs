using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using TPL.DataFlow.Implementation.Blocks;
using TPL.DataProviders;

namespace TPL.DataFlow.Implementation.ContentPrototype.Blocks
{
    public class ContentStoreBlock : BaseBlock
    {
        TransformBlock<List<Hotel>, List<Hotel>> _storeBlock;
        IDownloaderMonitoringService _downloaderMonitoringService;
        public ContentStoreBlock()
        {
            _downloaderMonitoringService = new DownloaderMonitoringService();
        }
        public override object GenerateBlock()
        {
            _storeBlock = new TransformBlock<List<Hotel>, List<Hotel>>(hotels => StoreHotels(hotels),GetExecutionOptions());
            return _storeBlock;
        }

        private List<Hotel> StoreHotels(List<Hotel> hotels)
        {
            List<Hotel> storeResponse = new List<Hotel>();
            foreach (var hotel in hotels)
            {
              
                hotel.Name += "store";

                storeResponse.Add(hotel);
                Console.WriteLine("In Store for " + hotel.Name);
                //Console.WriteLine("Input Count: " + _storeBlock.InputCount);
                //Console.WriteLine("Output Count: " + _storeBlock.OutputCount);
                //Console.WriteLine("TaskScheduler Id:" + TaskScheduler.Current.Id.ToString());
                _downloaderMonitoringService.UpdateTransactionalProgress(new List<string>() { hotel.Name });

                Thread.Sleep(200);
            }
            _downloaderMonitoringService.UpdateMilestoneProgress("Store_Progress",
                    new List<string>() { "Store Block complete for " + hotels.Count + "hotels" });
            return storeResponse;
        }
    }
}
