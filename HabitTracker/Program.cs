using Microsoft.Data.Sqlite;
using Services;
using HabitTracker;

string connectionString = @"Data Source=Habit-Tracker.db";
HabitDatabase db = new HabitDatabase(connectionString);
HabitService habitService = new HabitService(db);

int menuNumber = 0;

Console.WriteLine("\n\n");

do
{
    try
    {
        MenuService.ListMainMenu();

        Console.Write("Please choose a menu action: ");
        string? input = Console.ReadLine();

        Console.WriteLine();

        menuNumber = MenuService.GetValidMenuInput(input);

        EvaluateMainMenu(menuNumber);

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

void EvaluateMainMenu(int menuNumber)
{
    switch (menuNumber)
    {
        case 0:
            Console.WriteLine("\nExiting program....");
            break;
        case 1:
            CreateHabit();
            break;
        case 2:
            ListHabits();
            CreateHabitLog();
            break;
        case 3: 
            ListHabitLogs();
            break;
        case 4:
            ListHabitLogsById();
            break;
        case 5:
            ModifyMenu();
            break;
        default:
            break;
    };
}

void ModifyMenu()
{
    MenuService.ListModifyMenu();

    Console.Write("Please choose a menu action: ");
    string? input = Console.ReadLine();

    Console.WriteLine();

    menuNumber = MenuService.GetValidMenuInput(input);

    EvaluateModifyMenu(menuNumber);
}

void EvaluateModifyMenu(int menuNumber)
{
    Console.Clear();

    switch (menuNumber)
    {
        case 1:
            ListHabits();
            UpdateHabit();
            break;
        case 2:
            ListHabits();
            DeleteHabit();
            break;
        case 3: 
            UpdateHabitLog();
            break;
        case 4:
            DeleteHabitLog();
            break;
        case 5:
            UpdateQuantityUnit();
            break;
        case 6:
            DeleteQuantityUnit();
            break;
        case 9:
            CleanUpAndContinue();
            break;
        default:
            break;
    };
}

void CreateHabit()
{
    Habit habit = new Habit();
    habit.Name = ValidatorService.GetNotNullInput();

    habitService.CreateHabit(habit);
}

void ListHabits()
{
    List<Habit> habits = habitService.ReadHabits();

    Console.WriteLine("Habits:");

    foreach(var habit in habits)
        Console.WriteLine(habit.ToString());
}

void UpdateHabit()
{
    List<Habit> habits = habitService.ReadHabits();

    if (!habits.Any())
        return;
    
    Console.WriteLine("\nChoose the habit's number you want to update!");
    int id = ValidatorService.GetValidNumber();

    Console.Write("\nEnter the habit's new name: ");
    string name = ValidatorService.GetNotNullInput();

    string response = habitService.UpdateHabit(new Habit { HabitId = id, Name = name });

    Console.WriteLine(response);
}

void DeleteHabit()
{
    
}

void CreateHabitLog()
{
    HabitLog habitLog = new();
    Habit habit = new();

    Console.WriteLine("\nChoose the habit's number you want to log.");
    habit.HabitId = ValidatorService.GetValidNumber();

    habitLog.Habit = habit;
    habitLog.Date = ValidatorService.GetValidDateTime();
    habitLog.Quantity = ValidatorService.GetValidNumber();

    habitService.CreateHabitLog(habitLog);
}

void ListHabitLogs()
{
    List<HabitLog> habitLogs = habitService.ReadHabitLogs();

    Console.WriteLine("Logs:");

    foreach (var logs in habitLogs)
        Console.WriteLine(logs.ToString());
}

void UpdateHabitLog()
{
    System.Console.WriteLine("Update");
}

void DeleteHabitLog()
{
    System.Console.WriteLine("Delete");
}

void ListHabitLogsById()
{
    
}

void UpdateQuantityUnit()
{
    
}

void DeleteQuantityUnit()
{
    
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