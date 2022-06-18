using DataTransfer.Objects;

namespace Repositories;

public interface ITaskRepository
{
    IEnumerable<Task> GetTasks();
    IEnumerable<Task> GetTasks(Func<TaskDto, bool> filter);
    IEnumerable<Task> RemoveTask(int id);
    IEnumerable<Task> EditTask(int id, TaskDto newValue);
}