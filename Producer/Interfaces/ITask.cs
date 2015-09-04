namespace CaseManagement.Producer.Interfaces.Tasks
{
    public interface ITask
    {
        string GetDescription();
        string GetTaskType();
    }
}