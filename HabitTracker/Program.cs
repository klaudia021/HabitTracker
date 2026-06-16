using Microsoft.Data.Sqlite;

string connectionString = @"Data Source=Habit-Tracker.db";

using (var connection = new SqliteConnection(connectionString))
{
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