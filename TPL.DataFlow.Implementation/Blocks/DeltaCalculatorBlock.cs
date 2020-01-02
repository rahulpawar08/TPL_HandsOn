using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TPL.DataFlow.Implementation.Blocks
{
    public class DeltaCalculatorBlock : BaseBlock
    {
        public override object GenerateBlock()
        {
            TransformBlock<List<string>, List<string>> deltaBlock = new TransformBlock<List<string>, List<string>>
                (async hotels => await GetHotelDelta(hotels));
            return deltaBlock;
        }

        private async Task<List<string>> GetHotelDelta(List<string> hotels)
        {
            List<string> deltaHotels = new List<string>();
            foreach (var hotel in hotels)
            {
                Console.WriteLine("In Delta for the hotel:" + hotel);
                deltaHotels.Add("Delta" + hotel);
            }
            return deltaHotels;
        }
    }
}
