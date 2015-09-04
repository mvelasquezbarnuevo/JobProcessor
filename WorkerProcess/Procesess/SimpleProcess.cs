using System;
using CaseManagement.WorkerProcess.Procesess.Interfaces;
using System.Threading;

namespace CaseManagement.WorkerProcess.Procesess
{
    public class SimpleProcess: IProcess
    {
        public SimpleProcess()
        {
        }

        public void DoProcess(string message)
        {
            Console.WriteLine(" [x] Processing {0}", message);
            Thread.Sleep(5000);
        }
    }
}
