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

	// private static string ValidateModule(string command)
	// {
	// 	var supportedCommands = new string[]
	// 	{
	// 		"pixel",
	// 		"fast"
	// 	};

	// 	for (int i = 0; i < supportedModules.Length; i++)
	// 	{
	// 		if (supportedModules[i] == module)
	// 		{
	// 			return module;
	// 		}
	// 	}

	// 	throw new ArgumentException($"The module \'{module}\' is not supported.");
	// }

	// private static string ValidateInputFile(string file)
	// {
	// 	if (!System.IO.File.Exists(file))
	// 	{
	// 		throw new ArgumentException($"The file \'{file}\' does not exist.");
	// 	}
	// 	return file;
	// }

	// private static string ValidateOperation(string operation)
	// {
	// 	var supportedOperations = new string[]
	// 	{
	// 		"crop",
	// 		"rotateleft90",
	// 		"swapredandblue",
	// 		"sepia",
	// 		"changesaturation"
	// 	};

	// 	for (int i = 0; i < supportedOperations.Length; i++)
	// 	{
	// 		if (supportedOperations[i] == operation)
	// 		{
	// 			return operation;
	// 		}
	// 	}

	// 	throw new ArgumentException($"The operation \'{operation}\' is not supported.");
	// }

	// private static Rectangle ParseRectangle(string rectFormat)
	// {
	// 	int width, height, x, y;
	// 	var plusSplitedValues = rectFormat.Split('+');
	// 	if (plusSplitedValues.Length != 3)
	// 	{
	// 		throw new ArgumentException("Wrong crop options.");
	// 	}
	// 	var xSeparatedSizes = plusSplitedValues[0];
	// 	var sizes = xSeparatedSizes.Split('x');
	// 	if (sizes.Length != 2)
	// 	{
	// 		throw new ArgumentException("Wrong crop options.");
	// 	}

	// 	if (!int.TryParse(sizes[0], out width) || width < 0)
	// 	{
	// 		throw new ArgumentException("Width must be positive integer number.");
	// 	}
	// 	if (!int.TryParse(sizes[1], out height) || height < 0)
	// 	{
	// 		throw new ArgumentException("Height must be positive integer number.");
	// 	}
	// 	if (!int.TryParse(plusSplitedValues[1], out x))
	// 	{
	// 		throw new ArgumentException("X coordinate must be integer number.");
	// 	}
	// 	if (!int.TryParse(plusSplitedValues[2], out y))
	// 	{
	// 		throw new ArgumentException("Y coordinate must be integer number.");
	// 	}

	// 	return new Rectangle(x, y, width, height);
	// }

	// private static void ProcessCrop(IImageEditor editor, Bitmap bmp, string outputFile, string[] args)
	// {
	// 	if (args.Length != 5)
	// 	{
	// 		throw new ArgumentException("You have entered too many or less than needed arguments.");
	// 	}
	// 	var cropArguments = args[4];
	// 	var rect = ParseRectangle(cropArguments);
	// 	var bmpCopy = editor.Crop(bmp, rect.X, rect.Y, rect.Width, rect.Height);
	// 	bmpCopy.Save(outputFile);
	// }

	// private static void ProcessRotateLeft90(IImageEditor editor, Bitmap bmp, string outputFile, int length)
	// {
	// 	if (length != 4)
	// 	{
	// 		throw new ArgumentException("You have entered too many or less than needed arguments.");
	// 	}
	// 	var bmpCopy = editor.RotateLeft90(bmp);
	// 	bmpCopy.Save(outputFile);
	// }

	// private static void ProcessSwapRedAndBlue(IImageEditor editor, Bitmap bmp, string outputFile, int length)
	// {
	// 	if (length != 4)
	// 	{
	// 		throw new ArgumentException("You have entered too many or less than needed arguments.");
	// 	}
	// 	var bmpCopy = editor.SwapRedAndBlue(bmp);
	// 	bmpCopy.Save(outputFile);
	// }

	// private static void ProcessSepia(IImageEditor editor, Bitmap bmp, string outputFile, int length)
	// {
	// 	if (length != 4)
	// 	{
	// 		throw new ArgumentException("You have entered too many or less than needed arguments.");
	// 	}
	// 	var bmpCopy = editor.Sepia(bmp);
	// 	bmpCopy.Save(outputFile);
	// }

	// private static void ProcessChangeSaturation(IImageEditor editor, Bitmap bmp, string outputFile, string[] args)
	// {
	// 	if (args.Length != 5)
	// 	{
	// 		throw new ArgumentException("You have entered too many or less than needed arguments.");
	// 	}
	// 	int saturation;
	// 	var validateSatur = int.TryParse(args[4], out saturation);
	// 	if (!validateSatur || saturation < 0)
	// 	{
	// 		throw new ArgumentException("Saturation must be non-negative integer number");
	// 	}
	// 	var bmpCopy = editor.ChangeSaturation(bmp, saturation);
	// 	bmpCopy.Save(outputFile);
	// }

	// private static void ProcessOperation(IImageEditor editor, Bitmap bmp, string outputFile, string operation, string[] args)
	// {
	// 	Stopwatch sw = new Stopwatch();
	// 	sw.Start();
	// 	if (operation == "crop")
	// 	{
	// 		ProcessCrop(editor, bmp, outputFile, args);
	// 	}
	// 	else if (operation == "rotateleft90")
	// 	{
	// 		ProcessRotateLeft90(editor, bmp, outputFile, args.Length);
	// 	}
	// 	else if (operation == "swapredandblue")
	// 	{
	// 		ProcessSwapRedAndBlue(editor, bmp, outputFile, args.Length);
	// 	}
	// 	else if (operation == "sepia")
	// 	{
	// 		ProcessSepia(editor, bmp, outputFile, args.Length);
	// 	}
	// 	else if (operation == "changesaturation")
	// 	{
	// 		ProcessChangeSaturation(editor, bmp, outputFile, args);
	// 	}
	// 	else
	// 	{
	// 		throw new ArgumentException($"Unknown operation \'{operation}\'.");
	// 	}
	// 	sw.Stop();
	// 	Console.WriteLine($"Operation done in: {sw.Elapsed}");
	// }

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

		service!.AddTask(taskDto);
	}

	private static void ProcessEdit(string[] args)
	{

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
		var filePath = "./file.txt";
		var taskRepo = new TaskRepository(filePath);
		ArgumentProcessor.service = new TaskService(taskRepo);
		ProcessCommands(args);
	}
}
