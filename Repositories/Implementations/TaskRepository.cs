using FileStorage;
using DataTransfer.Objects;

namespace Repositories.Implementations;

public class TaskRepository : ITaskRepository
{
    private readonly FileList<TaskDto> _fileList;
    public TaskRepository(string filepath)
    {
        _fileList = new FileList<TaskDto>(filepath,
            new System.IO.Abstractions.FileSystem());
    }

    public TaskDto? GetTask(int id)
    {
        return _fileList.FirstOrDefault(task => task.Id == id);
    }

    public IEnumerable<TaskDto> GetTasks()
    {
        var tasks = _fileList.ToList();
        return tasks;
    }

    public IEnumerable<TaskDto> GetTasks(Func<TaskDto, bool> filter)
    {
        var tasks = _fileList.ToList();
        return tasks;
    }

    public void RemoveTask(int id)
    {
        _fileList.RemoveFirst(taskDto => taskDto.Id == id);
    }

    public void EditTask(int id, TaskDto newValue)
    {
        _fileList.EditFirst(
            taskDto => taskDto.Id == id, 
            origTaskDto => newValue
        );
    }

    public void AddTask(TaskDto task)
    {
        _fileList.Add(task);
    }
}
