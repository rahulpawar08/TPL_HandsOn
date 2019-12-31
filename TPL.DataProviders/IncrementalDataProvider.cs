using System;
using System.Collections.Generic;

namespace TPL.DataProviders
{
    public class IncrementalDataProvider
    {
        private readonly int BatchCount;
        private readonly int MaxHotels;

        public IncrementalDataProvider(int batchCount, int maxHotel)
        {
            BatchCount = batchCount == 0 ? 30 : batchCount;
            MaxHotels = maxHotel == 0 ? 300 : maxHotel;
        }

        public HotelResponse GetHotels(HotelRequest hotelRequest)
        {
            return GetIncrementalHotels(hotelRequest.SupplierName, hotelRequest.TickTime);
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
            hotelResponse.IsComplete = endIndex >= MaxHotels;

            Console.WriteLine("Complete status:"+ hotelResponse.IsComplete);
            return hotelResponse;
        }
    }
}
