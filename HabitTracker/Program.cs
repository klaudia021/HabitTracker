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
    catch (InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message);
        
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
            //TODO
            ListHabitLogsById();
            break;
        case 5:
            ListHabits();
            break;
        case 6:
            ModifyMenu();
            break;
        default:
            break;
    };
}

void ModifyMenu()
{
    Console.Clear();
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
            //TODO
            DeleteHabit();
            break;
        case 3: 
            //TODO
            UpdateHabitLog();
            break;
        case 4:
            //TODO
            DeleteHabitLog();
            break;
        case 5:
            //TODO
            UpdateQuantityUnit();
            break;
        case 6:
            //TODO
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
    Console.Clear();

    Habit habit = new Habit();
    Console.Write("\nEnter the habit's name: ");
    habit.Name = ValidatorService.GetNotNullInput();

    ListQuantityUnits();
    int number = ValidatorService.GetValidInteger("\nEnter the number of the chosen unit of measurement or enter 0 to create a new one: ");

    QuantityUnit quantityUnit = new QuantityUnit() { QuantityUnitId = number};

    if (number == 0)
    {
        Console.Write("\nEnter the name of the new unit of measurement: ");
        quantityUnit.Name = ValidatorService.GetNotNullInput();
    }

    habitService.CreateHabitWithQuantityUnit(habit, quantityUnit);
}

void ListHabits()
{
    Console.Clear();

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
    int id = ValidatorService.GetValidInteger();

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

    Console.WriteLine("\nChoose the habit's number you want to log. Enter 0 to go back.");
    var habitId = ValidatorService.GetValidInteger();

    if (habitId == 0)
        return;

    habitLog.Habit.HabitId = habitId;

    Console.Clear();

    habitLog.Date = ValidatorService.GetValidDateTime();

    string quantityName = habitService.ReadQuantityNameForHabit(habitId);
    habitLog.Quantity = ValidatorService.GetValidDouble($"\nEnter quantity({quantityName}): ");

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

void ListQuantityUnits()
{
    var quantities = habitService.ReadQuantityUnits();

    if (quantities.Count == 0)
    {
        Console.WriteLine("\nNo recorded quantity found. Please create a new one.");

        return;
    }

    Console.WriteLine("\nAvailable units of measurement: ");

    foreach (var quantity in quantities)
        Console.WriteLine($"{quantity.QuantityUnitId} - {quantity.Name}");
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