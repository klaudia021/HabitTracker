using Services;

namespace HabitTrackerTests;

public class MenuServiceTests
{
    [OneTimeSetUp]
    public void Setup()
    {}

    [TestCase("1", 1)]
    [TestCase("2", 2)]
    [TestCase("3", 3)]
    [TestCase("4", 4)]
    public void GetValidMenuInput_ShouldReturnIntegerNumber_WhenValidInputIsProvided(
            string stringInput,
            int expectedInt
        )
    {
        // Arrange
        using var input = new StringReader(stringInput);
        Console.SetIn(input);

        // Act
        int menuNumber = MenuService.GetValidMenuInput(stringInput);

        // Assert
        Assert.That(menuNumber, Is.EqualTo(expectedInt));
    }

    [Test]
    public void ListMenu_ShouldListMenuOptions()
    {
        // Arrange
        const string ViewAllText =      "1 - View all records";
        const string InsertRecordText = "2 - Insert a record";
        const string DeleteRecordText = "3 - Delete a record";
        const string UpdateRecordText = "4 - Update a record";

        using var output = new StringWriter();
        Console.SetOut(output);

        // Act
        MenuService.ListMainMenu();

        // Assert
        Assert.That(output.ToString(), Does.Contain(ViewAllText));
        Assert.That(output.ToString(), Does.Contain(InsertRecordText));
        Assert.That(output.ToString(), Does.Contain(DeleteRecordText));
        Assert.That(output.ToString(), Does.Contain(UpdateRecordText));
    }
}
