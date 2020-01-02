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

        //TODO: check this approach later.
        TransformBlock<HotelResponse, List<string>> _fetcherBlock;
        public ContentFetcherBlock()
        {
            _incrementalDataProvider = new IncrementalDataProvider(2,20);
            _fetcherBlock = new TransformBlock<HotelResponse, List<string>>
                (response => GetHotelList(response),new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 5,MaxMessagesPerTask=1 });
        }

        private List<string> GetHotelList(HotelResponse response)
        {
            List<string> fetcherResponse = new List<string>();
            foreach (var hotel in response.Hotels)
            {
                Console.WriteLine("In fetcher for " + hotel);
                fetcherResponse.Add(hotel);

                //Console.WriteLine("Input Count: " + _fetcherBlock.InputCount);
                //Console.WriteLine("Output Count: " + _fetcherBlock.OutputCount);
                //Console.WriteLine("TaskScheduler Id:" + TaskScheduler.Current.Id.ToString());

                //Thread.Sleep(200);
            }

            return fetcherResponse;
        }

        public override object GenerateBlock()
        {
            _fetcherBlock = new TransformBlock<HotelResponse, List<string>>
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
                loop_Count++;

                if (response.IsComplete)
                    break;

               // _fetcherBlock.Complete();

            } while (true);

            //TEST THE PROPOGATION COMPLETION - THE MAIN PROGRAM SHOULD NOT HAVE A CONSOLE READLINE

            //await Task.WhenAll(tasks.ToArray());
            _fetcherBlock.Complete();

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
        }
    }
}
