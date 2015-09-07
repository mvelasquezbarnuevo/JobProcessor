using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagement.WorkerProcess
{
    public class Starter
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(string.Format("Starting producer..."));

            Worker worker = new Worker();
            worker.Run(args);

            
        }
    }
}
