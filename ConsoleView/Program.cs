using Repositories.Implementations;
using Services.Implementations;


var helpPage =
	"Argument help page:\n" +
	"-a,  --add          { Title }  { Text: optional } { Deadline (yyyy-mm-dd): optional }\n" +
	"-e.  --edit         { Id } { Title } { Text: optional } { Deadline (yyyy-mm-dd): optional }\n" +
	"-r,  --remove       { Id }\n" +
	"-m,  --mark         { Id }\n" +
	"-s,  --show\n" +
	"-so, --show-ordered\n" +
	"-se, --show-expired\n";

try
{
	var filePath = "./data.jsonl";
	var taskRepo = new TaskRepository(filePath);
	var service = new TaskService(taskRepo);
	var argsProcessor = new ArgumentProcessor(service);
	argsProcessor.Parse(args);
}
catch (Exception ex)
{
	Console.Error.WriteLine($"Error: {ex.Message}\n");
	Console.WriteLine(helpPage);
}
