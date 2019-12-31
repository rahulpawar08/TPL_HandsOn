using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace TPL.DataFlow.Implementation.Blocks
{
    public class StoreBlock : BaseBlock
    {
        public override object GenerateBlock()
        {
            TransformBlock<List<string>, List<string>> saveBlock = new TransformBlock<List<string>, List<string>>
               (hotels => SaveHotel(hotels));
            return saveBlock;
        }

        private List<string> SaveHotel(List<string> hotels)
        {
            List<string> storeStatus = new List<string>();

            if (hotels != null)
            {
                foreach (var hotel in hotels)
                {
                    Console.WriteLine("In Store for the hotel:" + hotel);
                    storeStatus.Add(hotel + " success");
                }
            }
            else
            {
                Console.WriteLine("The hotels are null in Notify block.");
            }
            return storeStatus;
        }
    }
}
