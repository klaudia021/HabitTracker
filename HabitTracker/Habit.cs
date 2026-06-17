namespace HabitTracker;
public class Habit
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Quantity { get; set; }
    public Habit(int id, DateTime date, int quantity)
    {
        Id = id;
        Date = date;
        Quantity = quantity;
    }
    public Habit() {}

    public override string ToString()
    {
        return $"Id: {Id} \t Date: {Date.ToShortDateString()} \t Quantity: {Quantity}";
    }
}