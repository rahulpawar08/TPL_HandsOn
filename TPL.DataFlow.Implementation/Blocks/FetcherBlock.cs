using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;
using TPL.DataFlow.Implementation.BL;

namespace TPL.DataFlow.Implementation.Blocks
{
    public class FetcherBlock : BaseBlock
    {

        public override object GenerateBlock()
        {
            TransformBlock<string, List<string>> fetcherBlock = new TransformBlock<string, List<string>>
                (supplierUrl => GetHotels(supplierUrl));
            return fetcherBlock;
        }

        private List<string> GetHotels(string supplierUrl)
        {
            DataProvider dataProvider = new DataProvider();
            Console.WriteLine("In Fetcher for the supplierUrl:" +supplierUrl);
            List<string> hotels = new List<string>();
            foreach (var item in dataProvider.GetHotels(supplierUrl))
            {
                hotels.AddRange(item);
            }
            return hotels;
            //return new List<string>() { "hotel1", "hotel2", "hotel3", "hotel4",
            // "hotel5", "hotel6", "hotel7", "hotel8"};
        }
    }
}
