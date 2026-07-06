using HabitTracker;

public class HabitService
{
    private readonly HabitDatabase _habitDatabase;
    public HabitService(HabitDatabase habitDatabase)
    {
        _habitDatabase = habitDatabase;
    }

    public void CreateHabit(Habit habit)
    {
        _habitDatabase.CreateHabit(habit);
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
        List<Habit> habits = ReadHabits();
        Habit? habit = habits.Find(h => h.HabitId == habitLog.Habit.HabitId);

        if (habit == null)
        {
            Console.WriteLine("There is no habit with the given number!");

            return;
        }

        // How to find unit? If log already exists, use that, if not, request input. -> move logic to controller?
        

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

    
}