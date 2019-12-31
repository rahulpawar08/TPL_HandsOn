using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace TPL.DataFlow.Implementation.Blocks
{
    public class NotifyBlock : BaseBlock
    {
        public override object GenerateBlock()
        {
            ActionBlock<List<string>> notifyBlock = new ActionBlock<List<string>>
               (hotels => NotifyHotel(hotels));
            return notifyBlock;
        }

        private void NotifyHotel(List<string> hotels)
        {
            if (hotels != null)
            {
                foreach (var hotel in hotels)
                    Console.WriteLine("In Notify for the hotel:" + hotel);
            }
            else
            {
                Console.WriteLine("The hotels are null in Notify block.");
            }
        }
    }
}
