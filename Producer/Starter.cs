using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.Producer
{
    public class Starter
    {
        public static void Main(string[] args)
        {

            Producer producer = new Producer();
            producer.Run(args);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
