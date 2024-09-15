using System.Text;

if (args.Length == 0)
{

    StringBuilder builder = new();

    // Display the command line arguments using the args variable.
    foreach (var arg in args)
    {
        builder.AppendLine($"Argument={arg}");
    }

    Console.WriteLine(builder.ToString());
}
else
{
    switch (args[0])
    {
        case "version":
            Version version = new Version();
            version.List();
            break;

        default: 
            Console.WriteLine("");
            Console.WriteLine("Argument not recognized. Did you mean... ?");

            Help help = new Help();
            help.List();

            break;
    }
}