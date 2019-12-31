using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPL.DataFlow.Implementation;

namespace TPL_HandsOn
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataFlowExecutor _dataFlowExecutor = new DataFlowExecutor();
            Console.WriteLine("Starting the pipeline");

            _dataFlowExecutor.Start();

            Console.ReadLine();

        }
    }
}
