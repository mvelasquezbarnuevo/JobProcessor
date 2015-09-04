using System;
using CaseManagement.Producer.Interfaces.Tasks;

namespace CaseManagement.Producer.Tasks
{
    public class SimpleTask : ITask
    {
        private string _taskType;
        private string _description;
        public SimpleTask(string type)
        {
            _taskType = type;
        }

        public string GetDescription()
        {
            return _description;
        
        }

        public string GetTaskType()
        {
            return _taskType;
        }
    }
}
