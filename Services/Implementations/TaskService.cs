namespace Services.Implementations;
using Repositories;
using DataTransfer.Objects;

public class TaskService : ITaskService
{
	private readonly ITaskRepository taskRepo;

	public TaskService(ITaskRepository taskRepo)
	{
		this.taskRepo = taskRepo;
	}

	public IEnumerable<TaskDto> GetTasks()
	{
		return taskRepo.GetTasks();
	}

    public IEnumerable<TaskDto> GetTasksByDeadline()
	{
		throw new NotImplementedException();
	}

    public IEnumerable<TaskDto> GetOverdueTasks()
	{
		return taskRepo.GetTasks().Where(
			task => task.Deadline > DateTime.Now
		);
	}

    public void AddTask(TaskDto task)
	{
		taskRepo.AddTask(task);
	}

    public void RemoveTask(int id)
	{
		taskRepo.RemoveTask(id);
	}

    public void MarkAsCompleted(int id)
	{
		var task = taskRepo.GetTask(id);
		if (task == null) return;
		task.IsCompleted = true;
		taskRepo.EditTask(id, task);
	}

    public void EditTask(int id, TaskDto newValue)
	{
		taskRepo.EditTask(id, newValue);
	}
}
