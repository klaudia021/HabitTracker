public class Habit
{
    public int HabitId { get; set; }
    public string Name { get; set; }

    public Habit(int habitId, string name)
    {
        HabitId = habitId;
        Name = name;
    }

    public Habit() {}

    public override string ToString()
    {
        return $"Id: {HabitId} \t Name: {Name}";
    }
}