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
            //TestNormalDataFlowBlock();
            bool iscomplete = false;
            //do
            //{
            //    Console.WriteLine("The task is not complete");

            //} while (iscomplete);

            TestContentDataFlowBlock();

            Console.ReadLine();

        }

        private static void TestNormalDataFlowBlock()
        {
            IDataFlowExecutor _dataFlowExecutor = new DataFlowExecutor();
            Console.WriteLine("Starting the pipeline");

            _dataFlowExecutor.Start();
        }

        private static void TestContentDataFlowBlock()
        {
            IDataFlowExecutor _dataFlowExecutor = new ContentDataFlowExecutor();
            Console.WriteLine("Starting the pipeline");

            _dataFlowExecutor.Start();
        }
    }
}
