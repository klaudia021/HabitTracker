namespace HabitTracker;
public class HabitLog
{
    public int Id { get; set; }
    public Habit Habit { get; set; }
    public DateTime Date { get; set; }
    public double Quantity { get; set; }
    public QuantityUnit QuantityUnit { get; set; }
    public HabitLog(int id, Habit habit, DateTime date, double quantity, QuantityUnit quantityUnit)
    {
        Id = id;
        Habit = habit;
        Date = date;
        Quantity = quantity;
        QuantityUnit = quantityUnit;
    }

    public HabitLog()
    {
        Habit = new Habit();
        QuantityUnit = new QuantityUnit();
    }

    public override string ToString()
    {
        return $"Id: {Id, -5} Name: {Habit.Name, -20} Date: {Date.ToString("yyyy-MM-dd")} \t Quantity: {Quantity, 6} \t Unit: {QuantityUnit.Name}";
    }
}