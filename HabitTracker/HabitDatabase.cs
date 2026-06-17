using System.Globalization;
using Microsoft.Data.Sqlite;

namespace HabitTracker;
public class HabitDatabase
{
    private string _connectionString;
    public HabitDatabase(string connectionString)
    {
        _connectionString = connectionString;

        CreateTable();
    }

    private void CreateTable()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"CREATE TABLE IF NOT EXISTS DrinkingWater (
                                    DrinkingWaterId INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Date TEXT,
                                    Quantity INTEGER
                                )";

        command.ExecuteNonQuery();

        connection.Close();
    }

    public void Create(Habit habit)
    {
        if (habit == null)
        {
            Console.WriteLine("Habit is empty, please provide valid data!");

            return;
        }

        using var connection = new SqliteConnection(_connectionString);

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO DrinkingWater(Date, Quantity) VALUES(@Date, @Quantity)";
        command.Parameters.AddWithValue("@Date", habit.Date.ToShortDateString());
        command.Parameters.AddWithValue("@Quantity", habit.Quantity);

        connection.Open();

        int affectedRows = command.ExecuteNonQuery();

        Console.WriteLine($"{affectedRows} habit(s) added to database!");

        connection.Close();
    }

    public List<Habit> Read()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM DrinkingWater;";

        List<Habit> habits = new List<Habit>();
        SqliteDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            Console.WriteLine("No data present!");
            connection.Close();

            return habits;
        }

        while (reader.Read())
        {
            habits.Add(
                new Habit
                {
                    Id = reader.GetInt32(0), 
                    Date = DateTime.ParseExact(reader.GetString(1), "dd/MM/yyyy", new CultureInfo("en-US")),
                    Quantity = reader.GetInt32(2)
                }
            );
        }

        connection.Close();

        return habits;
    }

    public void Update()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        connection.Close();
    }

    public void Delete()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        connection.Close();
    }
}