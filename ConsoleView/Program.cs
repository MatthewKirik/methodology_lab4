var helpPage =
	"Argument help page:" +
	"-a,  --add          { Title }  { Text: optional } { Deadline: optional }" +
	"-e.  --edit         { Id } { Title } { Text: optional } { Deadline: optional }" +
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
