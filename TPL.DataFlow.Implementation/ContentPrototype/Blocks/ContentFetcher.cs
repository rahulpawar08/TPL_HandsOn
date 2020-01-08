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
    public class ContentFetcherBlock : ContentBaseBlock
    {
        IncrementalDataProvider _incrementalDataProvider;
        IDownloaderMonitoringService _downloaderMonitoringService;

        //TODO: check this approach later.
        TransformBlock<HotelResponse, List<Hotel>> _fetcherBlock;
        public ContentFetcherBlock()
        {
            _incrementalDataProvider = new IncrementalDataProvider(20,2000);
            _fetcherBlock = new TransformBlock<HotelResponse, List<Hotel>>
                (response => GetHotelList(response), GetExecutionOptions());
            _downloaderMonitoringService = new DownloaderMonitoringService();
        }

        private List<Hotel> GetHotelList(HotelResponse response)
        {
            List<Hotel> fetcherResponse = new List<Hotel>();
            foreach (var hotel in response.Hotels)
            {
                hotel.Status = Status.Processing;
                hotel.BlockStatus.Add(TPLBlocks.Fetcher, BlockStatus.ProcessingComplete);
                fetcherResponse.Add(hotel);

                Console.WriteLine("In fetcher for " + hotel.Name);
                //fetcherResponse.Add(hotel);

                //Console.WriteLine("Input Count: " + _fetcherBlock.InputCount);
                //Console.WriteLine("Output Count: " + _fetcherBlock.OutputCount);
                //Console.WriteLine("TaskScheduler Id:" + TaskScheduler.Current.Id.ToString());

                //Thread.Sleep(200);
                _downloaderMonitoringService.UpdateTransactionalProgress(new List<string>() { hotel.Name });
            }
            _downloaderMonitoringService.UpdateMilestoneProgress("Fetcher_Progress",
                    new List<string>() { "Fetcher Block complete for "+response.Hotels.Count + "hotels" });

            return fetcherResponse;
        }

        public override object GenerateBlock()
        {
            _fetcherBlock = new TransformBlock<HotelResponse, List<Hotel>>
                 (response => GetHotelList(response));

            return _fetcherBlock;
        }


        public override object LinkBlock(ContentBaseBlock contentBaseBlock)
        {
            return null;
           //return _fetcherBlock.LinkTo(contentBaseBlock.GetBlock());
        }

        public async Task GetHotels(HotelRequest request)
        {
            Console.WriteLine("In fetcher block for request - " + request.SupplierName + " ticktime: " + request.TickTime);
            HotelResponse response;
            List<Task> tasks = new List<Task>();
            int loop_Count = 1;
            do
            {
                response = _incrementalDataProvider.GetHotels(request);

                //Task task = _fetcherBlock.SendAsync(response);
                //tasks.Add(task);
                Console.WriteLine("Calling the FetcherBlock");

                _fetcherBlock.Post(response);

                 //await _fetcherBlock.SendAsync(response);

                Console.WriteLine("FetcherBlock execution done");

                request.TickTime = response.TickTime;
                Console.WriteLine("Loop count:" + loop_Count);
                

                if (response.IsComplete)
                    break;

                // _fetcherBlock.Complete();

                _downloaderMonitoringService.UpdateMilestoneProgress("Clarifi_Data_Download", 
                    new List<string>() { "Download complete for batch " + loop_Count + " with " + response.Hotels.Count + "hotels" });

               loop_Count++;
            } while (true);

            _fetcherBlock.Complete();

            #region ToBeRemoved
            //TEST THE PROPOGATION COMPLETION - THE MAIN PROGRAM SHOULD NOT HAVE A CONSOLE READLINE

            //await Task.WhenAll(tasks.ToArray());


            //Task completionTask = _fetcherBlock.Completion;
            //await completionTask;

            //do
            //{
            //    Task completionTask = _fetcherBlock.Completion;
            //    if (completionTask.IsCompleted)
            //    {
            //        Console.WriteLine("Fetcher is completed");
            //        break;
            //    }
            //} while (true);
            #endregion
        }
    }
}
