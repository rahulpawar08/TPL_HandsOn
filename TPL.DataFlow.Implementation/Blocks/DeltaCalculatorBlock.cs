﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace TPL.DataFlow.Implementation.Blocks
{
    public class DeltaCalculatorBlock : BaseBlock
    {
        public override object GenerateBlock()
        {
            TransformBlock<List<string>, List<string>> deltaBlock = new TransformBlock<List<string>, List<string>>
                (hotels => GetHotelDelta(hotels));
            return deltaBlock;
        }

        private List<string> GetHotelDelta(List<string> hotels)
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
