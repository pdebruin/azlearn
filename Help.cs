public class Help
{
    public void List()
    {
        Console.WriteLine("");
        Console.WriteLine("AzLearn brings Microsoft Learn to the command-line!");
        Console.WriteLine("---------------------------------------------------");

        Console.WriteLine("Use 'azlearn' to see available commands or go to https://aka.ms/azlearn.");
        Console.WriteLine("");

        Commands commands = new Commands();
        commands.List();
    }
}