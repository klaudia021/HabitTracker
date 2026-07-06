public static class DatabaseSeed
{
    public static string GetHabitSeedQuery()
    {
        return @"INSERT INTO habit(name) VALUES
                ('Drink Water'),
                ('Walk'),
                ('Run'),
                ('Read'),
                ('Journal'),
                ('Calories consumed'),
                ('Test habit to be renamed');";
    }

    public static string GetQuantityUnitSeedQuery()
    {
        return @"INSERT INTO quantity_unit(name) VALUES
                ('Liters'),
                ('Steps'),
                ('Kilometers'),
                ('Pages'),
                ('Entries'),
                ('Calories (kcal)');";
    }

    public static string GetHabitLogSeedQuery()
    {
        return @"INSERT INTO habit_log(habit_id, date, quantity, quantity_unit_id) VALUES
                (1, '2026-05-11', 2, 1),
                (1, '2026-05-12', 4, 1),
                (2, '2026-05-12', 5000, 2),
                (3, '2026-05-13', 2, 3),
                (4, '2026-05-13', 15, 4),
                (4, '2026-05-14', 3, 4),
                (5, '2026-05-14', 1, 5),
                (5, '2026-05-16', 2, 5),
                (6, '2026-05-17', 300, 6),
                (6, '2026-05-18', 1250, 6),
                (1, '2026-05-15', 3, 1),
                (2, '2026-05-18', 10000, 2),
                (7, '2026-03-20', 3, 2),
                (7, '2026-02-22', 1, 2),
                (4, '2026-05-11', 12, 4);";
    }
}