using System;
using System.Collections.Generic;
using System.Text;

namespace TPL.DataFlow.Implementation.BL
{
    public class DataProvider
    {

        public IEnumerable<List<string>> GetHotels(string supplierUrl)
        {
            yield return new List<string> { "hotel1", "hotel2" };
            yield return new List<string> { "hotel3", "hotel4" };
            yield return new List<string> { "hotel5", "hotel6" };
            yield return new List<string> { "hotel7", "hotel8" };
        }
    }
}
