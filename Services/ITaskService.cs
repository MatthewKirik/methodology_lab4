using DataTransfer.Objects;

namespace Services;

public interface ITaskService
{
    IEnumerable<TaskDto> GetTasks();
    IEnumerable<TaskDto> GetTasksByDeadline();
    IEnumerable<TaskDto> GetOverdueTasks();
    void AddTask(TaskDto task);
    void RemoveTask(int id);
    void MarkAsCompleted(int id);
    void EditTask(int id, TaskDto newValue);
}