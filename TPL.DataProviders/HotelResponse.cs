using System.Collections.Generic;

namespace TPL.DataProviders
{
    public class HotelResponse
    {
        public List<Hotel> Hotels = new List<Hotel>();
        public int TickTime { get; set; }

        public bool IsComplete { get; set; }
    }
}