using Repositories.Implementations;
using Services.Implementations;
using DataTransfer.Objects;

public static class ArgumentProcessor
{
	private static int minCommandArgs;
	private static int maxCommandArgs;
	private static TaskService? service;

	private static void ValidateArgumentsLength(int length)
	{
		if (length > ArgumentProcessor.maxCommandArgs)
		{
			throw new ArgumentException("You have entered too many arguments.");
		}
		else if (length < ArgumentProcessor.minCommandArgs)
		{
			throw new ArgumentException("You have entered too few arguments.");
		}
	}

	private static void ProcessAdd(string[] args)
	{
		ArgumentProcessor.minCommandArgs = 1;
		ArgumentProcessor.maxCommandArgs = 3;
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

	private static void ProcessEdit(string[] args)
	{
		ArgumentProcessor.minCommandArgs = 2;
		ArgumentProcessor.maxCommandArgs = 4;
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

	private static void ProcessRemove(string[] args)
	{

	}

	private static void ProcessMark(string[] args)
	{

	}

	private static void ProcessShow(string[] args)
	{

	}

	private static void ProcessShowOrdered(string[] args)
	{

	}

	private static void ProcessShowExpired(string[] args)
	{

	}

	public static void ProcessCommands(string[] args)
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

	public static void Parse(string[] args)
	{
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		///////// ???
		var filePath = "./file.txt";
		var taskRepo = new TaskRepository(filePath);
		ArgumentProcessor.service = new TaskService(taskRepo);
		ProcessCommands(args);
	}
}
