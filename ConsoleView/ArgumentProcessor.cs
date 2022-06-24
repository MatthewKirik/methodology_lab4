using DataTransfer.Objects;

public class ArgumentProcessor
{
	private int minArgs;
	private int maxArgs;
	private Services.ITaskService service;

	public ArgumentProcessor(Services.ITaskService service)
	{
		this.service = service;
	}

	private void ValidateArgumentsLength(int length)
	{
		if (minArgs == maxArgs && length != minArgs)
		{
			throw new ArgumentException($"This command needs {minArgs} argument, but got {length}.");
		}

		if (length > this.maxArgs)
		{
			throw new ArgumentException("You have entered too many arguments.");
		}
		else if (length < this.minArgs)
		{
			throw new ArgumentException("You have entered too few arguments.");
		}
	}

	private void ProcessAdd(string[] args)
	{
		this.minArgs = 1;
		this.maxArgs = 3;
		ValidateArgumentsLength(args.Length - 1);

		var taskDto = new TaskDto();
		taskDto.Title = args[1];

		if (args.Length == 3)
		{
			taskDto.Description = args[2];
		}
		else if (args.Length == 4)
		{
			var deadlineStr = args[3];
			var dateParts = deadlineStr.Split('-');
			if (dateParts.Length != 3)
			{
				throw new FormatException("Invalid deadline format.");
			}
			
			var isDeadLineCorrect = !DateTime.TryParse(deadlineStr, out var deadline);
			if (isDeadLineCorrect)
			{
				throw new FormatException("Invalid deadline format.");
			}

			taskDto.Description = args[2];
			taskDto.Deadline = deadline;
		}

		try
		{
			service!.AddTask(taskDto);
			Console.WriteLine("Success!");
		}
		catch (Exception)
		{
			Console.WriteLine("Cannot add task into storage.");
		}
	}

	private void ProcessEdit(string[] args)
	{
		this.minArgs = 2;
		this.maxArgs = 4;
		ValidateArgumentsLength(args.Length - 1);

		var idIsNotCorrect = !int.TryParse(args[1], out int id);
		if (idIsNotCorrect)
		{
			throw new ArgumentException("Task id must positive integer.");
		}

		var taskDto = new TaskDto();
		taskDto.Title = args[2];

		if (args.Length == 3)
		{
			taskDto.Description = args[3];
		}
		else if (args.Length == 4)
		{
			var deadlineStr = args[4];
			var dateParts = deadlineStr.Split('-');
			if (dateParts.Length != 3)
			{
				throw new FormatException("Invalid deadline format.");
			}
			
			var isDeadLineCorrect = !DateTime.TryParse(deadlineStr, out var deadline);
			if (isDeadLineCorrect)
			{
				throw new FormatException("Invalid deadline format.");
			}

			taskDto.Description = args[3];
			taskDto.Deadline = deadline;
		}

		try
		{
			service!.EditTask(id, taskDto);
			Console.WriteLine("Success!");
		}
		catch (Exception)
		{
			Console.WriteLine("Cannot edit task from storage.");
		}
	}

	private void ProcessRemove(string[] args)
	{
		this.minArgs = 1;
		this.maxArgs = 1;
		ValidateArgumentsLength(args.Length - 1);

		var idIsNotCorrect = !int.TryParse(args[1], out int id);
		if (idIsNotCorrect)
		{
			throw new ArgumentException("Task id must positive integer.");
		}

		try
		{
			service!.RemoveTask(id);
			Console.WriteLine("Success!");
		}
		catch (Exception)
		{
			Console.WriteLine("Cannot edit task from storage.");
		}
	}

	private void ProcessMark(string[] args)
	{
		this.minArgs = 1;
		this.maxArgs = 1;
		ValidateArgumentsLength(args.Length - 1);

		var idIsNotCorrect = !int.TryParse(args[1], out int id);
		if (idIsNotCorrect)
		{
			throw new ArgumentException("Task id must positive integer.");
		}

		try
		{
			service!.MarkAsCompleted(id);
			Console.WriteLine("Success!");
		}
		catch (Exception)
		{
			Console.WriteLine("Cannot edit task from storage.");
		}
	}

	private void ProcessShow(string[] args)
	{
		try
		{
			var tasks = service!.GetTasks();
			foreach (var task in tasks)
			{
				Console.WriteLine(task);
			}
		}
		catch (Exception)
		{
			Console.WriteLine("Cannot print tasks from storage.");
		}
	}

	private void ProcessShowOrdered(string[] args)
	{
		try
		{
			var tasks = service!.GetTasksByDeadline();
			foreach (var task in tasks)
			{
				Console.WriteLine(task);
			}
		}
		catch (Exception)
		{
			Console.WriteLine("Cannot print tasks from storage.");
		}
	}

	private void ProcessShowExpired(string[] args)
	{
		try
		{
			var tasks = service!.GetOverdueTasks();
			foreach (var task in tasks)
			{
				Console.WriteLine(task);
			}
		}
		catch (Exception)
		{
			Console.WriteLine("Cannot print tasks from storage.");
		}
	}

	private void ProcessCommands(string[] args)
	{
		if (args.Length == 0)
		{
			throw new NotImplementedException("Interactive mode not implemented.");
		}

		var command = args[0];
		switch (command)
		{
			case "-a":
			case "--add":
				ProcessAdd(args);
				break;
			case "-e":
			case "--edit":
				ProcessEdit(args);
				break;
			case "-r":
			case "--remove":
				ProcessRemove(args);
				break;
			case "-m":
			case "--mark":
				ProcessMark(args);
				break;
			case "-s":
			case "--show":
				ProcessShow(args);
				break;
			case "-so":
			case "--show-ordered":
				ProcessShowOrdered(args);
				break;
			case "-se":
			case "--show-expired":
				ProcessShowExpired(args);
				break;
			default:
				throw new ArgumentException("Unknown command.");
		}
	}

	public void Parse(string[] args)
	{
		ProcessCommands(args);
	}
}
