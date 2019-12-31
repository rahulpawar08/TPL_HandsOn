using System;
using System.Collections.Generic;

namespace TPL.DataProviders
{
    public class IncrementalDataProvider
    {
        private const int BatchCount =30;

        public HotelResponse GetHotels(string supplierName, int ticktime)
        {
            return GetIncrementalHotels(supplierName, ticktime);
        }

        private HotelResponse GetIncrementalHotels(string supplierName, int ticktime)
        {
            HotelResponse hotelResponse = new HotelResponse();
            List<string> hotels = new List<string>();
            int updatedTickTime = ticktime + 1;

            int startIndex = ticktime * BatchCount;
            int endIndex = startIndex + BatchCount;

            for (int index = startIndex; index <= endIndex; index++)
            {
                string str = "hotel:" + index + "-Ticktime:" + updatedTickTime;
                hotels.Add(str);
            }
            hotelResponse.Hotels = hotels;
            hotelResponse.TickTime = updatedTickTime;

            return hotelResponse;
        }
    }
}
