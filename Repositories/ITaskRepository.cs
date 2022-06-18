using DataTransfer.Objects;

namespace Repositories;

public interface ITaskRepository
{
    IEnumerable<TaskDto> GetTasks();
    IEnumerable<TaskDto> GetTasks(Func<TaskDto, bool> filter);
    void RemoveTask(int id);
    void EditTask(int id, TaskDto newValue);
    void AddTask(TaskDto task);
}