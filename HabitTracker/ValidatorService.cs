using System.Globalization;

namespace Services;

public static class ValidatorService
{
    public static DateTime GetValidDateTime()
    {
        DateTime date = DateTime.Now;
        bool validInput = false;

        do
        {
            try
            {
                Console.Write("\nEnter the habit's date (2026-05-19) OR enter 't' for today's date: ");
                string input = GetNotNullInput();

                if (input == "t")
                {
                    date = DateTime.Now;
                    validInput = true;
                }
                else
                {
                    date = DateTime.ParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    validInput = true;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Date is not in the correct format!");
            }
            
        } while (!validInput);

        return date;
    }

    public static int GetValidNumber()
    {
        int number = 0;
        string input;

        do
        {
            Console.Write("\nEnter a number: ");
            input = GetNotNullInput();

        } while (!int.TryParse(input, out number));

        return number;
    }

    public static string GetNotNullInput()
    {
        string? input = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(input))
        {
            Console.Write("\nPlease enter a valid input: ");

            input = Console.ReadLine();
        }

        return input;
    }
}