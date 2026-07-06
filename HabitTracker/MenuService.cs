namespace Services;
public static class MenuService
{
    public static void ListMainMenu()
    {
        Console.WriteLine("Please select a menu option!\n");

        Console.WriteLine("------------------------------");
        Console.WriteLine("1 - Create a habit");
        Console.WriteLine("2 - Log a habit");
        Console.WriteLine("3 - See all habit logs");
        Console.WriteLine("4 - See one habit's logs");
        Console.WriteLine("5 - Modify menu");
        Console.WriteLine("0 - Exit");
        Console.WriteLine($"------------------------------");
    }

    public static void ListModifyMenu()
    {
        Console.WriteLine("Please select a menu option!\n");

        Console.WriteLine("------------------------------");
        Console.WriteLine("1 - Modify habit");
        Console.WriteLine("2 - Delete habit");
        Console.WriteLine("3 - Modify habit log");
        Console.WriteLine("4 - Delete habit log");
        Console.WriteLine("5 - Modify unit of measurement");
        Console.WriteLine("6 - Delete unit of measurement\n");
        Console.WriteLine("9 - Back");
        Console.WriteLine("0 - Exit");
        Console.WriteLine($"------------------------------");
    }

    public static int GetValidMenuInput(string? input)
    {
        int menuNumber = 0;

        while (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out menuNumber))
        {
            Console.Write("\nInvalid input! Please enter a valid menu number: ");
            input = Console.ReadLine();
        }

        return menuNumber;
    }
}