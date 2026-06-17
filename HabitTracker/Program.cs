using Microsoft.Data.Sqlite;
using Services;
using HabitTracker;

string connectionString = @"Data Source=Habit-Tracker.db";
HabitDatabase db = new HabitDatabase(connectionString);

int menuNumber = 0;

do
{
    try
    {
        MenuService.ListMenu();

        Console.Write("Please choose a menu action: ");
        string? input = Console.ReadLine();

        Console.WriteLine();

        menuNumber = MenuService.GetValidMenuInput(input);

        EvaluateMenu(menuNumber);

    }
    catch (SqliteException ex)
    {
        Console.WriteLine("A database error occurred!");

        LogError(ex);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An unexpected error occurred!");
        
        LogError(ex);
    }
    finally
    {
        if (menuNumber != 0)
        {
            CleanUpAndContinue();
        }
    }

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
    Habit habit = new();

    habit.Date = ValidatorService.GetValidDateTime();
    habit.Quantity = ValidatorService.GetValidQuantity();

    db.Create(habit);
}

void Read()
{
    List<Habit> habits = db.Read();

    foreach (var habit in habits)
        Console.WriteLine(habit.ToString());
}

void Update()
{
    System.Console.WriteLine("Update");
}

void Delete()
{
    System.Console.WriteLine("Delete");
}

void LogError(Exception exception)
{
    try
    {
        string date = DateTime.Now.ToString("MM-dd-yyyy");
        using StreamWriter output = new StreamWriter($"Log-{date}.txt", true);

        output.WriteLine($"{date}: {exception.GetType().Name} - {exception.Message}");
    }
    catch (Exception)
    {
        Console.WriteLine("Cannot log error!");
    }
}

void CleanUpAndContinue()
{
    Console.Write("\nPress any button to continue...");
    Console.ReadLine();

    Console.Clear();
}