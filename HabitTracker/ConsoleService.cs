public class ConsoleService : IInputService
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}