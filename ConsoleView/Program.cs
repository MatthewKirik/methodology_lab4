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
	ArgumentProcessor.Parse(args);
}
catch (Exception ex)
{
	Console.Error.WriteLine($"Error: {ex.Message}\n");
	Console.WriteLine(helpPage);
}
