
string connectionString = @"Data Source=Habit-Tracker.db";

int menuNumber = 0;

do
{
    MenuService.ListMenu();

    Console.Write("Please choose a menu action: ");
    string? input = Console.ReadLine();

    menuNumber = MenuService.GetValidMenuInput(input);

    EvaluateMenu(menuNumber);

} while (menuNumber != 0);


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