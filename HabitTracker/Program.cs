
string connectionString = @"Data Source=Habit-Tracker.db";

IInputService consoleService = new ConsoleService();

int menuNumber = 0;

do
{
    ListMenu();

    Console.Write("Please choose a menu action: ");
    string? input = consoleService.ReadLine();

    menuNumber = GetValidMenuInput(input);

    EvaluateMenu(menuNumber);

} while (menuNumber != 0);


void ListMenu()
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

int GetValidMenuInput(string? input)
{
    int menuNumber = 0;

    while (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out menuNumber))
    {
        Console.Write("\nInvalid input! Please enter a valid menu number: ");
        input = consoleService.ReadLine();
    }

    return menuNumber;
}

void EvaluateMenu(int menuNumber)
{
    switch (menuNumber)
    {
        case 0:
            Console.WriteLine("\nExiting program....");
            break;
        case 1:
            Read();
            break;
        case 2: 
            Create();
            break;
        case 3: 
            Delete();
            break;
        case 4:
            Update();
            break;
        default:
            break;
    };
}

void Create()
{
    System.Console.WriteLine("Create");
}

void Read()
{
    System.Console.WriteLine("Read");
}

void Update()
{
    System.Console.WriteLine("Update");
}

void Delete()
{
    System.Console.WriteLine("Delete");
}