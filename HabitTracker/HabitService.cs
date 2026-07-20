using HabitTracker;

public class HabitService
{
    private readonly HabitDatabase _habitDatabase;
    public HabitService(HabitDatabase habitDatabase)
    {
        _habitDatabase = habitDatabase;
    }

    public void CreateHabitWithQuantityUnit(Habit habit, QuantityUnit quantityUnit)
    {
        var quantityUnitDb = CreateQuantityUnit(quantityUnit);
        var habitDb = CreateHabit(habit);

        if (quantityUnitDb == null || habitDb == null)
            throw new InvalidOperationException("Error: Habit could not be created with the given unit of measurement.");

        var quantityUnitId = quantityUnitDb.QuantityUnitId;
        var habitId = habitDb.HabitId;

        _habitDatabase.CreateHabitQuantityUnitConnection(habitId, quantityUnitId);           
    }

    private Habit? CreateHabit(Habit habit)
    {
        Habit? habitDb = _habitDatabase.GetHabitByName(habit.Name);

        if (habitDb == null)
        {
            _habitDatabase.CreateHabit(habit);
            habitDb = _habitDatabase.GetHabitByName(habit.Name);

            Console.WriteLine("Habit created successfully!");
        }
        else
            throw new InvalidOperationException($"Error: Habit already exists with name '{habit.Name}'");

        return habitDb;
    }

    public List<Habit> ReadHabits()
    {
        return _habitDatabase.ReadHabits();
    }

    public string UpdateHabit(Habit habit)
    {
        if (habit == null || string.IsNullOrWhiteSpace(habit.Name) || habit.HabitId <= 0)
            return "Data is not valid! Please provide valid data!";

        if (_habitDatabase.GetHabitById(habit.HabitId) == null)
            return $"Habit is not found with ID {habit.HabitId}!";

        return _habitDatabase.UpdateHabit(habit);
    }

    public void DeleteHabit(int id)
    {
        
    }

    public void CreateHabitLog(HabitLog habitLog)
    {
        Habit? habit = _habitDatabase.GetHabitById(habitLog.Habit.HabitId);

        if (habit == null)
        {
            Console.WriteLine("There is no habit with the given number!");

            return;
        }

        var quantityUnit = ReadQuantityForHabit(habit.HabitId);

        if (quantityUnit == null)
        {
            Console.WriteLine($"Quantity not found for habit {habitLog.Habit.Name}!");

            return;
        }

        habitLog.QuantityUnit = quantityUnit;

        _habitDatabase.CreateHabitLog(habitLog);
    }

    public List<HabitLog> ReadHabitLogs()
    {
        return _habitDatabase.ReadHabitLogs();
    }

    public void UpdateHabitLog(HabitLog habitLog)
    {
        _habitDatabase.UpdateHabitLog(habitLog);
    }

    public void DeleteHabitLog(int id)
    {
        _habitDatabase.DeleteHabitLog(id);
    }
    
    private QuantityUnit? CreateQuantityUnit(QuantityUnit quantityUnit)
    {
        QuantityUnit? quantityUnitDb = null;
        bool newQuantityToBeCreated = quantityUnit.QuantityUnitId == 0;
       
        if (newQuantityToBeCreated)
            quantityUnitDb = _habitDatabase.GetQuantityUnitByName(quantityUnit.Name);
        else 
            quantityUnitDb = _habitDatabase.GetQuantityUnitById(quantityUnit.QuantityUnitId);

        bool quantityFoundInDb = quantityUnitDb != null;

        if (!newQuantityToBeCreated && !quantityFoundInDb)
            throw new InvalidOperationException("\nUnit of measurement not found. Habit could not be added.");

        if (!quantityFoundInDb)
        {
            _habitDatabase.CreateQuantityUnit(quantityUnit.Name);
            quantityUnitDb = _habitDatabase.GetQuantityUnitByName(quantityUnit.Name);
            
            Console.WriteLine("\nUnit of measurement created successfully!");
        }

        return quantityUnitDb;
    }

    public string ReadQuantityNameForHabit(int habitId)
    {
        return ReadQuantityForHabit(habitId)?.Name ?? "N/A";
    }

    public QuantityUnit? ReadQuantityForHabit(int habitId)
    {
        return _habitDatabase.GetQuantityUnitByHabitId(habitId);
    }

    public List<QuantityUnit> ReadQuantityUnits()
    {
        return _habitDatabase.ReadQuantityUnits();
    }
}