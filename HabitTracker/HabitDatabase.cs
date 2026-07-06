using System.Globalization;
using Microsoft.Data.Sqlite;

namespace HabitTracker;
public class HabitDatabase
{
    private string _connectionString;
    public HabitDatabase(string connectionString)
    {
        _connectionString = connectionString;

        Console.WriteLine("Setting up the database...");

        try
        {
            int affectedRows = CreateTables();
            if (affectedRows != 0)
                Console.WriteLine($"Tables have been created successfully!");

            if (!(ReadHabits().Any() || ReadQuantityUnits().Any() || ReadHabitLogs().Any()))
            {
                SeedDatabase();
                Console.WriteLine($"Database has been seeded successfully!");
            }

            Console.WriteLine("Setup completed.");
        }
        catch (SqliteException ex)
        {
            Console.WriteLine("There was database error. Seeding was not successful.");
            Console.WriteLine($"Message: {ex.Message}");
        }
        catch (Exception)
        {
            Console.WriteLine("An unexpected error occurred!");
        }
    }

    private void SeedDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = $"{DatabaseSeed.GetHabitSeedQuery()} {DatabaseSeed.GetQuantityUnitSeedQuery()} {DatabaseSeed.GetHabitLogSeedQuery()}";

        int affectedRows = command.ExecuteNonQuery();

        connection.Close();
    }
    private int CreateTables()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string createTableStatements = 
            @"CREATE TABLE IF NOT EXISTS habit (
                habit_id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT
            );
            CREATE TABLE IF NOT EXISTS quantity_unit (
                quantity_unit_id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT
            );
            CREATE TABLE IF NOT EXISTS habit_log (
                habit_log_id INTEGER PRIMARY KEY AUTOINCREMENT,
                habit_id INTEGER,
                date TEXT,
                quantity INTEGER,
                quantity_unit_id INTEGER,
                FOREIGN KEY(habit_id) REFERENCES habit(habit_id) ON DELETE CASCADE,
                FOREIGN KEY(quantity_unit_id) REFERENCES quantity_unit(quantity_unit_id) ON DELETE RESTRICT
            );";

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = createTableStatements;
        int affectedRows = command.ExecuteNonQuery();

        connection.Close();

        return affectedRows;
    }

    public void CreateHabit(Habit habit)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO habit(name) VALUES(@HabitName)";
        command.Parameters.AddWithValue("@HabitName", habit.Name);

        command.ExecuteNonQuery();

        connection.Close();
    }
    public List<Habit> ReadHabits()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM habit;";

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
                    HabitId = reader.GetInt32(0), 
                    Name = reader.GetString(1)
                }
            );
        }

        connection.Close();

        return habits;
    }

    public string UpdateHabit(Habit habit)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE habit SET name = @Name WHERE habit_id = @HabitId";
        command.Parameters.AddWithValue("@Name", habit.Name);
        command.Parameters.AddWithValue("@HabitId", habit.HabitId);
        int affectedRows = command.ExecuteNonQuery();

        connection.Close();

        return affectedRows > 0 ? "Update was successful!" : "Habit was NOT updated. Something went wrong.";
    }

    public void DeleteHabit(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = "DELETE habit WHERE habit_id = @HabitId";
        command.Parameters.AddWithValue("@HabitId", id);
        command.ExecuteNonQuery();

        connection.Close();
    }
    
    public Habit? GetHabitById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM habit WHERE habit_id = @HabitId";
        command.Parameters.AddWithValue("@HabitId", id);
        SqliteDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            connection.Close();

            return null;
        }
        
        Habit habit = new Habit { HabitId = reader.GetInt32(0), Name = reader.GetString(1) };
        connection.Close();

        return habit;
    }
    public void CreateHabitLog(HabitLog habitLog)
    {
        if (habitLog == null)
        {
            Console.WriteLine("Habit is empty, please provide valid data!");

            return;
        }

        using var connection = new SqliteConnection(_connectionString);

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO habit_log(habit_id, date, quantity, quantity_unit_id) VALUES(@HabitId, @Date, @Quantity, @QuantityUnitId)";
        command.Parameters.AddWithValue("@HabitId", habitLog.Habit.HabitId);
        command.Parameters.AddWithValue("@Date", habitLog.Date.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("@Quantity", habitLog.Quantity);
        command.Parameters.AddWithValue("@QuantityUnitId", habitLog.QuantityUnit.QuantityUnitId);
        connection.Open();

        int affectedRows = command.ExecuteNonQuery();

        Console.WriteLine($"{affectedRows} habit(s) added to database!");

        connection.Close();
    }

    public List<HabitLog> ReadHabitLogs()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT habit_log_id, habit.habit_id, habit.name, date, 
                                quantity, quantity_unit.quantity_unit_id, quantity_unit.name 
                                FROM habit_log 
                                JOIN quantity_unit ON habit_log.quantity_unit_id = quantity_unit.quantity_unit_id 
                                JOIN habit ON habit_log.habit_id = habit.habit_id;";

        List<HabitLog> habitLogs = new List<HabitLog>();
        SqliteDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            Console.WriteLine("No data present!");
            connection.Close();

            return habitLogs;
        }

        while (reader.Read())
        {
            habitLogs.Add(
                new HabitLog
                {
                    Id = reader.GetInt32(0), 
                    Habit = new Habit(reader.GetInt32(1), reader.GetString(2)),
                    Date = DateTime.ParseExact(reader.GetString(3), "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Quantity = reader.GetInt32(4),
                    QuantityUnit = new QuantityUnit(reader.GetInt32(5), reader.GetString(6))
                }
            );
        }

        connection.Close();

        return habitLogs;
    }

    public void UpdateHabitLog(HabitLog habitLog)
    {
        if (habitLog == null || habitLog.Id <= 0)
        {
            Console.WriteLine("Data is not valid! Please provide valid data!");
            return;
        }

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE habit_log SET habit_id = @HabitId, date = @Date, quantity = @Quantity, quantity_unit_id = @QuantityUnitId WHERE habit_log_id = @HabitLogId";
        command.Parameters.AddWithValue("@HabitId", habitLog.Habit.HabitId);
        command.Parameters.AddWithValue("@Date", habitLog.Date.ToShortDateString());
        command.Parameters.AddWithValue("@Quantity", habitLog.Quantity);
        command.Parameters.AddWithValue("@QuantityUnitId", habitLog.QuantityUnit.QuantityUnitId);
        command.Parameters.AddWithValue("@HabitLogId", habitLog.Id);
        command.ExecuteNonQuery();

        connection.Close();
    }

    public void DeleteHabitLog(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM habit_log WHERE habit_log_id = @Id";
        command.Parameters.AddWithValue("@Id", id);
        int affectedRows = command.ExecuteNonQuery();

        Console.WriteLine($"{affectedRows} habit logs have been deleted from the database!");

        connection.Close();
    }

    public List<QuantityUnit> ReadQuantityUnits()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM quantity_unit;";

        List<QuantityUnit> quantities = new List<QuantityUnit>();
        SqliteDataReader reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            Console.WriteLine("No data present!");
            connection.Close();

            return quantities;
        }

        while (reader.Read())
        {
            quantities.Add(
                new QuantityUnit
                {
                    QuantityUnitId = reader.GetInt32(0), 
                    Name = reader.GetString(1),
                }
            );
        }

        connection.Close();

        return quantities;
    }
}