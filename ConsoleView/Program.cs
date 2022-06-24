using Repositories.Implementations;
using Services.Implementations;


var helpPage =
	"Argument help page:" +
	"-a,  --add          { Title }  { Text: optional } { Deadline (yyyy-mm-dd): optional }" +
	"-e.  --edit         { Id } { Title } { Text: optional } { Deadline (yyyy-mm-dd): optional }" +
	"-r,  --remove       { Id }" +
	"-m,  --mark         { Id }" +
	"-s,  --show" +
	"-so, --show-ordered" +
	"-se, --show-expired";

try
{
	var filePath = "./data.jsonl";
	var taskRepo = new TaskRepository(filePath);
	var service = new TaskService(taskRepo);
	ArgumentProcessor.Parse(service, args);
}
catch (Exception ex)
{
	Console.Error.WriteLine($"Error: {ex.Message}\n");
	Console.WriteLine(helpPage);
}
