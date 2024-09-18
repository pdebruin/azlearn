using System.Numerics;
using System.Text;

if (args.Length == 0)
{
    Help help = new Help();
    help.List();
}
else
{
    StringBuilder builder = new();
    foreach (var arg in args)
    {
        builder.AppendLine($"{arg}");
    }
    //Console.WriteLine(builder.ToString());

    switch (args[0].ToLower())
    {
        case "version":
            Version version = new Version();
            version.List();
            break;

        case "function":
        case "functions":
            Console.WriteLine("function(s)");

            AzureService azureService = new AzureService();
            azureService.List("functions");
            break;
        default: 
            Console.WriteLine("");
            Console.WriteLine("Argument not recognized. Did you mean... ?");

            Help help = new Help();
            help.List();

            break;
    }
}