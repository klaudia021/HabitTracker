public static class MenuService
{
    public static void ListMenu()
    {
        Console.Clear();

        Console.WriteLine("Please select a menu option!\n");

        Console.WriteLine("------------------------------");
        Console.WriteLine("1 - View all records");
        Console.WriteLine("2 - Insert a record");
        Console.WriteLine("3 - Delete a record");
        Console.WriteLine("4 - Update a record");
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