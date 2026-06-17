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
}
