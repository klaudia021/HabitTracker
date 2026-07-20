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
        const string CreateHabitText =      "1 - Create a habit";
        const string LogHabitText =         "2 - Log a habit";
        const string SeeAllLogsText =       "3 - See all habit logs";
        const string SeeOneLogText =        "4 - See one habit's logs";
        const string SeeAllHabitsText =     "5 - See all habits";
        const string ModifyMenuText =       "6 - Modify menu";

        using var output = new StringWriter();
        Console.SetOut(output);

        // Act
        MenuService.ListMainMenu();

        // Assert
        Assert.That(output.ToString(), Does.Contain(CreateHabitText));
        Assert.That(output.ToString(), Does.Contain(LogHabitText));
        Assert.That(output.ToString(), Does.Contain(SeeAllLogsText));
        Assert.That(output.ToString(), Does.Contain(SeeOneLogText));
        Assert.That(output.ToString(), Does.Contain(SeeAllHabitsText));
        Assert.That(output.ToString(), Does.Contain(ModifyMenuText));
    }
}
